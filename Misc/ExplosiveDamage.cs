using UnityEngine;
using System.Collections;

public class ExplosiveDamage : MonoBehaviour {
	
	public float range		= 5;
	public float maxDamage	= 100;
	public bool blown = false;

	public void explode() {
		GetComponent<Detonator>().Explode();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
		print (hitColliders.Length);
		blown = true;
		foreach (Collider hit in hitColliders) {
			print (hit.transform.gameObject.name);
			float damage = Mathf.Lerp(0, maxDamage, Vector3.Distance(hit.transform.position, transform.position)
				/range);
			if (hit.transform.FindChild("Camera") != null) {
				if (hit.transform.FindChild("Camera").gameObject.GetComponent<Health>() != null) {
					hit.transform.FindChild("Camera").gameObject.GetComponent<Health>().Damage(damage);	
				}
			}
			if (hit.gameObject.name == "RaycastLayer") {
				if (hit.transform.parent.gameObject.GetComponent<EnemyHealth>() != null) {
					hit.transform.parent.gameObject.GetComponent<EnemyHealth>().Health -= (int)damage;	
				}
			}
			if (hit.transform.gameObject.GetComponent<EnemyHealth>() != null) {
				hit.transform.gameObject.GetComponent<EnemyHealth>().Health -= (int)damage;	
			}
			if (hit.transform.gameObject.GetComponent<Detonator>() != null) {
				hit.transform.gameObject.GetComponent<Detonator>().Explode();
			}
			if (hit.transform.gameObject.GetComponent<ExplosiveDamage>() != null) {
				if (!hit.transform.gameObject.GetComponent<ExplosiveDamage>().blown) {
					hit.transform.gameObject.GetComponent<ExplosiveDamage>().explode();
				}
			}
		}
		print ("BOOM");
	}
}
