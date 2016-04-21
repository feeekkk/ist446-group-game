using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalIp : MonoBehaviour {

	public InputField input;

	// Use this for initialization
	void Start () {
		input.text = Network.player.ipAddress;
		input.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
