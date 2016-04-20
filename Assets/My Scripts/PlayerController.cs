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
	}
}
