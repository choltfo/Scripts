using UnityEngine;
using System.Collections;

public class TimeObjective : Objective {
	
	public float activationTime = 0f;
	public float delay = 10f;
	
	bool pastActive = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (pastActive != Active && Active) {
			activationTime = Time.time;
			pastActive = true;
		}
		
		if (Active) {
			if (delay + activationTime > Time.time) {
				Complete();
			}
		}
	}
}
