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
	bool hasTarget = false;
	Vector3 targetDest;

	// Use this for initialization
	void Start ()
	{
		myNavMeshAgent = GetComponent<NavMeshAgent> ();
		checkRate = Random.Range (5f, 10f);
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
		if (Time.time > nextCheck) {
			hasTarget = false;
			Debug.Log ("getting new target");
			nextCheck = Time.time + checkRate;
		}

		if (hasTarget) {
			myNavMeshAgent.SetDestination (targetDest);
			enemyShootScript.Shoot ();
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
		hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, LayerMask.GetMask ("Turret"));

		if (hitColliders.Length > 0) {
			// we have detected a turret
			float rand = Random.value;

			if (rand > .4) {
				//Debug.Log ("moving towards turret");
				// move towards player
				int ran = Random.Range (0, hitColliders.Length);
				targetDest = hitColliders [ran].transform.position;
				myNavMeshAgent.SetDestination (targetDest);
				hasTarget = true;
				return true;
			}
		}
		return false;
	}

	void GoTowardsSpawn ()
	{
		myNavMeshAgent.SetDestination (destinationSpawn);
		this.hasTarget = true;
		targetDest = destinationSpawn;
	}

	bool CheckIfEnemyInRange ()
	{

		hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, detectionLayer);

		if (hitColliders.Length > 0) {
			// we have detected enemy
			float ran = Random.value;

			int rand = Random.Range (0, hitColliders.Length);
			if (ran > .3) {
				// move towards whoever
				targetDest = hitColliders [rand].transform.position;
				myNavMeshAgent.SetDestination (targetDest);
				hasTarget = true;
				return true;
			}
			// dont run to them but shoot at them
			LookAtTarget (hitColliders [rand].transform.position);
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
