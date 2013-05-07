using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Detonator))]

public class ThrownGrenade : MonoBehaviour {
	public float range;
	public float maxDamage;
	public float delay;
	public float primeTime;
	public bool blown = false;
	
	public void prime (float l_delay) {
		primeTime	= Time.time;
		delay		= l_delay;
	}
	
	void Update () {
		if ((Time.time > (primeTime + delay)) && !blown) {
			GetComponent<Detonator>().Explode();
			//RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, new Vector3(0,0,0));
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
			print (hitColliders.Length);
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
					hit.transform.gameObject.GetComponent<ExplosiveDamage>().explode();	
				}
			}
			print ("BOOM");
			blown = true;
		}
	}
}
