using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public int health = 100;
	public float destroyTime = 5;
	// seconds before object is destroyed after dying

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void TakeDamage (int amount)
	{
		health -= amount;

		if (health <= 0) {
			Die ();
		}
	}

	public void Die ()
	{
		Debug.Log (gameObject.ToString () + "is dead.");
		Destroy (gameObject, destroyTime);
	}
}
