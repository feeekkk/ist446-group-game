using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private int animalScore = 50;
	private int farmerScore = 50;
	private Text animalText;
	private Text farmerText;
	public GameObject player;

	// Use this for initialization
	void Start ()
	{
		animalText = GameObject.Find ("Animals Score").GetComponent<Text> ();
		farmerText = GameObject.Find ("Farmers Score").GetComponent<Text> ();

		// spawn player in farmer spawn
		Vector3 spawn = GameObject.Find ("Farmer Spawn").GetComponent<Spawner> ().GetSpawnPosition ();
		Instantiate (player, spawn, Quaternion.identity);
	}

	public void DecrementAnimalScore ()
	{
		animalScore--;
		animalText.text = animalScore.ToString ();

		if (animalScore < 0) {
			GameOver ();
		}
	}

	public void DecrementFarmerScore ()
	{
		farmerScore--;
		farmerText.text = farmerScore.ToString ();

		if (farmerScore < 0) {
			GameOver ();
		}
	}

	public void GameOver ()
	{
		SceneManager.LoadScene ("GameOver");
	}
}
