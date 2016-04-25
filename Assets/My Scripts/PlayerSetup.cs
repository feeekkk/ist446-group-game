using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSetup : NetworkBehaviour
{

	[SerializeField] Behaviour[] componentsDisabled;

	// Use this for initialization
	void Start ()
	{
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsDisabled.Length; i++) {
//				componentsDisabled [i].enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
