using UnityEngine;
using System.Collections;

public class EnviromentalDamage : MonoBehaviour {
	
	public Health health;
	public float lastDamageTime;
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
