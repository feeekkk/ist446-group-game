using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject objectToSpawn;
	public int numberOfEnemies;
	public float spawnRadius = 3f;
	private Vector3 spawnPosition;

	// Use this for initialization
	void Start ()
	{
		SpawnObjects ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void SpawnObjects ()
	{
		for (int i = 0; i < numberOfEnemies; i++) {
			spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
			Instantiate (objectToSpawn, spawnPosition, Quaternion.identity);
		}
	}
}
