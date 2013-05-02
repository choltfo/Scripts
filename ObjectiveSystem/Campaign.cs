using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// A campaign.
/// </summary>
public class Campaign {
	/// <summary>
	/// The current mission.
	/// </summary>
	public int currentMission = 0;
	/// <summary>
	/// The missions.
	/// </summary>
	public Mission[] missions;
	/// <summary>
	/// Whether the campaign is complete.
	/// </summary>
	public bool complete = false;
	//public Vector3 displayCoords;
	
	/// <summary>
	/// Updates the missions.
	/// </summary>
	/// <returns>
	/// Whether the missions are all complete.
	/// </returns>
	public bool updateMissions() {
		//Number of the last completed mission
		int lastCompleted = -1;
		//Updates lastCompleted 
		for (int i = 0; i < missions.Length; i++) {
			if (missions[i].complete) {
				lastCompleted = i;
			}
		}
		//Returns true if all missions are completed
		if (lastCompleted+1 == missions.Length) {
			return true;
		}
		//If not all missions are completed, load objectives for next mission
		missions[lastCompleted+1].updateObjectives();
		currentMission = lastCompleted + 1;
		return false;
	}
	
	/// <summary>
	/// Draws the GUI.
	/// </summary>
	public void drawGUI() {
		missions[currentMission].draw();
	}
	
	/// <summary>
	/// Applies the styles.
	/// </summary>
	/// <param name='headingStyle'>
	/// Heading style.
	/// </param>
	/// <param name='contentStyle'>
	/// Content style.
	/// </param>
	public void applyStyles (GUIStyle headingStyle, GUIStyle contentStyle) {
		foreach (Mission mission in missions) {
			mission.applyStyles (headingStyle, contentStyle);
			//Debug.Log("Set style for mission " + mission.missionName);
		}
	}
	
	/// <summary>
	/// Checks the completion.
	/// </summary>
	/// <returns>
	/// The completion.
	/// </returns>
	public bool checkCompletion() {
		foreach (Mission mission in missions) {
			if (!mission.checkCompletion()) {
				return false;
			}
		}
		return true;
	}
}
