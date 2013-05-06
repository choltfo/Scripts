using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]


/// <summary>
/// Vehicle controls.
/// </summary>
public class VehicleControls : MonoBehaviour {
	
	public bool active = false;
	public float maxSpeed = 30;
	public float maxReverseSpeed = 10;
	//public float accelerationAsPercent = 	Need to figure this out.
	public float handling = 30;
	public Controls controls;
	
	float turning = 0;
	float steering = 0;
	float accelerator = 0;
	float speed = 0;
	
	public void update () {
		speed = 0;
		accelerator = 0;
		steering = 0;
		turning = 0;
		if (active) {
			turning = Input.getAxis("Horizontal");
			accelerator = Input.getAxis("Vertical");
			if (accelerator > 0) {
				speed = mathf.lerp(0,maxSpeed,accelerator);
			}	
			if (accelerator < 0) {
				speed = -mathf.lerp(0,maxReverseSpeed,mathf.abs(accelerator));
			}
			if (accelerator > 0) {
				turning = mathf.lerp(0,handling,steering);
			}	
			if (accelerator < 0) {
				turning = -mathf.lerp(0,handling,mathf.abs(steering));
			}
			
			
			
		}
		transform.rotate(0,turning,0);
		rigidbody.velocity += transform.forward * speed;
	}
}



