using UnityEngine;
using System.Collections;

public class PickupObjective : Objective {
	void Start () {}
	void Update () {}
	
	public void Interact () {
		if (Complete()) {
			foreach( Transform trans in gameObject.transform) {
				Destroy(trans.gameObject);
			}
			Destroy(gameObject.GetComponent<MeshCollider>());
			Destroy(gameObject.GetComponent<MeshRenderer>());
			Destroy(gameObject.GetComponent<MeshFilter>());
		}
	}
}
