using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public int lives = 5;
	Text livesText;
	private bool rapidFire = false;
	private bool increasedAccuracy = false;

	void Start ()
	{
		livesText = GameObject.Find ("Num Lives").GetComponent<Text> ();
		livesText.text = lives.ToString ();
	}

	public void Die ()
	{
		this.lives--;
		livesText.text = this.lives.ToString ();
		SetEnabled (false);
		StartCoroutine (WaitToRespawn ());
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
}
