using UnityEngine;
using System.Collections;

public class MeleeWeapon {
	
	// GameObject related variables.
	public GameObject instantiableObject;	// The thing you swing. Must contain a joint.
	public GameObject instantiablePickup;	// The thing to grab before swinging.
	
	// Animation related variables
	public Quaternion holdRotation;			// How to hold the weapon.
	public Vector3 holdPosition;			// Where to hold the weapon.
	public Quaternion pathEndRotation;		// How to hold the weapon when cocking it back.
	public Vector3 pathEnd;					// Where to hold the weapon when cocking it back.
	public Quaternion pathStartRotation;	// How to hold the weapon when it fails to make contact with anything.
	public Vector3 pathStart;				// Where to hold the weapon when it fails to hit anything.
	
	// Gameplay related variables.
	public float damage		 = 50f;			// How much damage this weapon does with a direct blow.
	public float swingTime	 = 1f;			// How long (in seconds) it takes to bash something with this.
	public string name		 = "Incendiary chainsaw katana";// No explanation necessary.
	public float curDurab	 = 100f;		// The current durability of this weapon
	public float capDurab	 = 100f;		// The maximum durability of a melee weapon.
	public float decDurab	 = 0.05f;		// How much to decrement the durability (curDurab) by.
	
	// Behind the scenes variables.
	public bool isValid		 = false;		// Set to true to make this weapon work!
	[HideInInspector]
	public bool isOut		 = false;		// Whether the weapon is weilded. Leave false.
	
	GameObject mainObject;
	GameObject hand;
	
	
	public bool create (GameObject Body) {
		if (!isValid) return false;			// Invalid objects do not exist.
		
		
		
		return true;
	}
	
	public bool withdraw (GameObject Head) {
		if (!isValid) return false;			// Invalid objects do not exist.
		
		
		
		return true;
	}
}
