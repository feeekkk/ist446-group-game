using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	Shoot shootScript;
	ShootGrenade shootGrenadeScript;
	private int activeWeaponIndex = 0;

	// Use this for initialization
	void Start ()
	{
		shootScript = gameObject.GetComponentInParent<Shoot> ();
		shootGrenadeScript = gameObject.GetComponentInParent<ShootGrenade> ();
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

		if (Input.GetButton ("Fire2") && shootGrenadeScript) {
			shootGrenadeScript.SpawnGrenade ();
		}
	}
}
