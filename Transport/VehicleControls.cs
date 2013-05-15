using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]


/// <summary>
/// Vehicle controls.
/// </summary>
public class VehicleControls : MonoBehaviour {
	
	public float distToGround;
	public bool active = false;
	public float maxSpeed = 1;
	public float maxReverseSpeed = 10;
	//public float accelerationAsPercent = 	Need to figure this out.
	public float handling = 100;
	public Controls controls;
	
	public float turning = 0;
	public float steering = 0;
	public float accelerator = 0;
	public float speed = 0;
	
	public float gravity = 9.8f;
	public float descentSpeed = 0f;
	
	public float previousVelocity = 0f;
	public float crashMagnitude = 40f;
	public bool DEAD = false;
	
	public float downThreshold = 3;
	
	public float lastTouch = 0;
	
	void OnCollisionStay (Collision collision) {
		if (collision.collider.gameObject.name == "Terrain") {
			lastTouch = Time.fixedTime;
		}
	}
	
	void OnCollisionExit (Collision collision) {
		if (collision.collider.gameObject.name == "Terrain") {
			descentSpeed = 0;
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if (collision.collider.gameObject.name == "Terrain") {
			descentSpeed = 0;
		}
	}
	
	void FixedUpdate() {
		
		if ((rigidbody.velocity.magnitude - previousVelocity) * 10 > Mathf.Abs(crashMagnitude) || 
			(rigidbody.velocity.magnitude - previousVelocity) * 10 < -(Mathf.Abs(crashMagnitude))) {
			if (gameObject.GetComponent<Vehicle>().player != null) {
				gameObject.GetComponent<Vehicle>().player.transform.FindChild("Camera").gameObject.
					GetComponent<Health>().Damage(Mathf.Abs(previousVelocity - rigidbody.velocity.magnitude));
			}
			print ((rigidbody.velocity.magnitude - previousVelocity) * 10);
			DEAD = true;
		}
		//print ((rigidbody.velocity.magnitude - previousVelocity) * 10);
		previousVelocity = rigidbody.velocity.magnitude;
		
		if (!(Terrain.activeTerrain.SampleHeight(transform.position) >
				transform.position.y - downThreshold) && Time.fixedTime > lastTouch) {
			//descentSpeed += gravity * Time.deltaTime * 10;
			//transform.Translate(new Vector3(0, -descentSpeed * Time.deltaTime, 0), Space.World);
			rigidbody.AddForce(0f,-98f*rigidbody.mass,0f);
		}
		
		
		speed = 0;
		accelerator = 0;
		steering = 0;
		turning = 0;
		if (active) {
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
		if ((Terrain.activeTerrain.SampleHeight(transform.position) >
				transform.position.y - downThreshold) && (Vector3.Dot(transform.up, new Vector3(0,1,0)) > 0.75)) {
			if (accelerator > 0) {
				transform.Rotate(0,turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			} else {
				transform.Rotate(0,-turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			}
			rigidbody.velocity += transform.forward * speed;
		}
	}
}



