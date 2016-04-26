using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public LayerMask detectionLayer;
	Collider[] hitColliders;
	float checkRate;
	// distribute the decision making between lots of AI
	float nextCheck;
	float detectionRadius = 150f;
	NavMeshAgent myNavMeshAgent;
	EnemyShoot enemyShootScript;
	PlayerController pc;
	Vector3 destinationSpawn;
	bool animal = false;

	// Use this for initialization
	void Start ()
	{
		myNavMeshAgent = GetComponent<NavMeshAgent> ();
		checkRate = Random.Range (0.8f, 1.2f);
		enemyShootScript = gameObject.GetComponentInChildren<EnemyShoot> ();

		if (!enemyShootScript) {
			throw new UnityException ("no enemy shoot script found");
		}

		pc = GetComponent<PlayerController> ();

		if (pc.GetTeam ().Equals ("ANIMALS")) {
			animal = true;
			GameObject[] spawns = GameObject.FindGameObjectsWithTag ("Farmer Spawn");
			this.destinationSpawn = spawns [0].GetComponent<Spawner> ().GetSpawnPosition ();
		} else {
			GameObject[] spawns = GameObject.FindGameObjectsWithTag ("Animal Spawn");
			// pick random animal spawn
			float rand = Random.value;
			if (rand < 0.3) {
				this.destinationSpawn = spawns [0].GetComponent<Spawner> ().GetSpawnPosition ();
			} else if (rand < 0.6) {
				this.destinationSpawn = spawns [1].GetComponent<Spawner> ().GetSpawnPosition ();
			} else {
				this.destinationSpawn = spawns [2].GetComponent<Spawner> ().GetSpawnPosition ();
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time < nextCheck) {
			return;
		}

		if (animal) {
			// #1 priority
			if (CheckIfTurretInRange ()) {
				return;
			}
		}

		if (!CheckIfEnemyInRange ()) {
			GoTowardsSpawn ();
		}
	}

	bool CheckIfTurretInRange ()
	{
		nextCheck = Time.time + checkRate;

		hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, LayerMask.GetMask ("Turret"));

		if (hitColliders.Length > 0) {
			// we have detected a turret
			float rand = Random.value;

			if (rand > .4) {
				Debug.Log ("moving towards turret");
				// move towards player
				myNavMeshAgent.SetDestination (hitColliders [0].transform.position);
				enemyShootScript.Shoot ();
				return true;
			}
		}
		return false;
	}

	void GoTowardsSpawn ()
	{
		myNavMeshAgent.SetDestination (destinationSpawn);
		nextCheck = Time.time + checkRate;
	}

	bool CheckIfEnemyInRange ()
	{
		nextCheck = Time.time + checkRate;

		hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, detectionLayer);

		if (hitColliders.Length > 0) {
			// we have detected the player
			float rand = Random.value;

			if (rand > .4) {
				// move towards player
				myNavMeshAgent.SetDestination (hitColliders [0].transform.position);

				if (rand > .6) {
					enemyShootScript.Shoot ();
				}
				return true;
			}

			LookAtTarget (hitColliders [0].transform.position);
			enemyShootScript.Shoot ();
		}
		return false;
	}

	void LookAtTarget (Vector3 target)
	{
		float rotationSpeed = 1000f;
		//find the vector pointing from our position to the target
		Vector3 _direction = (target - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
	}
}
