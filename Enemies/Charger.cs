using UnityEngine;
using System.Collections;

/// <summary>
/// An enemy that charges at the player when they are within a range.
/// It deals a fixed amount of damage, and adds a drug effect.
/// </summary>

public class Charger : Enemy {
	/// <summary>
	/// The delay between attacks.
	/// </summary>
	public float effectDelay;
	/// <summary>
	/// The range at which the charger sees the player.
	/// </summary>
	public float activationDistance = 5;
	/// <summary>
	/// The timestamp of the most recent effect.
	/// </summary>
	public float recentEffectTime = 0;
	/// <summary>
	/// The drug effect applied with damage.
	/// </summary>
	public DrugEffect collideEffect;
	/// <summary>
	/// The damage applied to player on contact.
	/// </summary>
	public float damage;
		
	void OnCollisionStay(Collision collision) {
		//Debug.Log ("Hit something, specifically "+collision.gameObject.name);
		if ((Time.time - recentEffectTime) >= effectDelay) {
			Debug.Log ("Time is valid");
			if (collision.gameObject.transform.FindChild("Camera") != null) {
				if (collision.gameObject.transform.FindChild("Camera").GetComponent<DrugDosage>() != null) {
					collision.transform.FindChild("Camera").gameObject.GetComponent<DrugDosage>().addEffect(collideEffect.Duplicate());
					collision.transform.FindChild("Camera").gameObject.GetComponent<Health>().Damage(damage);
					Debug.Log ("Applying effect to player");
					recentEffectTime = Time.time;
				}
			}
		}
	}
	
	void Start() {
		recentEffectTime = -effectDelay;
		collideEffect.UID = DrugEffect.generateUID();
		motor = (CharacterController)gameObject.GetComponent("CharacterController");
		health = GetComponent<EnemyHealth>();
	}
	
	void Update () {
		
		float x = 0;
		float z = 0;
		float y = 0;
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
		if (!motor.isGrounded) {
			y = -0.1f;
		}
		Vector3 Movement = new Vector3(x,y,z);
		//Movement = new Vector3 (Vector3.Angle(transform.position, player.transform.position),0,0)*0.1f;
		//transform.rotation = new Quaternion (Vector3.Angle(transform.position, player.transform.position),0,0,0);
		if (Time.timeScale == 0) {
			Movement = new Vector3(0,0,0);
		}
		if (Vector3.Distance(transform.position, player.transform.position) > activationDistance) {
			Movement = new Vector3(0,0,0);
		}
		//print(Vector3.Distance(transform.position, player.transform.position));
		motor.Move(Movement);
	}
}
