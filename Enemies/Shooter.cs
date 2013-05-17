using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

/// <summary>
/// An enemy that shoots back.
/// In theory, it should move untill it has a line of sight to the player, and then shoot.
/// Bit more work needed.
/// </summary>

public class Shooter : Enemy {
	//public float effectDelay;
	//public float recentEffectTime = 0;
	
	//public DrugEffect collideEffect;
	//public float damage;
	
	/*void OnCollisionStay(Collision collision) {
		//Debug.Log ("Hit something, specifically "+collision.gameObject.name);
		if ((Time.time - recentEffectTime) >= effectDelay) {
			Debug.Log ("Time is valid");
			if (collision.gameObject.transform.FindChild("Camera").GetComponent<DrugDosage>() != null) {
				collision.transform.FindChild("Camera").gameObject.GetComponent<DrugDosage>().addEffect(collideEffect.Duplicate());
				collision.transform.FindChild("Camera").gameObject.GetComponent<Health>().Damage(damage);
				Debug.Log ("Applying effect to player");
				recentEffectTime = Time.time;
			}
		}
	}*/	
	
	void Start() {
		//collideEffect.UID = DrugEffect.generateUID();
		motor = gameObject.GetComponent<CharacterController>();
		health = GetComponent<EnemyHealth>();
	}
	
	void Update () {
		
		float x = 0;
		float z = 0;
		RaycastHit hit;
		//If something is between Enemy and Player...
		if (Physics.Linecast(transform.position, player.transform.position, out hit) && hit.transform.gameObject!=player) {
			if (player.transform.position.x > transform.position.x) {
				x = 0.05f;
			}
			if (player.transform.position.x < transform.position.x) {
				x = -0.05f;
			}
			if (player.transform.position.z > transform.position.z) {
				z = 0.05f;
			}
			if (player.transform.position.z < transform.position.z) {
				z = -0.05f;
			}
		}
		Vector3 Movement = new Vector3(x,0,z);
		if (Time.timeScale == 0) {
			Movement = new Vector3(0,0,0);
		}
		motor.Move(Movement);
	}
}
