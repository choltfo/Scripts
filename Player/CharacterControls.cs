using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

/// <summary>
/// Controls a character, specifically a player.
/// Written on the Unity forums, I believe.
/// Heavily modified to add crouching, add sprinting, and remove wall climbing.
/// </summary>
public class CharacterControls : MonoBehaviour {
 
	public float speed					= 10.0f;
	public float sprintSpeed			= 10.0f;
	public float crouchSpeed			= 5f;
	public float gravity				= 10.0f;
	public float maxVelocityChange		= 10.0f;
	public bool canJump					= true;
	public float jumpHeight				= 2.0f;
	private bool grounded				= false;
	public float groundingSafetyMargin	= 0.1f;

	public AudioClip walkNoise;
	public AudioClip jumpNoise;
	public AudioSource audioSource;


	public bool sprinting			= false;
	public bool crouching			= false;
	public float ccHeight			= 1.25f;
	public float ccRadius			= 0.25f;
	public float crouchTime			= 1;
	public Vector3 cameraCrouchPos;
	public Vector3 cameraNormalPos;
	//float lastCrouchSwitch			= 0;
	
	public Controls controls;
	public Stats stats;
	
	public float sprintingDesatiationRate = 150;
	public float jumpingDesatiationRate = 150;
 
	CapsuleCollider cc;
	
	void Awake () {
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
		cc = GetComponent<CapsuleCollider>();
		//Time.timeScale = 0.25f;
	}
 
	void FixedUpdate () {
		
		stats.satiationDeclinePercentage = (sprinting ? sprintingDesatiationRate : 100);
		
	    if (grounded) {
			
			crouching = Input.GetKey(controls.crouch);
			sprinting = crouching ? false : Input.GetKey(controls.sprint);
			
			//if (Input.GetKeyDown(controls.crouch) || Input.GetKeyUp(controls.crouch)) {
			//	lastCrouchSwitch = Time.time;
			//}
			
			if (crouching) {
				//cc.height = Mathf.Lerp (ccHeight, ccRadius*2f, (Time.time-lastCrouchSwitch)/crouchTime);
				//cc.radius = Mathf.Lerp (ccRadius, ccHeight/2, (Time.time-lastCrouchSwitch)/crouchTime);
				cc.direction = 2;	// Z axis
			} else {
				//cc.height = Mathf.Lerp (ccHeight, ccRadius*2f, (Time.time-lastCrouchSwitch)/crouchTime);
				//cc.radius = Mathf.Lerp (ccHeight/2, ccRadius, (Time.time-lastCrouchSwitch)/crouchTime);
				//cc.height = Mathf.Lerp (ccRadius*2f, ccHeight, (Time.time-lastCrouchSwitch)/crouchTime);
				cc.direction = 1;	// Y axis
			}
			
			
	        // Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(0,0,0);
	        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= (crouching ? crouchSpeed : (sprinting ? sprintSpeed : speed));
			
			// Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			//audioSource.loop = true;
			audioSource.clip = walkNoise;
			if (!audioSource.isPlaying && targetVelocity != Vector3.zero) {
				audioSource.Play();
			}

	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				audioSource.PlayOneShot(jumpNoise);
	        }
	    } else {
			// If not on the ground, we are clearly not sprinting.
			sprinting = false;
		}
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
 
	    grounded = false;
			
	}
 
	void OnCollisionStay (Collision C) {
	    if (C.contacts[0].point.y - groundingSafetyMargin < (transform.position.y - (cc.direction == 1 ? cc.height/2 : cc.radius)) ) {
			grounded = true;
		}
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
		
	
}