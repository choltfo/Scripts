using UnityEngine;
using System.Collections;

public class PenetrableCover : MonoBehaviour {
	
	public float impermeability = 50; // 100 is stainless steel, 10 is balsa wood.
	
	public float tolerance = 100; // 100 is unmarred, 0 is gone.
	
	public ParticleSystem PS;
	
	// Update is called once per frame
	void Update () {
		if (tolerance <= 0) {
			PS.Play();
			Destroy(gameObject);
		}
	}
	
	public BulletHit catchBullet (BulletHit hit) {				// This is so that the object doesn;t hit itself....
		Physics.Raycast(hit.hit.normal, hit.hit.point + (hit.hit.normal*2), out hit.hit, hit.maxRange - ((impermeability/100)*hit.maxRange));
		hit.Damage = hit.Damage - ((impermeability/100)*hit.Damage);
		hit.maxRange = hit.maxRange - ((impermeability/100)*hit.maxRange);
		hit.calculateDamage();
		return hit;
	}
}
