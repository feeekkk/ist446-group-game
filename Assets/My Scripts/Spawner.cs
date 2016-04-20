using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject objectToSpawn;
	public int amount;
	public float spawnRadius = 3f;
	private Vector3 spawnPosition;

	// Use this for initialization
	void Start ()
	{
		Spawn ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Spawn ()
	{
		for (int i = 0; i < amount; i++) {
			spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
			Instantiate (objectToSpawn, spawnPosition, Quaternion.identity);
		}
	}
}
