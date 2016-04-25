using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
	[SerializeField] Material[] animalMaterials;

	public enum Teams
	{
		FARMERS,
		ANIMALS}

	;

	private Teams team = Teams.FARMERS;
	public int lives = 5;
	Text livesText;
	private bool rapidFire = false;
	private bool increasedAccuracy = false;
	private int count;
	// count of frames since damage last taken

	void Start ()
	{
		livesText = GameObject.Find ("Num Lives").GetComponent<Text> ();
		livesText.text = lives.ToString ();
		int numPlayers = ClientScene.localPlayers.Count;
		if (numPlayers % 4 != 1) {
			team = Teams.ANIMALS;
			int index = (int)Random.Range (0, animalMaterials.Length);
			Material[] materials = new Material[1];
			materials [0] = animalMaterials [index];
			//GetComponent<MeshRenderer>.materials = materials;
		}
	}

	void Update ()
	{
		count++;
		if (GetComponent<Health> ().health < GetComponent<Health> ().maxHealth && count > 300) {
			GetComponent<Health> ().Heal (1);
		}
	}

	public void Die ()
	{
		this.lives--;
		livesText.text = this.lives.ToString ();
		SetEnabled (false);
		ClearPowerups ();
		StartCoroutine (WaitToRespawn ());
		ResetPlayerHealth ();
	}

	void ClearPowerups ()
	{
		this.rapidFire = false;
		this.increasedAccuracy = false;
	}

	IEnumerator WaitToRespawn ()
	{
		yield return new WaitForSeconds (5);
		Respawn ();
	}

	/**
	 * helper for enabling / disabling all the components
	 */
	private void SetEnabled (bool enabled)
	{
		gameObject.GetComponent<CharacterController> ().enabled = enabled;
	}

	public void Respawn ()
	{
		SetEnabled (true);
	}

	public void IncreasePlayerMaxHealth (float max)
	{
		if (!gameObject.GetComponent<Health> ()) {
			Debug.LogError ("No Health Script Found On Player");
			return;
		}

		gameObject.GetComponent<Health> ().SetMaxHealth (max);
		gameObject.GetComponent<Health> ().Heal (max);
	}

	public void ResetPlayerMaxHealth ()
	{
		gameObject.GetComponent<Health> ().SetMaxHealth (100); // to do: pull from health script
	}

	public void ResetPlayerHealth ()
	{
		gameObject.GetComponent<Health> ().SetHealth (100); // to do: pull from health script
	}

	public void AddLife ()
	{
		this.lives++;
		livesText.text = this.lives.ToString ();
	}

	public void AddRapidFire ()
	{
		this.rapidFire = true;
	}

	public bool HasRapidFire ()
	{
		return this.rapidFire;
	}

	public void IncreaseAccuracy ()
	{
		Debug.Log ("increased");
		this.increasedAccuracy = true;
	}

	public bool HasIncreasedAccuracy ()
	{
		return this.increasedAccuracy;
	}

	public float ReceiveDamage (float damage)
	{
		GetComponent<Health> ().health -= damage;
		if (GetComponent<Health> ().health < 1) {
			Die ();
		}
		count = 0;
		return GetComponent<Health> ().health;
	}
}
