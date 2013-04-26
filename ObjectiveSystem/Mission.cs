using UnityEngine;
using System.Collections;

[System.Serializable]

public class Mission {
	
	public string missionName;
	public int currentObjecive = 0;
	public Objective[] objectives;
	public bool complete = false;
	public GUIStyle headingStyle;
	int activeObjectives;
	//public Vector3 displayCoords;
	
	public bool updateObjectives() {
		complete = true; //Probably not a good idea
		//Checks if any of the objectives are incomplete
		int completedCount = 0;
		for (int i = 0; i < objectives.Length; i++) {
			if (!objectives[i].complete) {
				complete = false;
				objectives[i].Activate(completedCount);
				completedCount++;
			} else {
				objectives[i].Deactivate();
			}
		}
		return complete;
	}

	public bool checkCompletion() {
		foreach (Objective objective in objectives) {
			if (!objective.complete) {
				return false;
			}
		}
		return true;
	}
	
	public void Begin () {
		//Debug.Log("STARTING MISSION");
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i-1].Activate(i-1);
		}
	}
	
	public void applyStyles (GUIStyle l_headingStyle, GUIStyle l_contentStyle) {
		headingStyle = l_headingStyle;
		foreach (Objective objective in objectives) {
			objective.labelStyle = l_contentStyle;
			//Debug.Log("Set style for objective " + objective.name);
		}
	}
	
	public void draw() {
		GUI.Label(new Rect(50,10,300,30),missionName,headingStyle);
		GUI.Box(new Rect(50,10,300, 80+(50*activeObjectives)), "");
	}
}
