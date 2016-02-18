using UnityEngine;
using System.Collections;

public class ThrowGrenade : MonoBehaviour
{
	public GameObject grenadePrefab;
	public float timeToLive = 10f;
	public float propulsionForce = 200f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			SpawnGrenade ();
		}
	}

	void SpawnGrenade ()
	{
		GameObject go = (GameObject)Instantiate (grenadePrefab, transform.TransformPoint (0, 0, 0.5f), transform.rotation);
		go.GetComponent<Rigidbody> ().AddForce (transform.forward * propulsionForce, ForceMode.Impulse);
		Destroy (go, timeToLive);
	}
}
