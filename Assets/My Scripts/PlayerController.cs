using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : NetworkBehaviour
{
	[SerializeField] Material[] animalMaterials;

	public enum Teams
	{
		FARMERS,
		ANIMALS}

	;

	public GameObject deadBody;
	private Teams team = Teams.FARMERS;
	public int lives = 5;
	Text livesText;
	private bool rapidFire = false;
	private bool increasedAccuracy = false;
	private int count;
	// count of frames since damage last taken
	private GameManager gm;

	void Start ()
	{
		livesText = GameObject.Find ("Num Lives").GetComponent<Text> ();
		livesText.text = lives.ToString ();
		int numPlayers = ClientScene.localPlayers.Count;

		FirstPersonController fpc = GameObject.FindObjectOfType<FirstPersonController> ();
		if (team == Teams.ANIMALS) {
			fpc.m_RunSpeed = fpc.m_RunSpeed + 10;            
		} 

		gm = GameObject.Find ("GAME_MANAGER").GetComponent<GameManager> ();
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
		Quaternion rotation = Quaternion.Euler (90, 0, 0);
		Instantiate (deadBody, transform.position, rotation);
		SetEnabled (false);
		ClearPowerups ();
		StartCoroutine (WaitToRespawn ());
		ResetPlayerHealth ();

		if (this.team == Teams.FARMERS) {
			gm.IncrementAnimalScore ();
		} else {
			gm.IncrementFarmerScore ();
		}
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
