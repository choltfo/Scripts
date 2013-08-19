using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (ConstantForce))]

/// <summary>
/// Vehicle controls.
/// </summary>

[AddComponentMenu("TTP/Vehicles/Controls")]
public class VehicleControls : MonoBehaviour {
	
	public float distToGround;
	public bool isCarActive = false;
	public float maxSpeed = 15;
	public float maxReverseSpeed = 10;
	public float maxAccel;
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
	
	public vehicleType type;
	
	void OnCollisionStay(Collision C) {
		
		if (type == vehicleType.Water && C.gameObject.layer != LayerMask.NameToLayer("Water")) {
			return;
		}
		
		if (type == vehicleType.Land && C.gameObject.layer == LayerMask.NameToLayer("Water")) {
			return;
		}
		float locVelZ = transform.InverseTransformDirection(rigidbody.velocity).z;
		if (Vector3.Dot(transform.up, new Vector3(0,1,0)) > 0.75 &&
			locVelZ < maxSpeed) {
			constantForce.relativeForce = new Vector3(0,0,speed);
			constantForce.relativeTorque = new Vector3(0,(turning),0);
		} else {
			constantForce.relativeForce = new Vector3(0,0,0);
			constantForce.relativeTorque = new Vector3(0,0,0);
		}
		
		/*rigidbody.AddForce(0f,-gravity*rigidbody.mass,0f);
		
		
		if (Vector3.Dot(transform.up, new Vector3(0,1,0)) > 0.75 && damage >0) {
			if (accelerator >= 0) {
				transform.Rotate(0,turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			} else {
				transform.Rotate(0,-turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			}
			rigidbody.velocity += transform.forward * speed;
		}*/
	}
	
	void OnCollisionExit (Collision C) {
		constantForce.relativeForce = new Vector3(0,0,0);
		constantForce.relativeTorque = new Vector3(0,0,0);
	}
	
	void FixedUpdate() {
		
		
		
		if ((rigidbody.velocity.magnitude - previousVelocity) * 10 > Mathf.Abs(crashMagnitude) || 
			(rigidbody.velocity.magnitude - previousVelocity) * 10 < -(Mathf.Abs(crashMagnitude))) {
			if (gameObject.GetComponent<Vehicle>().isActive && type != vehicleType.Water) {
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
				speed = Mathf.Lerp(0, maxAccel, accelerator);
			}	
			if (accelerator < 0) {
				speed = -Mathf.Lerp(0, maxAccel, Mathf.Abs(accelerator));
			}
			if (steering > 0) {
				turning = Mathf.Lerp(0, handling, steering);
			}	
			if (steering < 0) {
				turning = -Mathf.Lerp(0, handling, Mathf.Abs(steering));
			}
		}
	}
}

public enum vehicleType {
	Land,
	Water
}

