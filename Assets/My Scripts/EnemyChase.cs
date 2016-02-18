using UnityEngine;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
	public LayerMask detectionLayer;
	Collider[] hitColliders;
	float checkRate;
	// distribute the decision making between lots of AI
	float nextCheck;
	float detectionRadius = 50f;
	NavMeshAgent myNavMeshAgent;

	// Use this for initialization
	void Start ()
	{
		myNavMeshAgent = GetComponent<NavMeshAgent> ();
		checkRate = Random.Range (0.8f, 1.2f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckIfPlayerInRange ();
	}

	void CheckIfPlayerInRange ()
	{
		if (Time.time > nextCheck && myNavMeshAgent.enabled == true) {
			nextCheck = Time.time + checkRate;

			hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, detectionLayer);

			if (hitColliders.Length > 0) {
				// we have detected the player, lets move towards it
				myNavMeshAgent.SetDestination (hitColliders [0].transform.position);
			}
		}
	}
}
