using UnityEngine;
using System.Collections;

public class EnviromentalDamage : MonoBehaviour {
	
	public Health health;
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "Red Goo") {
			print ("You Died");
			health.Damage(100);
		}
	}
}
