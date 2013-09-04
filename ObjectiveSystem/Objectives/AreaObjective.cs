using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Collider))]

/// <summary>
/// An objective triggered by traipsing into an area.
/// </summary>
public class AreaObjective : Objective {
	
	public List<GameObject> players;
	
	void OnTriggerEnter (Collider other){
		Debug.Log ("Object "+ other.gameObject.name + " entered '" + objectiveName+ "'.");
		if (players.Contains(other.gameObject) && Active) {
			Complete();
		}
		if (other.gameObject.GetComponent<Vehicle>() != null) {
			if (players.Contains(other.gameObject.GetComponent<Vehicle>().player)) {
				Complete();
			}
		}
	}
}
