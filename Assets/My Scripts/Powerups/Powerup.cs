using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Powerup : MonoBehaviour
{
	public string name;
	GameController gc;

	void Start ()
	{
		gc = ScriptableObject.CreateInstance<GameController> ();
	}

	void OnTriggerEnter (Collider c)
	{
		Debug.Log ("entered");
		if (c.gameObject.tag != "Player") {
			return;
		}

		switch (name) {
		case "Bear Strength":
			gc.IncreasePlayerMaxHealth (200f);
			break;
		case "Rapid Fire":
			break;
		case "Eye Of The Tiger":
			break;
		case "Catlike Instincts":
			break;
		}
		Destroy (transform.parent.gameObject);
	}
		
}
