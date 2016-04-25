using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public LayerMask detectionLayer;
	Collider[] hitColliders;
	float checkRate;
	// distribute the decision making between lots of AI
	float nextCheck;
	float detectionRadius = 50f;
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
		enemyShootScript = gameObject.GetComponent<EnemyShoot> ();

		if (!enemyShootScript) {
			throw new UnityException ("no enemy shoot script found");
		}

		pc = GetComponent<PlayerController> ();

		if (pc.GetTeam ().Equals ("ANIMALS")) {
			animal = true;
			this.destinationSpawn = GameObject.Find ("Farmer Spawn").GetComponent<Spawner> ().GetSpawnPosition ();
		} else {
			this.destinationSpawn = GameObject.Find ("Animal Spawn").GetComponent<Spawner> ().GetSpawnPosition ();
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
		Debug.Log ("going towards spawn");
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
				Debug.Log ("moving towards player");
				// move towards player
				myNavMeshAgent.SetDestination (hitColliders [0].transform.position);

				if (rand > .6) {
					Debug.Log ("and shooting at player");
					enemyShootScript.Shoot ();
				}
				return true;
			}

			Debug.Log ("moving towards barn");
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
