using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	// TO DO: All of this stuff should go into a weapon struct or something
	float fireRate = 0.3f;
	float nextFire = 0f;
	RaycastHit hit;
	float range = 300f;
	Transform flash;
	int damage = 25;

	// Use this for initialization
	void Start ()
	{
		flash = transform.Find ("M4A1").Find ("Muzzle Flash");
	}

	/**
	 * attempts to fire the weapon. returns whether or not it fired
     */
	public bool FireWeapon ()
	{
		if (Time.time > nextFire) {
			Debug.DrawRay (transform.position, transform.forward, Color.green, 3f);

			if (Physics.Raycast (transform.position, transform.forward, out hit, range)) {
				flash.GetComponent<Animation> ().Play ();

				if (hit.transform.GetComponent<Health> ()) {
					// this collider has health, lets damage it
					Debug.Log ("bullet hit " + hit.transform.name);
					hit.transform.GetComponent<Health> ().TakeDamage (damage);
				} else {
					Debug.Log ("shot missed");
				}
			}

			nextFire = Time.time + fireRate; // reset fire rate
			return true;
		}

		return false;
	}
}
