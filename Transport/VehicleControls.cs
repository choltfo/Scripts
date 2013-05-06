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
			turning = Input.GetAxis("Horizontal");
			accelerator = Input.GetAxis("Vertical");
			if (accelerator > 0) {
				speed = Mathf.Lerp(0,maxSpeed,accelerator);
			}	
			if (accelerator < 0) {
				speed = -Mathf.Lerp(0,maxReverseSpeed,Mathf.Abs(accelerator));
			}
			if (accelerator > 0) {
				turning = Mathf.Lerp(0,handling,steering);
			}	
			if (accelerator < 0) {
				turning = -Mathf.Lerp(0,handling,Mathf.Abs(steering));
			}
			
			
			
		}
		transform.Rotate(0,turning,0);
		rigidbody.velocity += transform.forward * speed;
	}
}



