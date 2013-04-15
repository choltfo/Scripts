using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public GameObject player;
	CharacterController motor;
	EnemyHealth health;
	public float effectDelay;
	public float recentEffectTime = 0;
	public float TIME;
	public DrugEffect collideEffect;
	public float damage;
	
	void OnCollisionStay(Collision collision) {
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
	}	
	
	void Start() {
		collideEffect.UID = DrugEffect.generateUID();
		motor = (CharacterController)gameObject.GetComponent("CharacterController");
		health = GetComponent<EnemyHealth>();
	}
	
	void Update () {
		TIME = Time.time;
		
		float x = 0;
		float z = 0;
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
		Vector3 Movement = new Vector3(x,0,z);
		//Movement = new Vector3 (Vector3.Angle(transform.position, player.transform.position),0,0)*0.1f;
		//transform.rotation = new Quaternion (Vector3.Angle(transform.position, player.transform.position),0,0,0);
		motor.Move(Movement);
	}
}
