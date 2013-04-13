using UnityEngine;
using System.Collections;

[System.Serializable]

public class Campaign {
	
	public int currentMission = 0;
	public Mission[] missions;
	public bool complete = false;
	//public Vector3 displayCoords;
	
	public bool updateMissions() {
		bool notDone = false;
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
			
			//if (missions[lastCompleted+1].active == false) {
			//	missions[lastCompleted+1].Activate();
			//}
			//if (missions[lastCompleted] != null) {
			//	missions[lastCompleted].Disactivate();
			//}
			return false;
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
