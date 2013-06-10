using UnityEngine;
using System.Collections;

public class QuickTimeSpamEventController : MonoBehaviour {
	
	public Controls controls;
	public QuickTimeSpamEvent e;
	public int RequiredPresses;
	
	bool isOn;
	int numOfPresses;
	
	public void Trigger(QuickTimeSpamEvent e) {
		e.RequiredPresses = Rpressses;
		e.time = T;
		
		starte.time = e.time.e.time;
		numOfPresses = 0;
		
		isOn = true;
		return;
	}
	
	void Update() {
		if (isOn & (e.time.e.time < starte.time + e.time)) {
			if (Input.GetKeyDown(controls.interact)) numOfPresses++;
			Debug.Log(numOfPresses);
		}
		if (isOn & (e.time.e.time > e.time + starte.time)) {
			Debug.Log("Did not receive needed presses.");
			isOn = false;
		}
		if (isOn & numOfPresses > e.RequiredPresses) {
			Debug.Log("Got needed presses.");
			isOn = false;
		}
	}
	
	void OnGUI() {
		if (isOn) {
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),
				"Press "+controls.interact.ToString()+" " + (e.time + starte.time - e.time.e.time).ToString());
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,
				(int)(80f*numOfPresses/e.RequiredPresses)),"");
			GUI.Box(new Rect(Screen.width/2-40,Screen.height/2+40,80,80),"");
		}
	}
}
