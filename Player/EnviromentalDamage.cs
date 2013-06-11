using UnityEngine;
using System.Collections;

/// <summary>
/// Take damage from bumping into otherwise benign things.
/// </summary>
public class EnviromentalDamage : MonoBehaviour {
	/// <summary>
	/// The health to affect.
	/// </summary>
	public Health health;
	/// <summary>
	/// The time at which the player last took damage.
	/// </summary>
	public float lastDamageTime;
	/// <summary>
	/// The time between getting hurt.
	/// </summary>
	public float damageOccurDelay = 5;
	
	public float crashThreshold = 15;
	
	void OnCollisionEnter(Collision C) {
		print (Mathf.Pow((C.relativeVelocity.magnitude-crashThreshold)/2,2));
		if (C.relativeVelocity.magnitude > crashThreshold) {
			health.Damage(Mathf.Pow((C.relativeVelocity.magnitude-crashThreshold)/2,2), DamageCause.KineticInjury);
		}
	}
	
	void OnCollisionStay(Collision collision) {
		if (Time.time - lastDamageTime < damageOccurDelay) {
			return;
		}
		if (collision.gameObject.name == "Water Table") {
			health.Damage(100, DamageCause.Radiation);
			lastDamageTime = Time.time;
			Time.timeScale = 0f;
		}
		if (collision.gameObject.name == "campfire" && collision.gameObject.transform.FindChild("fire") != null) {
			print ("OW!");
			health.Damage(5, DamageCause.Fire);
			lastDamageTime = Time.time;
		}
		if (collision.gameObject.name == "fire") {
			print ("OW!");
			health.Damage(5, DamageCause.Fire);
			lastDamageTime = Time.time;
		}
	}
}
