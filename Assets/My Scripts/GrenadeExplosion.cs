using UnityEngine;
using System.Collections;

public class GrenadeExplosion : MonoBehaviour
{
	private Collider[] hitColliders;
	public float blastRadius;
	public float explosionPower;
	public LayerMask explosionLayers;
	private float upwardsModifier = 1f;

	void OnCollisionEnter (Collision col)
	{
		ExplosionWork (col.contacts [0].point);
		Destroy (gameObject);
	}

	void ExplosionWork (Vector3 explosionPoint)
	{
		hitColliders = Physics.OverlapSphere (explosionPoint, blastRadius, explosionLayers);

		foreach (Collider hitCol in hitColliders) {
			if (hitCol.GetComponent<Rigidbody> () != null) {
				hitCol.GetComponent<Rigidbody> ().isKinematic = false;
				hitCol.GetComponent<Rigidbody> ().AddExplosionForce (explosionPower, explosionPoint, blastRadius, upwardsModifier, ForceMode.Impulse);
			}
		}
	}
}
