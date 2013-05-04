using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// The pause screen.
/// </summary>

public class Pause : MonoBehaviour {
	/// <summary>`
	/// The controls layout to use.
	/// </summary>
	public Controls controls;
	/// <summary>
	/// The audio to play through.
	/// </summary>
	public AudioListener audio;
	/// <summary>
	/// The width of the items.
	/// </summary>
	public int itemWidth = 150;
	/// <summary>
	/// The height of the items.
	/// </summary>
	public int itemHeight = 50;
	/// <summary>
	/// The pane label style.
	/// </summary>
	public GUIStyle paneLabelStyle;
	/// <summary>
	/// The death screen style.
	/// </summary>
	public GUIStyle deathScreenStyle;
	/// <summary>
	/// The color of the death screen.
	/// </summary>
	public Color deathScreenColor;
	/// <summary>
	/// The <see cref="CamerFader"/> to fade to death with.
	/// </summary>
	public CameraFade fader;
	/// <summary>
	/// The current pane.
	/// </summary>
	public string pane = "/Pause";
	/// <summary>
	/// The 
	
	void Update () {
		if (Input.GetKeyDown(controls.pause)) {
			pane = "/Pause";
			if (Time.timeScale != 0) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}
		}
	}
	KeyCode ChangeKey (KeyCode origKey) {
		Event e = Event.current;
		if (e.isKey) {
			return e.keyCode;
		}
		else {
			return origKey;
		}
	}
	void OnGUI () {
		if (Time.timeScale == 0) {
			GUI.Label(new Rect(0,0,Screen.width, 50), pane, paneLabelStyle);
			//If game is paused, draw pause menu.
			//Heights are 50, 125, 200, 275, 350, 375, etc. Increment by 25. 
			switch (pane) {
				case "/Pause":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Objectives")) {
						pane = "/Objective";
					}					
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,125,itemWidth,itemHeight), "Videos")) {
						pane = "/Pause/Videos";
					}
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,200,itemWidth,itemHeight), "Controls")) {
						pane = "/Pause/Controls";
					}
					break;
				case "/Objective":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Close")) {
						pane = "/Pause";
						Time.timeScale = 1f;
					}
					Debug.Log("DOSOME\nSHITMOTHERF***ER".Split("\n".ToCharArray()).Length-1);
					break;
				case "/Dead":																					//MAYBE
					GUI.Label(new Rect((Screen.width/2) - 200,(Screen.height/2) - 20, 400 ,40), "YOU ARE DEAD!", deathScreenStyle);
					break;
				case "/Pause/Controls":
				/*
					for (int i=0; i < 4; i++) {
						for (int j=0; j < 3; j++) {
							if (GUI.Button(new Rect(((Screen.width/4)-150/2)+(Screen.width/4)*j,50+75*i,150,itemHeight), "TestButton")) {
								Debug.Log("WHAT");
								Debug.Log(ChangeKey(controls.interact));
							}
						}
					}
					*/
					int selectedButton = 0;
					string[] buttonText = {"TestButton", "TestButton", "TestButton", "TestButton", "TestButton", "TestButton" };

					selectedButton = GUI.SelectionGrid(new Rect((Screen.width/2)-300/2,50,300,200), selectedButton, buttonText, 3);

					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,350,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					break;
				case "/Pause/Videos":
					string [] fileEntries = Directory.GetFiles(Application.dataPath+"/Resources/Cutscenes/");
					int i = 1;
					foreach(string fileName in fileEntries) {
						if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50+75*i,itemWidth,itemHeight),
						"Video " + i.ToString())) {
							print(fileName);
							System.Diagnostics.Process.Start(fileName);
						}
						i++;
					}
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,350,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					break;
				default:
					Debug.Log("Invalid switch - " + pane);
					pane = "/Pause";
					break;
			}
		}
	}
}
