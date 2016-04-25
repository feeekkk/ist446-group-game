using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public float health = 100;
	public float maxHealth = 100;
	GameObject blood1;
	GameObject blood2;
	GameObject blood3;
	GameObject blood4;
	//public float increaseHealth;
	Text healthLeft;

	// Use this for initialization
	void Start ()
	{
		healthLeft = GameObject.Find ("Health Number").GetComponent<Text> ();
		InvokeRepeating ("IncreaseHealth", 10.0f, 10.0f);
		Mathf.Clamp (this.health, 0, maxHealth);
		blood1 = GameObject.Find ("BloodEmpty");
		blood2 = GameObject.Find ("Blood1");
		blood3 = GameObject.Find ("Blood2");
		blood4 = GameObject.Find ("Blood3");
		blood1.GetComponent<Image> ().enabled = true;
		blood2.GetComponent<Image> ().enabled = false;
		blood3.GetComponent<Image> ().enabled = false;
		blood4.GetComponent<Image> ().enabled = false;
	}

	void IncreaseHealth ()
	{
		if (this.health < this.maxHealth)
			this.health += 10.0f;
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		if (health <= 0) {
			Die ();
		}	
		if (gameObject.tag == "Player")
			healthLeft.text = this.health.ToString ();
	}

	public void Die ()
	{
		// update gui
		if (gameObject.tag == "Player") {
			gameObject.GetComponent<PlayerController> ().Die ();
			return;
		}
		gameObject.SetActive (false);
	}

	public void SetMaxHealth (float max)
	{
		this.maxHealth = max;
		Mathf.Clamp (this.health, 0, this.maxHealth);
	}

	public void SetHealth (float resetHealth)
	{
		this.health = resetHealth;
		Mathf.Clamp (this.health, 0, this.maxHealth);
		healthLeft.text = this.health.ToString ();
	}

	public void Heal (float amount)
	{
		this.health = amount;
	}

	void Update ()
	{
		if (gameObject.tag == "Player") {
			if (health <= 100 && health > 70) {
				blood1.GetComponent<Image> ().enabled = true;
				blood2.GetComponent<Image> ().enabled = false;
				blood3.GetComponent<Image> ().enabled = false;
				blood4.GetComponent<Image> ().enabled = false;
			} 
			if (health <= 70 && health > 50) {
				blood2.GetComponent<Image> ().enabled = true;
			} 
			if (health <= 50 && health > 20) {
				blood3.GetComponent<Image> ().enabled = true;
			} 
			if (health <= 20) {
				blood4.GetComponent<Image> ().enabled = true;
			}
		}
	}
}
