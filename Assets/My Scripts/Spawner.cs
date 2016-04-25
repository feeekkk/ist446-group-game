using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject objectToSpawn;
	public int amount;
	public float spawnRadius = 3f;
	// Use this for initialization
	void Start ()
	{
		Spawn ();
	}

	public void Spawn ()
	{
		for (int i = 0; i < amount; i++) {
			Vector3 pos = GetSpawnPosition ();
			Instantiate (objectToSpawn, pos, Quaternion.identity);
		}
	}

	public Vector3 GetSpawnPosition ()
	{
		return transform.position + Random.insideUnitSphere * spawnRadius;
	}
}
