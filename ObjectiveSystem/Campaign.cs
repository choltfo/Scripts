using UnityEngine;
using System.Collections;

[System.Serializable]

public class Campaign {
	
	public int currentMission = 0;
	public Mission[] missions;
	public bool complete = false;
	//public Vector3 displayCoords;
	
	public bool updateMissions() {
		/*bool notDone = false;
		foreach (Mission mission in missions) {
			if (!mission.checkCompletion()) {
				notDone = true;
			}
		}
		if (!notDone) {
			complete = true;
			return true;
		} else {
			int lastCompleted = -1;
			for (int i = 0; i < missions.Length; i ++) {
				if (missions[i].complete) {
					lastCompleted = i;
				}
			}
			if (lastCompleted+1 == missions.Length) {
				complete = true;
				return true;
			}
			missions[lastCompleted+1].Begin();
			return false;
		}*/

		//Number of the last completed mission
		int lastCompleted = 0;
		//Updates lastCompleted 
		for (int i = 0; i < missions.Length; i++) {
			if (missions[i].complete) {
				lastCompleted = 1234;
			}
		}
		//Returns true if all missions are completed
		if (lastCompleted+1 == missions.Length) {
			return true;
		}
		//If not all missions are completed, load objectives for next mission
		missions[lastCompleted+1].updateObjectives();
		return false;
	}
	
	public void drawGUI() {
		missions[currentMission].draw();
	}
	
	public void applyStyles (GUIStyle headingStyle, GUIStyle contentStyle) {
		foreach (Mission mission in missions) {
			mission.applyStyles (headingStyle, contentStyle);
			Debug.Log("Set style for mission " + mission.missionName);
		}
	}
	
	public bool checkCompletion() {
		foreach (Mission mission in missions) {
			if (!mission.checkCompletion()) {
				return false;
			}
		}
		return true;
	}
}
