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
	public GameObject blood;
	AudioSource audio;
	// Use this for initialization
	void Start ()
	{
		if (transform.Find ("M4A1")) {
			flash = transform.Find ("M4A1").Find ("Muzzle Flash");
		}
		wd = gameObject.GetComponentInChildren<WeaponData> ();
		ammoLeftText = GameObject.Find ("Ammo Left").GetComponent<Text> ();
		audio = GameObject.Find ("Shot Sound").GetComponent<AudioSource> ();

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
			Vector3 shotVector = CalculateShotVector ();

			// check for hit
			if (Physics.Raycast (transform.position, shotVector, out hit, wd.range)) {

				if (hit.transform.GetComponent<Health> ()) {
					// this collider has health, lets damage it
					Debug.Log ("bullet hit " + hit.transform.name);
					hit.transform.GetComponent<Health> ().TakeDamage (wd.damage);
					Instantiate (blood, hit.transform.position, hit.transform.rotation);
				} else {
					//Debug.Log ("shot missed");
				}
			}

			DrawFX (shotVector);
			UpdateNextFire ();
			UpdateAmmoRemaining ();
			playShot ();

			return true;
		}

		return false;
	}

	//http://answers.unity3d.com/comments/467798/view.html
	private Vector3 CalculateShotVector ()
	{
		Vector3 direction = Random.insideUnitCircle * wd.accuracyScaleLimit;
		direction.z = wd.accuracyZ; // circle is at Z units 
		direction = transform.TransformDirection (direction.normalized);
		return direction;
	}

	private void UpdateAmmoRemaining ()
	{
		// update ammo left
		wd.currentAmmo--;

		if (transform.parent && transform.parent.tag == "Player") {
			// update gui
			ammoLeftText.text = wd.currentAmmo.ToString ();
		}
	}

	private void UpdateNextFire ()
	{
		float fireRate = wd.fireRate;

		// check if shooter has rapid fire
		if (transform.parent && transform.parent.GetComponent<PlayerController> ()) {
			if (transform.parent.GetComponent<PlayerController> ().HasRapidFire ()) {
				fireRate /= 2; // shoot twice as fast
			}
		}

		nextFire = Time.time + fireRate;
	}

	private void DrawFX (Vector3 direction)
	{
		//Debug.DrawRay (transform.position, direction, Color.green, 3f);
		if (flash) {
			flash.GetComponent<Animation> ().Play ();
		}

		BulletFX (transform.position, direction);
	}

	void Reload ()
	{
		wd.currentAmmo = wd.maxAmmo;
	}

	private void BulletFX (Vector3 startPos, Vector3 direction)
	{
		Vector3 endPos;

		if (!hit.transform) {
			endPos = transform.position + (direction * wd.range);
		} else {
			endPos = hit.transform.position;
		}


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

	void playShot ()
	{
		if (transform.parent && transform.parent.tag == "Player") {
			audio.Play ();
		}
	}
}
