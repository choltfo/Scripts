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
	
	void OnCollisionStay(Collision collision) {
		if (Time.time - lastDamageTime < damageOccurDelay) {
			return;
		}
		if (collision.gameObject.name == "Water Table") {
			print ("You Died");
			health.Damage(100);
			lastDamageTime = Time.time;
			Time.timeScale = 0f;
		}
		if (collision.gameObject.name == "campfire" && collision.gameObject.transform.FindChild("fire") != null) {
			print ("OW!");
			health.Damage(5);
			lastDamageTime = Time.time;
		}
		if (collision.gameObject.name == "fire") {
			print ("OW!");
			health.Damage(5);
			lastDamageTime = Time.time;
		}
	}
}
