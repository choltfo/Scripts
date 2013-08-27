using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

/// <summary>
/// Controls a character, specifically a player.
/// Written on the Unity forums, I believe.
/// </summary>
public class CharacterControls : MonoBehaviour {
 
	public float speed				= 10.0f;
	public float sprintSpeed		= 10.0f;
	public float gravity			= 10.0f;
	public float maxVelocityChange	= 10.0f;
	public bool canJump				= true;
	public float jumpHeight			= 2.0f;
	private bool grounded			= false;
	
	public AudioClip jumpNoise;
	public AudioSource jumpSource;
	
	public bool sprinting			= false;
	
	public Controls controls;
	public Stats stats;
	
	public float sprintingDesatiationRate = 150;
	public float jumpingDesatiationRate = 150;
 
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
	}
 
	void FixedUpdate () {
		
		stats.satiationDeclinePercentage = (sprinting ? sprintingDesatiationRate : 100);
		
	    if (grounded) {
			
			sprinting = Input.GetKey(controls.sprint);
			
			
			
	        // Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(0,0,0);
	        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= (sprinting ? sprintSpeed : speed);
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
 
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				jumpSource.PlayOneShot(jumpNoise);
	        }
	    } else {
			// If not on the ground, we are clearly not sprinting.
			sprinting = false;
		}
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
 
	    grounded = false;
			
	}
 
	void OnCollisionStay () {
	    grounded = true;    
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
		
	
}