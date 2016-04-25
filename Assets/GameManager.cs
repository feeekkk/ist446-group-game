using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private int animalScore = 100;
	private int farmerScore = 50;
	private Text animalText;
	private Text farmerText;

	// Use this for initialization
	void Start ()
	{
		animalText = GameObject.Find ("Animals Score").GetComponent<Text> ();
		farmerText = GameObject.Find ("Farmers Score").GetComponent<Text> ();
	}

	public void IncrementAnimalScore ()
	{
		animalScore--;
		animalText.text = animalScore.ToString ();

		if (animalScore < 0) {
			GameOver ();
		}
	}

	public void IncrementFarmerScore ()
	{
		farmerScore--;
		farmerText.text = farmerScore.ToString ();

		if (farmerScore < 0) {
			GameOver ();
		}
	}

	public void GameOver ()
	{
		
	}
}
