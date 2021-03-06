﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class JoinMenu : MonoBehaviour {

	public InputField ip;
	public GameObject startButton;
	public GameObject startText;
	public GameObject mainButton;
	public GameObject mainText;
	public GameObject ClientManager;
	private bool startHover = false;
	private bool mainHover = false;

	// Use this for initialization
	void Start () {
		startButton.AddComponent<Rigidbody> ();
		mainButton.AddComponent<Rigidbody> ();
		startButton.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		mainButton.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 20)) {
			if (hit.collider != null) {
				if (hit.collider.attachedRigidbody.gameObject == startButton) {
					startHover = true;
					mainHover = false;
				}
				else if (hit.collider.attachedRigidbody.gameObject == mainButton) {
					startHover = false;
					mainHover = true;
				}
			}
		}
		else
		{
			startHover = false;
			mainHover = false;
		}
		if (startHover && Input.GetMouseButtonDown(0)) {
			ClientManager.GetComponent<NetworkManager>().networkAddress = ip.text;
			ClientManager.GetComponent<NetworkManager>().networkPort = 20600;
			ClientManager.GetComponent<NetworkManager>().onlineScene = "Farm Level";
			ClientManager.GetComponent<NetworkManager>().StartClient ();
		} else {

		}
		if (mainHover && Input.GetMouseButtonDown(0)) {
			SceneManager.LoadScene("NetworkSelect");
		} else {

		}
	}
}
