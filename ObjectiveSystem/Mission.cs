using UnityEngine;
using System.Collections;

[System.Serializable]

public class Mission {
	
	public string missionName;
	public int currentObjecive = 0;
	public Objective[] objectives;
	public bool complete = false;
	public GUIStyle headingStyle;
	//public Vector3 displayCoords;
	
	public bool updateObjectives() {
		bool notDone = false;
		foreach (Objective objective in objectives) {
			if (!objective.checkCompletion()) {
				notDone = true;
			}
		}
		if (!notDone) {
			complete = true;
			return true;
		}
		
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i].Activate(i);
		}
		return false;
	}

	public bool checkCompletion() {
		foreach (Objective objective in objectives) {
			if (!objective.checkCompletion()) {
				return false;
			}
		}
		return true;
	}
	
	public void Begin () {
		//Debug.Log("STARTING MISSION");
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i].Activate(i);
		}
	}
	
	public void applyStyles (GUIStyle l_headingStyle, GUIStyle l_contentStyle) {
		headingStyle = l_headingStyle;
		foreach (Objective objective in objectives) {
			objective.labelStyle = l_contentStyle;
			Debug.Log("Set style for objective " + objective.name);
		}
	}
	
	public void draw() {
		GUI.Label(new Rect(50f,10,300f,30f),missionName,headingStyle);
	}
}
