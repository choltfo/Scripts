using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]

public class AreaObjective : Objective {
	void Start () {}
	void Update () {}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.GetComponent("CampaignDisplay") != null) {
			CampaignDisplay player = ((CampaignDisplay)other.gameObject.GetComponent("CampaignDisplay"));
			Complete();
		}
	}
}
