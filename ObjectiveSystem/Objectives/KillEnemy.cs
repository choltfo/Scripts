using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class KillEnemy : Objective {
	void Start () {}
	void Update () {}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.GetComponent("CampaignDisplay") != null) {
			Complete();
		}
	}
}
