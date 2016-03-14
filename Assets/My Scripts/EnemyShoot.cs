using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
	Shoot shootScript;
	public int burst = 3;
	// shots per burst

	// Use this for initialization
	void Start ()
	{
		shootScript = gameObject.GetComponent<Shoot> ();
		if (!shootScript) {
			throw new UnityException ("No shoot script found in this game object");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ShootBurst ()
	{
		
		int count = 0;

		//while (count < 3) {
		bool shot = shootScript.FireWeapon ();
		if (shot) {
			count++;
		}
		//}

	}
}
