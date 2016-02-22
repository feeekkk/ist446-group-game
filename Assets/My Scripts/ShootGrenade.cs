using UnityEngine;
using System.Collections;

public class ShootGrenade : MonoBehaviour
{
	public GameObject grenadePrefab;
	public float timeToLive = 10f;
	private float propulsionForce = 300f;
	float fireRate = 1f;
	float nextFire = 0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire2") && Time.time > nextFire) {
			SpawnGrenade ();
			nextFire = Time.time + fireRate;
		}
	}

	void SpawnGrenade ()
	{
		GameObject go = (GameObject)Instantiate (grenadePrefab, transform.TransformPoint (0, 0, 1f), transform.rotation);
		go.GetComponent<Rigidbody> ().AddForce (transform.forward * propulsionForce, ForceMode.Impulse);
		Destroy (go, timeToLive);
	}
}
