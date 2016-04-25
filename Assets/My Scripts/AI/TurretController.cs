using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour
{
	public LayerMask detectionLayer;
	float rotationSpeed = 1000f;
	// to do: make animal the detection layer
	Collider[] hitColliders;
	float checkRate = 1f;
	// once a second
	float nextCheck;
	float detectionRadius = 100f;
	Shoot shootScript;

	// Use this for initialization
	void Start ()
	{
		shootScript = gameObject.GetComponent<Shoot> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckIfPlayerInRange ();
	}

	void CheckIfPlayerInRange ()
	{
		if (Time.time > nextCheck) {
			nextCheck = Time.time + checkRate;

			hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, detectionLayer);

			if (hitColliders.Length > 0) {
				// detected player
				//Debug.Log ("Turret Detected player");
				RotateToPlayer ();
				shootScript.FireWeapon ();
			}
		}
	}

	void RotateToPlayer ()
	{
		//Debug.Log ("rotating");
		GameObject target = hitColliders [0].gameObject;

		//find the vector pointing from our position to the target
		Vector3 _direction = (target.transform.position - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation (_direction);

		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
	}
}
