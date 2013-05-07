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
	
	public bool IsGrounded;
	private float hoverError;
	public float IsGroundedMargin;
	public float RayHoverHeight = 4.0f;
	 
	void FixedUpdate() {
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, -Vector3.up);
		 
		if (Physics.Raycast(downRay, out hit))
		hoverError = RayHoverHeight - hit.distance;
		 
		if (hoverError > IsGroundedMargin)
		IsGrounded = false;
		 
		if (hoverError < IsGroundedMargin)
		IsGrounded = true;
	}
	
	public void Update () {
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
				transform.position.y - 5) && (Vector3.Dot(transform.up, new Vector3(0,1,0)) > 0.75)) {
			transform.Rotate(0,turning*(rigidbody.velocity.magnitude/20)*(handling/100),0);
			rigidbody.velocity += transform.forward * speed;
		}
	}
}



