using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	float nextFire = 0f;
	RaycastHit hit;
	Transform flash;
	WeaponData wd;

	// Use this for initialization
	void Start ()
	{
		flash = transform.Find ("M4A1").Find ("Muzzle Flash");
		wd = gameObject.GetComponent<WeaponData> ();
	}

	/**
	 * attempts to fire the weapon. returns whether or not it fired
     */
	public bool FireWeapon ()
	{
		if (Time.time > nextFire) {
			Debug.DrawRay (transform.position, transform.forward, Color.green, 3f);
			flash.GetComponent<Animation> ().Play ();

			if (Physics.Raycast (transform.position, transform.forward, out hit, wd.range)) {

				if (hit.transform.GetComponent<Health> ()) {
					// this collider has health, lets damage it
					Debug.Log ("bullet hit " + hit.transform.name);
					hit.transform.GetComponent<Health> ().TakeDamage (wd.damage);
				} else {
					Debug.Log ("shot missed");
				}
			}

			nextFire = Time.time + wd.fireRate; // reset fire rate
			return true;
		}

		return false;
	}
}
