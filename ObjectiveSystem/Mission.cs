using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Represents a mission.
/// </summary>
public class Mission {
	/// <summary>
	/// The name of the mission.
	/// </summary>
	public string missionName;
	/// <summary>
	/// The current objecive.
	/// </summary>
	public int currentObjecive = 0;
	/// <summary>
	/// The objectives.
	/// </summary>
	public Objective[] objectives;
	/// <summary>
	/// Whether this mission is complete.
	/// </summary>
	public bool complete = false;
	/// <summary>
	/// The heading style.
	/// </summary>
	public GUIStyle headingStyle;
	/// <summary>
	/// The number of active objectives.
	/// </summary>
	int activeObjectives;
	//public Vector3 displayCoords;
	
	/// <summary>
	/// Updates the objectives.
	/// </summary>
	/// <returns>
	/// Whether the objectioves are complete.
	/// </returns>
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
	
	/// <summary>
	/// Checks the completion of the objectives.
	/// </summary>
	/// <returns>
	/// Whether the objectives are complete.
	/// </returns>
	public bool checkCompletion() {
		foreach (Objective objective in objectives) {
			if (!objective.complete) {
				return false;
			}
		}
		return true;
	}
	
	/// <summary>
	/// Begin this instance.
	/// </summary>
	public void Begin () {
		//Debug.Log("STARTING MISSION");
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i-1].Activate(i-1);
		}
	}
	
	/// <summary>
	/// Applies the styles.
	/// </summary>
	/// <param name='l_headingStyle'>
	/// The heading style.
	/// </param>
	/// <param name='l_contentStyle'>
	/// The content style.
	/// </param>
	public void applyStyles (GUIStyle l_headingStyle, GUIStyle l_contentStyle) {
		headingStyle = l_headingStyle;
		foreach (Objective objective in objectives) {
			objective.labelStyle = l_contentStyle;
			//Debug.Log("Set style for objective " + objective.name);
		}
	}
	
	/// <summary>
	/// Draw this mission's GUI.
	/// </summary>
	public void draw() {
		GUI.Label(new Rect(50,10,300,30),missionName,headingStyle);
		GUI.Box(new Rect(50,10,300, 80+(50*activeObjectives)), "");
	}
}
