using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Detonator))]


/// <summary>
/// Controls a grenade that has been thrown by the player and is about to blow.
/// </summary>
public class ThrownGrenade : MonoBehaviour {
	/// <summary>
	/// The delay before the grenade blows.
	/// </summary>
	public float delay;
	/// <summary>
	/// The time at which the grenade was primed.
	/// </summary>
	public float primeTime;
	/// <summary>
	/// Has the grenade gone up in smoke yet?
	/// </summary>
	public bool blown = false;
	/// <summary>
	/// Should the grenade explode when it hits something?
	/// </summary>
	public bool detonateOnCollision;
	
	
	
	/// <summary>
	/// Prime the grenade to explode after l_delay seconds.
	/// </summary>
	/// <param name='l_delay'>
	/// The delay before the grenade blows up.
	/// </param>
	public void prime (float l_delay) {
		primeTime	= Time.time;
		delay		= l_delay;
	}
	
	/// <summary>
	/// Detonate this grenade. Right now.
	/// </summary>
	void detonate () {
		blown = true;
		GetComponent<ExplosiveDamage>().explode();
	}
	
	void OnCollisionEnter(Collision collision){
		if (detonateOnCollision) {
			detonate();
		}
	}
	
	void Update () {
		if ((Time.time > (primeTime + delay)) && !blown && !detonateOnCollision) {
			detonate();
		}
	}
}
