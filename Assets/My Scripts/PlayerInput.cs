using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	Shoot shootScript;
	ShootGrenade shootGrenadeScript;

	// Use this for initialization
	void Start ()
	{
		shootScript = gameObject.GetComponent<Shoot> ();
		if (!shootScript) {
			throw new UnityException ("No shoot script found in this game object");
		}

		shootGrenadeScript = gameObject.GetComponent<ShootGrenade> ();
		if (!shootGrenadeScript) {
			Debug.LogError ("No shoot grenade script found");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForInput ();
	}

	void CheckForInput ()
	{
		if (Input.GetButton ("Fire1")) {
			shootScript.FireWeapon ();
		}

		if (Input.GetButton ("Fire2") && shootGrenadeScript) {
			shootGrenadeScript.SpawnGrenade ();
		}
	}
}
