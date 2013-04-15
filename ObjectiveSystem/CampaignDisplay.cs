using UnityEngine;
using System.Collections;

[System.Serializable]

public class CampaignDisplay : MonoBehaviour {
	
	public Campaign campaign;
	bool DONE = false;
	
	void start () {
		campaign.applyStyles();
	}
	
	void Update () {
		if (DONE) return;
		//Debug.Log("Checking missions");
		if (campaign.updateMissions()) {
			Debug.Log("YOU HAVE BEATEN ALL MISSIONS BY updateMissions");
			DONE = true;
		}
	}
	
	void OnGUI () {
		campaign.drawGUI();
		if (GUI.Button(new Rect(50,Screen.height-75,200,50), "RELOAD WORLD")) {
			Application.LoadLevel(Application.loadedLevel);
		}	
	}
}
