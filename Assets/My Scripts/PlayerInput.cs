using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	Shoot shootScript;
	ShootGrenade shootGrenadeScript;
	Camera c;

	// Use this for initialization
	void Start ()
	{
		shootScript = gameObject.GetComponentInChildren<Shoot> ();
		c = transform.GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForInput ();
	}

	void CheckForInput ()
	{
		if (Input.GetButtonDown ("Switch Weapon")) {
			shootScript.switchActiveWeapon ();
		}

		if (Input.GetButton ("Fire1")) {
			shootScript.FireWeapon ();
		}

		if (Input.GetButton ("Fire2")) {
			c.fieldOfView = 30;
		} else {
			c.fieldOfView = 60; // reset it
		}

		if (Input.GetButtonDown ("Reload")) {
			shootScript.Reload ();
		}
	}
}
