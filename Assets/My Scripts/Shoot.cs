using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
	float nextFire = 0f;
	RaycastHit hit;
	Transform flash;
	WeaponData wd;
	Text ammoLeftText;
	PlayerController pc;
	public GameObject bulletFXPrefab;

	// Use this for initialization
	void Start ()
	{
		flash = transform.Find ("M4A1").Find ("Muzzle Flash");
		wd = gameObject.GetComponent<WeaponData> ();
		ammoLeftText = GameObject.Find ("Ammo Left").GetComponent<Text> ();

	}

	/**
	 * attempts to fire the weapon. returns whether or not it fired
     */
	public bool FireWeapon ()
	{
		if (wd.currentAmmo <= 0) {
			this.Reload ();
			return false;
		}

		if (Time.time > nextFire) {
			Debug.DrawRay (transform.position, transform.forward, Color.green, 3f);
			flash.GetComponent<Animation> ().Play ();

			Vector3 bulletEndPosition = transform.position + (transform.forward * wd.range);
			BulletFX (transform.position, bulletEndPosition);

			if (Physics.Raycast (transform.position, transform.forward, out hit, wd.range)) {

				if (hit.transform.GetComponent<Health> ()) {
					// this collider has health, lets damage it
					Debug.Log ("bullet hit " + hit.transform.name);
					hit.transform.GetComponent<Health> ().TakeDamage (wd.damage);
				} else {
					Debug.Log ("shot missed");
				}
			}

			float fireRate = wd.fireRate;

			// check if shooter has rapid fire
			if (transform.parent && transform.parent.GetComponent<PlayerController> ()) {
				if (transform.parent.GetComponent<PlayerController> ().HasRapidFire ()) {
					fireRate /= 2; // shoot twice as fast
				}
			}

			nextFire = Time.time + fireRate;
			// update ammo left
			wd.currentAmmo--;

			if (transform.parent && transform.parent.tag == "Player") {
				// update gui
				ammoLeftText.text = wd.currentAmmo.ToString ();
			}

			return true;
		}

		return false;
	}

	void Reload ()
	{
		wd.currentAmmo = wd.maxAmmo;
	}

	void BulletFX (Vector3 startPos, Vector3 endPos)
	{
		if (bulletFXPrefab != null) {
			GameObject bulletFX = (GameObject)Instantiate (bulletFXPrefab, startPos, Quaternion.LookRotation (endPos - startPos));
			LineRenderer lr = bulletFX.transform.Find ("LineFX").GetComponent<LineRenderer> ();

			if (lr != null) {
				lr.SetPosition (0, startPos);
				lr.SetPosition (1, endPos);
			} else {
				Debug.LogError ("Line Renderer missing");
			}
		} else {
			Debug.LogError ("bulletFXPrefab missing");
		}

	}
}
