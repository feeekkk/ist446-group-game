using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health = 100;
	public float maxHealth = 100;

	// Use this for initialization
	void Start ()
	{
		Mathf.Clamp (this.health, 0, maxHealth);
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
