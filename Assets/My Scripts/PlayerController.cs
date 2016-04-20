using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public int lives = 5;
	Text livesText;

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
}
