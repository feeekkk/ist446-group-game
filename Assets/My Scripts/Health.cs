using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health = 100;
	public float maxHealth = 100;
	public float destroyTime = 5;
	// seconds before object is destroyed after dying

	// Use this for initialization
	void Start ()
	{
		Mathf.Clamp (this.health, 0, maxHealth);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		if (health <= 0) {
			Die ();
		}
	}

	public void Die ()
	{
		//Debug.Log (gameObject.ToString () + "is dead.");
		Destroy (gameObject, destroyTime);
	}

	public void SetMaxHealth (float max)
	{
		this.maxHealth = max;
		Mathf.Clamp (this.health, 0, this.maxHealth);
	}

	public void Heal (float amount)
	{
		this.health = amount;
	}
}
