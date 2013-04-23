using UnityEngine;
using System.Collections;

[System.Serializable]

public class CampaignDisplay : MonoBehaviour {
	
	public Campaign campaign;
	bool DONE = false;
	public GUIStyle headingStyle;
	public GUIStyle contentStyle;
	
	void Start () {
		campaign.applyStyles(headingStyle, contentStyle);
	}
	
	void Update () {
		if (DONE) return;
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
