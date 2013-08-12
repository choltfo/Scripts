using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Holds a campaign, and hooks it to an update sequence.
/// </summary>
public class CampaignDisplay : MonoBehaviour {
	/// <summary>
	/// The campaign that is played through.
	/// </summary>
	public Campaign campaign;
	/// <summary>
	/// The pause screen.
	/// </summary>
	public Pause pauseScreen;
	/// <summary>
	/// The controls to use.
	/// </summary>
	public Controls controls;
	bool DONE = false;
	/// <summary>
	/// The heading style.
	/// </summary>
	public GUIStyle headingStyle;
	/// <summary>
	/// The content style.
	/// </summary>
	public GUIStyle contentStyle;
	
	public TriggerableEvent startingEvent;
	public SubtitleController subtitleController;
	
	void Start () {
		
		startingEvent.Trigger(subtitleController);
		
		//LevelSerializer.SerializeLevelToFile("SaveGame");
		//print ("Saved level.");
		campaign.applyStyles(headingStyle, contentStyle);
		foreach (Mission mission in campaign.missions) {
			foreach (Objective objective in mission.objectives) {
				objective.inCampaign = true;
			}
		}
	}
	
	void Update () {
		if (DONE) return;
		if (Input.GetKeyDown(controls.objectiveDetails)) {
			pauseScreen.pane = "/Objective";
			Time.timeScale = 0f;
		}
		//Debug.Log("Checking missions");
		//
		if (campaign.updateMissions()) {
			Debug.Log("YOU HAVE BEATEN ALL MISSIONS BY updateMissions");
			DONE = true;
		}
		
		if (campaign.hasChanged()) {
			//LevelSerializer.SerializeLevelToFile("SaveGame");
			//print ("Saved level.");
		}
		
	}
	
	void OnGUI () {
		if (Time.timeScale == 0) {
			return;
		}
		campaign.drawGUI();
		if (GUI.Button(new Rect(50,Screen.height-75,200,50), "RELOAD WORLD")) {
			Application.LoadLevel(Application.loadedLevel);
		}	
	}
}
