using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
	float nextFire = 0f;
	RaycastHit hit;
	Transform flash;
	WeaponData[] wds;
	Text ammoLeftText;
	Text maxAmmoText;
	PlayerController pc;
	public GameObject bulletFXPrefab;
	public GameObject blood;
	AudioSource audio;
	public int activeWeaponIndex = 0;
	Text reloadText;
	bool allowShooting;
	AudioSource reloadSound;


	// Use this for initialization
	void Start ()
	{
		flash = transform.Find ("Muzzle Flash");
		wds = gameObject.GetComponentsInChildren<WeaponData> ();
		ammoLeftText = GameObject.Find ("Ammo Left").GetComponent<Text> ();
		maxAmmoText = GameObject.Find ("Ammo Full Clip").GetComponent<Text> ();
		audio = GameObject.Find ("Shot Sound").GetComponent<AudioSource> ();
		// disable all weapons but the first
		for (int i = 1; i < wds.Length; i++) {
			Debug.Log ("disabled" + wds [i].name);
			wds [i].gameObject.SetActive (false);
		}
		reloadSound = GameObject.Find ("Reload Sound").GetComponent<AudioSource> ();
		reloadText = GameObject.Find ("Reload Text").GetComponent<Text> ();
		allowShooting = true;
	}

	/**
	 * attempts to fire the weapon. returns whether or not it fired
     */
	public bool FireWeapon ()
	{
		if (wds [activeWeaponIndex].currentAmmo <= 0) {
			this.Reload ();
			return false;
		}

		if (Time.time > nextFire && allowShooting == true) {
			Vector3 shotVector = CalculateShotVector ();

			// check for hit
			if (Physics.Raycast (transform.position, shotVector, out hit, wds [activeWeaponIndex].range)) {

				if (hit.transform.GetComponent<Health> ()) {
					// this collider has health, lets damage it
					Debug.Log ("bullet hit " + hit.transform.name);
					hit.transform.GetComponent<Health> ().TakeDamage (wds [activeWeaponIndex].damage);
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
		Vector3 direction = Random.insideUnitCircle * wds [activeWeaponIndex].accuracyScaleLimit;
		direction.z = wds [activeWeaponIndex].accuracyZ; // circle is at Z units 
		direction = transform.TransformDirection (direction.normalized);
		return direction;
	}

	private void UpdateAmmoRemaining ()
	{
		// update ammo left
		wds [activeWeaponIndex].currentAmmo--;

		if (transform.parent && transform.parent.tag == "Player") {
			UpdateGui ();
		}
	}

	private void UpdateGui ()
	{
		ammoLeftText.text = wds [activeWeaponIndex].currentAmmo.ToString ();
		maxAmmoText.text = wds [activeWeaponIndex].maxAmmo.ToString ();
	}

	private void UpdateNextFire ()
	{
		float fireRate = wds [activeWeaponIndex].fireRate;

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

	public void Reload ()
	{
		allowShooting = false;
		playReload ();
		reloadText.text = "RELOADING...".ToString ();
		Invoke ("reloadTextChange", 3.0f);
		wds [activeWeaponIndex].currentAmmo = wds [activeWeaponIndex].maxAmmo;
		UpdateGui ();
	}

	void reloadTextChange ()
	{
		reloadText.text = "".ToString ();
		allowShooting = true;
	}


	void playReload ()
	{
		if (transform.parent && transform.parent.tag == "Player") {
			reloadSound.Play ();
		}
	}

	private void BulletFX (Vector3 startPos, Vector3 direction)
	{
		Vector3 endPos;

		if (!hit.transform) {
			endPos = transform.position + (direction * wds [activeWeaponIndex].range);
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

	public void switchActiveWeapon ()
	{
		wds [activeWeaponIndex].gameObject.SetActive (false);

		if (activeWeaponIndex == wds.Length - 1) {
			activeWeaponIndex = 0;
		} else {
			activeWeaponIndex++;
		}

		wds [activeWeaponIndex].gameObject.SetActive (true);

		UpdateGui ();
	}
}
