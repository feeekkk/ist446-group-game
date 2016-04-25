using UnityEngine;
using System.Collections;

public class WeaponData : MonoBehaviour
{
	public float currentAmmo = 24f;
	public float maxAmmo = 24f;
	public float fireRate = 0.3f;
	public float damage = 25f;
	public float range = 300f;
	public float accuracyScaleLimit = .5f;
	public float accuracyZ = 1;
}
