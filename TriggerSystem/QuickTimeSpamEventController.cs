using UnityEngine;
using System.Collections.Generic;

public class QuickTimeSpamEventController : MonoBehaviour {
	
	public SubtitleController subtitleController;
	public Controls controls;
<<<<<<< HEAD
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
=======
	public QuickTimeSpamEvent e;
	
	float startTime = 0;
	bool isOn = false;
	int numOfPresses = 0;
	
	public void Trigger(QuickTimeSpamEvent le) {
		e = le;
		
>>>>>>> Fixed the 'replace all'ed class.
		startTime = Time.time;
		numOfPresses = 0;
		
		isOn = true;
		return;
	}
	
	void Update() {
<<<<<<< HEAD
		if (isOn & (Time.time < startTime + time)) {
			if (Input.GetKeyDown(controls.interact)) numOfPresses++;
			Debug.Log(numOfPresses);
		}
		if (isOn & (Time.time > startTime + time)) {
=======
		if (isOn & (Time.time < startTime + e.time)) {
			if (Input.GetKeyDown(controls.interact)) numOfPresses++;
			Debug.Log(numOfPresses);
		}
		if (isOn & (Time.time > e.time + startTime)) {
>>>>>>> Fixed the 'replace all'ed class.
			Debug.Log("Did not receive needed presses.");
			e.failureResult.Trigger(subtitleController);
			isOn = false;
		}
<<<<<<< HEAD
		if (isOn & numOfPresses > RequiredPresses) {
=======
		if (isOn & numOfPresses > e.requiredPresses) {
>>>>>>> Fixed the 'replace all'ed class.
			Debug.Log("Got needed presses.");
			e.successResult.Trigger(subtitleController);
			isOn = false;
		}
	}
	
	void OnGUI() {
		if (isOn) {
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),
<<<<<<< HEAD
				"Press "+controls.interact.ToString()+" " + (time + startTime - Time.time).ToString());
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,
				(int)(80f*numOfPresses/RequiredPresses)),"");
=======
				"Press "+controls.interact.ToString()+" " + (e.time + startTime - Time.time).ToString());
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,
				(int)(80f*numOfPresses/e.requiredPresses)),"");
>>>>>>> Fixed the 'replace all'ed class.
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),"");
		}
	}
}
