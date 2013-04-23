using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public Controls controls;
	public AudioListener audio;
	public bool isPaused = false;
	public int itemWidth = 200;
	public int itemHeight = 50;
	public GUIStyle paneLabelStyle;
	string pane = "/Pause";
	void Start () {}
	
	void Update () {
		if (Input.GetKeyDown(controls.pause)) {
			switch (isPaused) {
				case false:
					Time.timeScale = 0f;
					isPaused = true;
					break;
				case true:
					Time.timeScale = 1f;
					isPaused = false;
					break;
			}
		}
	}
	
	void OnGUI () {
		if (isPaused) {
			GUI.Label(new Rect(0,0,Screen.width, 50), pane, paneLabelStyle);
			//If game is paused, draw pause menu.
			switch (pane) {
				case "/Pause":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Videos")) {
						pane = "/Pause/Videos";
					}
					break;
				case "/Pause/Videos":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,125,itemWidth,itemHeight), "Video 1")) {
						print (Application.dataPath);
						System.Diagnostics.Process.Start(@Application.dataPath+"/Cutscenes/Cutscene_001.MP4");
					}
					break;
			}
		}
	}
}