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
	
	void Start () {
		campaign.applyStyles(headingStyle, contentStyle);
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
