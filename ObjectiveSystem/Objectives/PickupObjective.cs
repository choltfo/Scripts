using UnityEngine;
using System.Collections;

public class PickupObjective : Objective {
	void Start () {}
	void Update () {}
	
	public void Interact () {
		foreach( Transform trans in gameObject.transform) {
			Destroy(trans.gameObject);
		}
		if (!complete) {
			complete = true;
		}
	}
}
