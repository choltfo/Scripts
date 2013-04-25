using UnityEngine;
using System.Collections;
using System.IO;

public class Pause : MonoBehaviour {
	public Controls controls;
	public AudioListener audio;
	public int itemWidth = 200;
	public int itemHeight = 50;
	public GUIStyle paneLabelStyle;
	string pane = "/Pause";
	
	void Update () {
		if (Input.GetKeyDown(controls.pause)) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}
		}
	}
	
	void OnGUI () {
		if (Time.timeScale == 0) {
			GUI.Label(new Rect(0,0,Screen.width, 50), pane, paneLabelStyle);
			//If game is paused, draw pause menu.
			//Heights are 50, 125, 200, 275, 350, 375, etc. Increment by 25. 
			switch (pane) {
				case "/Pause":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Videos")) {
						pane = "/Pause/Videos";
					}
					break;
				case "/Pause/Videos":
					string [] fileEntries = Directory.GetFiles(Application.dataPath+"/Resources/Cutscenes/");
					int i = 1;
					foreach(string fileName in fileEntries){
						if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50+75*i,itemWidth,itemHeight),
						"Video " + i.ToString())) {
							print(fileName);
							System.Diagnostics.Process.Start(fileName);
						}
						i++;
					}
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					break;
			}
		}
	}
}