using UnityEngine;
using System.Collections.Generic;

public class QuickTimeSpamEventController : MonoBehaviour {
	
	public Controls controls;
	public List<QuickTimeSpamEvent> es = new List<QuickTimeSpamEvent>();
	public int RequiredPresses;
	public float time;
	
	float startTime = 0;
	bool isOn = false;
	int numOfPresses;
	
	public void Queue(QuickTimeSpamEvent e) {
		// THIS IS BROKEN, FIX IT!
		Debug.Log("                      Queing QTE E");
		RequiredPresses = e.requiredPresses;
		time = e.time;
		startTime = Time.time;
		numOfPresses = 0;
		
		isOn = true;
		return;
	}
	
	void Update() {
		if (isOn & (Time.time < startTime + time)) {
			if (Input.GetKeyDown(controls.interact)) numOfPresses++;
			Debug.Log(numOfPresses);
		}
		if (isOn & (Time.time > startTime + time)) {
			Debug.Log("Did not receive needed presses.");
			isOn = false;
		}
		if (isOn & numOfPresses > RequiredPresses) {
			Debug.Log("Got needed presses.");
			isOn = false;
		}
	}
	
	void OnGUI() {
		if (isOn) {
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),
				"Press "+controls.interact.ToString()+" " + (time + startTime - Time.time).ToString());
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,
				(int)(80f*numOfPresses/RequiredPresses)),"");
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),"");
		}
	}
}
