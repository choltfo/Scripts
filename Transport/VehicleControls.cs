using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]


/// <summary>
/// Vehicle controls.
/// </summary>
public class VehicleControls : MonoBehaviour {
	
	public float distToGround;
	public bool isCarActive = false;
	public float maxSpeed = 1;
	public float maxReverseSpeed = 10;
	//public float accelerationAsPercent  Need to figure this out.
	public float handling = 100;
	public Controls controls;
	
	public float turning = 0;
	public float steering = 0;
	public float accelerator = 0;
	public float speed = 0;
	
	public float gravity = 9.8f;
	
	public float previousVelocity = 0f;
	public float crashMagnitude = 40f;
	public bool DEAD = false;
	
	public bool erectOnEnter = false;

	public float damage = 100f;
	public float maxHealth = 100f;
	
	void OnCollisionStay(Collision C) {
		rigidbody.AddForce(0f,-gravity*rigidbody.mass,0f);
		
		if (erectOnEnter && isCarActive) {
			float z = transform.eulerAngles.z;
			transform.Rotate(0, 0, -z);
		}
		if (Vector3.Dot(transform.up, new Vector3(0,1,0)) > 0.75 && damage >0) {
			if (accelerator >= 0) {
				transform.Rotate(0,turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			} else {
				transform.Rotate(0,-turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			}
			rigidbody.velocity += transform.forward * speed;
		}
	}
	
	void FixedUpdate() {
		if ((rigidbody.velocity.magnitude - previousVelocity) * 10 > Mathf.Abs(crashMagnitude) || 
			(rigidbody.velocity.magnitude - previousVelocity) * 10 < -(Mathf.Abs(crashMagnitude))) {
			if (gameObject.GetComponent<Vehicle>().isActive) {
				gameObject.GetComponent<Vehicle>().player.transform.FindChild("Camera").gameObject.
					GetComponent<Health>().Damage(Mathf.Abs(previousVelocity - rigidbody.velocity.magnitude),
					DamageCause.VehicularMisadventure);
				damage -= previousVelocity - rigidbody.velocity.magnitude;
			}
		}
		previousVelocity = rigidbody.velocity.magnitude;
		
		
		
		speed = 0;
		accelerator = 0;
		steering = 0;
		turning = 0;
		if (isCarActive && damage > 0) {
			steering = Input.GetAxis("Horizontal");
			accelerator = Input.GetAxis("Vertical");
			if (accelerator > 0) {
				speed = Mathf.Lerp(0,maxSpeed,accelerator);
			}	
			if (accelerator < 0) {
				speed = -Mathf.Lerp(0,maxReverseSpeed,Mathf.Abs(accelerator));
			}
			if (steering > 0) {
				turning = Mathf.Lerp(0,handling,steering);
			}	
			if (steering < 0) {
				turning = -Mathf.Lerp(0,handling,Mathf.Abs(steering));
			}
		}
	}
}



