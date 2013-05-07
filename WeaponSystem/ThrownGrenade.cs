using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Detonator))]

public class ThrownGrenade : MonoBehaviour {
	
	float delay;
	float primeTime;
	
	public void prime (float l_delay) {
		primeTime	= Time.time;
		delay		= l_delay;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
