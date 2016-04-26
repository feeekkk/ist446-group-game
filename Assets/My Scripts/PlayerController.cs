using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{

	public enum Teams
	{
		FARMERS,
		ANIMALS}

	;

	public GameObject deadBody;
	public Teams team;
	public int lives = 5;
	Text livesText;
	private bool rapidFire = false;
	private bool increasedAccuracy = false;
	private int count;
	// count of frames since damage last taken
	private GameManager gm;
	public bool isLocalPlayer = false;
	private GameObject[] spawns;

	void Start ()
	{
		if (gameObject.tag == "Player") {
			isLocalPlayer = true;
		}
		livesText = GameObject.Find ("Num Lives").GetComponent<Text> ();
		if (isLocalPlayer) {
			livesText.text = lives.ToString ();
		}

		FirstPersonController fpc = GameObject.FindObjectOfType<FirstPersonController> ();
		if (team == Teams.ANIMALS) {
			fpc.m_RunSpeed = 10;            
		} 
			
		gm = GameObject.Find ("GAME_MANAGER").GetComponent<GameManager> ();

		switch (team) {
		case Teams.ANIMALS:
			spawns = GameObject.FindGameObjectsWithTag ("Animal Spawn");
			break;
		case Teams.FARMERS:
			spawns = GameObject.FindGameObjectsWithTag ("Farmer Spawn");
			break;
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
		Debug.Log ("ded: " + gameObject.name);
		this.lives--;
		if (isLocalPlayer) {
			livesText.text = this.lives.ToString ();
		}
		Quaternion rotation = Quaternion.Euler (90, 0, 0);
		Instantiate (deadBody, transform.position, rotation);

		if (this.lives >= 0) {
			StartCoroutine (WaitToRespawn ());
		}

		SetEnabled (false);
		ClearPowerups ();
		ResetPlayerHealth ();

		if (this.team == Teams.FARMERS) {
			gm.IncrementAnimalScore ();
		} else {
			gm.IncrementFarmerScore ();
		}
	}

	public string GetTeam ()
	{
		return team.ToString ();
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

		if (isLocalPlayer) {
			gameObject.GetComponent<MeshRenderer> ().enabled = enabled;
			gameObject.GetComponent<MeshCollider> ().enabled = enabled;
			gameObject.GetComponent<FirstPersonController> ().enabled = enabled;
		} else {
			// lives in default jawn
			transform.Find ("default").GetComponent<MeshRenderer> ().enabled = enabled;
			gameObject.GetComponent<EnemyController> ().enabled = enabled;
		}
			
		gameObject.GetComponent<Health> ().enabled = enabled;
		gameObject.GetComponentInChildren<Shoot> ().enabled = enabled;
		gameObject.GetComponentInChildren<Shoot> ().SetActiveWeapon (enabled);
	}

	public void Respawn ()
	{
		if (spawns.Length > 1) {
			// pick random animal spawn
			float rand = Random.value;
			if (rand < 0.3) {
				transform.position = spawns [0].GetComponent<Spawner> ().GetSpawnPosition ();
			} else if (rand < 0.6) {
				transform.position = spawns [1].GetComponent<Spawner> ().GetSpawnPosition ();
			} else {
				transform.position = spawns [2].GetComponent<Spawner> ().GetSpawnPosition ();
			}
		} else {
			transform.position = spawns [0].GetComponent<Spawner> ().GetSpawnPosition ();
		}

		SetEnabled (true);
		Debug.Log ("respawning: " + gameObject.name);
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
		if (isLocalPlayer) {
			livesText.text = this.lives.ToString ();
		}
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
