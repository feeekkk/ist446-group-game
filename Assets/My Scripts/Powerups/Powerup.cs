using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup : MonoBehaviour
{
	public string powerupName;

	void OnTriggerEnter (Collider c)
	{
		if (c.gameObject.tag != "Player") {
			return;
		}

		PlayerController pc = c.gameObject.GetComponent<PlayerController> ();

		if (!pc) {
			Debug.LogError ("No player controller found on player");
			return;
		}

		switch (powerupName) {
		case "Bear Strength":
			pc.IncreasePlayerMaxHealth (200f);
			break;
		case "Rapid Fire":
			pc.AddRapidFire ();
			break;
		case "Eye Of The Tiger":
			pc.IncreaseAccuracy ();
			break;
		case "Catlike Instincts":
			pc.AddLife ();
			break;
		}
		Destroy (transform.parent.gameObject);
	}
		
}
