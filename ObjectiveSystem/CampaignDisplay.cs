using UnityEngine;
using System.Collections;

[System.Serializable]

public class CampaignDisplay : MonoBehaviour {
	
	public Campaign campaign;
	bool DONE = false;
	
	void start () {}
	
	void Update () {
		if (DONE) return;
		//Debug.Log("Checking missions");
		if (campaign.updateMissions()) {
			Debug.Log("YOU HAVE BEATEN ALL MISSIONS BY updateMissions");
			DONE = true;
		}
	}
	
	void OnGUI () {
		if (GUI.Button(new Rect(50,0,200,50), "RELOAD WORLD")) {
			Application.LoadLevel(Application.loadedLevel);
		}	
	}
}
