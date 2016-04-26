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
	Text healthLeft;
	PlayerController pc;
	float timeToBeginHealing;
	float timeUntilBeginHealing = 5f;
	// 5 seconds without being hit

	// Use this for initialization
	void Start ()
	{
		healthLeft = GameObject.Find ("Health Number").GetComponent<Text> ();
		Mathf.Clamp (this.health, 0, maxHealth);
		blood1 = GameObject.Find ("BloodEmpty");
		blood2 = GameObject.Find ("Blood1");
		blood3 = GameObject.Find ("Blood2");
		blood4 = GameObject.Find ("Blood3");
		blood1.GetComponent<Image> ().enabled = true;
		blood2.GetComponent<Image> ().enabled = false;
		blood3.GetComponent<Image> ().enabled = false;
		blood4.GetComponent<Image> ().enabled = false;

		pc = GetComponent<PlayerController> ();
		if (!pc) {
			pc = GetComponentInParent<PlayerController> ();
		}
	}

	public void TakeDamage (float amount)
	{
		health -= amount;

		if (health <= 0) {
			Die ();
		}	
		if (pc && pc.isLocalPlayer)
			healthLeft.text = this.health.ToString ();

		timeToBeginHealing = Time.time + timeUntilBeginHealing;
	}

	public void Die ()
	{
		if (pc) {
			pc.Die ();
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
		if (pc && pc.isLocalPlayer) {
			healthLeft.text = this.health.ToString ();
		}
	}

	public void Heal (float amount)
	{
		this.health += amount;
		if (pc && pc.isLocalPlayer) {
			healthLeft.text = this.health.ToString ();
		}
	}

	void Update ()
	{
		if (pc && pc.isLocalPlayer) {
			if (health >= 100) {
				blood1.GetComponent<Image> ().enabled = false;
				blood2.GetComponent<Image> ().enabled = false;
				blood3.GetComponent<Image> ().enabled = false;
				blood4.GetComponent<Image> ().enabled = false;
			}
			if (health < 100 && health > 70) {
				blood1.GetComponent<Image> ().enabled = true;
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

		if (health < 100 && (Time.time > timeToBeginHealing)) {
			Heal (1);
			Debug.Log ("healing: " + health);
		}
	}
}
