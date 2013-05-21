using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Detonator))]


/// <summary>
/// Controls a grenade that has been thrown by the player and is about to blow.
/// </summary>
public class ThrownGrenade : MonoBehaviour {
	/// <summary>
	/// The blast radius of the grenade.
	/// </summary>
	public float range;
	/// <summary>
	/// The damage sustained by injurable entities at ground zero.
	/// </summary>
	public float maxDamage;
	/// <summary>
	/// The delay before the grenade blows.
	/// </summary>
	public float delay;
	/// <summary>
	/// The time at which the grenade was primed.
	/// </summary>
	public float primeTime;
	/// <summary>
	/// Has the grenade gone up in smoke yet?
	/// </summary>
	public bool blown = false;
	/// <summary>
	/// Should the grenade explode when it hits something?
	/// </summary>
	public bool detonateOnCollision;
	
	/// <summary>
	/// Prime the grenade to explode after l_delay seconds.
	/// </summary>
	/// <param name='l_delay'>
	/// The delay before the grenade blows up.
	/// </param>
	public void prime (float l_delay) {
		primeTime	= Time.time;
		delay		= l_delay;
	}
	
	/// <summary>
	/// Detonate this grenade. Right now.
	/// </summary>
	void detonate () {
		//RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, new Vector3(0,0,0));
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
		//print (hitColliders.Length);
		foreach (Collider hit in hitColliders) {
			//print (hit.transform.gameObject.name);
			float damage = Mathf.Lerp(0, maxDamage, Vector3.Distance(hit.transform.position, transform.position)/range);
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
			if (hit.transform.gameObject.GetComponent<ExplosiveDamage>() != null) {
				if (!hit.transform.gameObject.GetComponent<ExplosiveDamage>().blown) {
					hit.transform.gameObject.GetComponent<ExplosiveDamage>().explode();
				}
			}
		}
		//print ("BOOM");
		blown = true;
		GetComponent<Detonator>().Explode();
	}
	
	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name='collision'>
	/// Collision.
	/// </param>
	void OnCollisionEnter(Collision collision){
		if (detonateOnCollision) {
			detonate();
		}
	}
	
	void Update () {
		if ((Time.time > (primeTime + delay)) && !blown && !detonateOnCollision) {
			detonate();
			Destroy (this);
		}
	}
}
