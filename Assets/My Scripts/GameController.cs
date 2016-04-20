using UnityEngine;
using System.Collections;

public class GameController : ScriptableObject
{
	GameObject player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		// this is null cause player is instantiated after. so fetch it again later
	}

	/**
	 * Increases player's max's health and then sets health to be full amount
	 */ 
	public void IncreasePlayerMaxHealth (float max)
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	
		if (!player.GetComponent<Health> ()) {
			Debug.LogError ("No Health Script Found On Player");
			return;
		}

		player.GetComponent<Health> ().SetMaxHealth (max);
		player.GetComponent<Health> ().Heal (max);
	}

	public void ResetPlayerMaxHealth (GameObject player)
	{
		player.GetComponent<Health> ().SetMaxHealth (100); // to do: pull from health script
	}
}
