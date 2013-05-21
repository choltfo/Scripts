using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

/// <summary>
/// An objective triggered by traipsing into an area.
/// </summary>
public class AreaObjective : Objective {
	
	public GameObject player;
	
	void OnTriggerEnter (Collider other){
		//Debug.Log ("Object "+ other.gameObject.name + " entered '" + objectiveName+ "'.");
		if (other.gameObject == player && Active) {
			Complete();
		}
	}
}
