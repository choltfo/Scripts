using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class AreaObjective : Objective {
	
	public GameObject player;
	
	void Start () {}
	void Update () {}
	void OnTriggerEnter (Collider other){
		//Debug.Log ("Object "+ other.gameObject.name + " entered '" + objectiveName+ "'.");
		if (other.gameObject == player && Active) {
			Complete();
		}
	}
}
