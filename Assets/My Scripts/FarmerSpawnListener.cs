using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmerSpawnListener : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.transform.GetComponent<PlayerController> () && col.transform.GetComponent<PlayerController> ().GetTeam ().Equals ("ANIMALS")) {
			GameObject.Find ("Game Over Text").GetComponent<Text> ().text = "GAME OVER! THE ANIMALS HAVE TAKEN THE FARM.";
			StartCoroutine (WaitToEndGame ());
		}
	}

	IEnumerator WaitToEndGame ()
	{
		yield return new WaitForSeconds (10);
		EndGame ();
	}

	void EndGame ()
	{
		SceneManager.LoadScene ("GameOver");
	}
}
