using UnityEngine;
using System.Collections;

/// <summary>
/// The health level of any enemy.
/// </summary>

public class EnemyHealth : Objective {
	/// <summary>
	/// The health level of the enemy.
	/// </summary>
	public float Health = 100;
	
	public float crashThreshhold = 50;
	
	void onCollisionEnter(Collision C) {
		if (C.relativeVelocity.magnitude > crashThreshhold) {
			print(damage(Mathf.Pow ((C.relativeVelocity.magnitude - crashThreshhold)/2,2)));
		}
	}
	
	public float damage (float damage, DamageCause COD = DamageCause.Default) {
		if (Health > damage) {
			Health -= damage;
		} else {
			Health = 0;
			kill (COD);
		}
		return damage;
	}
	
	void kill (DamageCause COD = DamageCause.Default) {
		print (gameObject.name + " suffered a death by " + COD.ToString());
		Destroy (gameObject);
	}
}
