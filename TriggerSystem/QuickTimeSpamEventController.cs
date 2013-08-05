using UnityEngine;
using System.Collections.Generic;

public class QuickTimeSpamEventController : MonoBehaviour {
	
	public SubtitleController subtitleController;
	public Controls controls;
	public List<QuickTimeSpamEvent> es = new List<QuickTimeSpamEvent>();
	public QuickTimeSpamEvent e;
	public int RequiredPresses;
	public float time;
	
	float startTime = 0;
	bool isOn = false;
	int numOfPresses;
	
	public void Queue(QuickTimeSpamEvent le) {
		// THIS IS BROKEN, FIX IT!
		Debug.Log("                      Queing QTE E");
		e = le;
		startTime = Time.time;
		isOn = true;
		numOfPresses = 0;
	}
	
	void Update() {
		if (isOn & (Time.time < startTime + e.time)) {
			if (Input.GetKeyDown(controls.interact)) numOfPresses++;
			Debug.Log(numOfPresses);
		}
		if (isOn & (Time.time > e.time + startTime)) {
			Debug.Log("Did not receive needed presses.");
			e.failureResult.Trigger(subtitleController);
			isOn = false;
		}
		if (isOn & numOfPresses > e.requiredPresses) {
			Debug.Log("Got needed presses.");
			e.successResult.Trigger(subtitleController);
			isOn = false;
		}
	}
	
	void OnGUI() {
		if (isOn) {
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),
				"Press "+controls.interact.ToString()+" " + (e.time + startTime - Time.time).ToString());
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,
				(int)(80f*numOfPresses/e.requiredPresses)),"");
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),"");
		}
	}
}
