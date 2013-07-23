using UnityEngine;
using System.Collections;
using System.IO;
using System.Reflection;

/// <summary>
/// The pause screen.
/// </summary>

public class Pause : MonoBehaviour {
	/// <summary>`
	/// The controls layout to use.
	/// </summary>
	public Controls controls;
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
	/// The pane path of the current pause window.
	/// </summary>
	
	void Update () {
		if (Input.GetKeyDown(controls.pause)) {
			pane = "/Pause";
			if (Time.timeScale != 0) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
			}
		}

		Screen.showCursor = (Time.timeScale == 0f);
		Screen.fullScreen = true;
	}
	
	void Start () {
		Screen.fullScreen = true;
	}
	
	KeyCode ChangeKey (KeyCode origKey) {
		while (true) {
			Event e = Event.current;
			if (e.isKey) {
				if (e.keyCode == KeyCode.Escape) {
					return origKey;
				}
				else {
					Debug.Log(e.keyCode);
					return e.keyCode;
				}
			}
		}
		/*
		bool done = false;
		while (!done) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				done = true;
			}
		}*/
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
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,275,itemWidth,itemHeight), "Reload")) {
						Time.timeScale = 1f;
						LevelSerializer.LoadSavedLevelFromFile("SaveGame");
					}
					break;
				case "/Objective":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50,itemWidth,itemHeight), "Close")) {
						pane = "/Pause";
						Time.timeScale = 1f;
					}
					break;
				case "/Dead":																					//MAYBE
					GUI.Label(new Rect((Screen.width/2) - 200,(Screen.height/2) - 20, 400 ,40), "YOU ARE DEAD!", deathScreenStyle);
					break;
				case "/Pause/Controls":
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,350,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					FieldInfo[] variables = 
						typeof(Controls).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);//.Select(f => f.Name);
					//for (int i=0; i < 23 ; i++) {
					int i = 0;
					// WARNING: REFLECTIONS AHEAD!
					foreach (FieldInfo variable in variables) {
						if (GUI.Button(new Rect(((Screen.width/4)-150/2)+(Screen.width/4),50+75*i,150,itemHeight), variable.Name + ": " + variable.GetValue(controls))) {
							print (variable.Name + " >> " + variable.FieldType.ToString());
							variable.SetValue(controls, ChangeKey((KeyCode)variable.GetValue(controls)));
						}
						i++;
					}
					break;
				case "/Pause/Videos":
				
					string [] fileEntries = Directory.GetFiles(Application.dataPath+"/Resources/Cutscenes/");
					int o = 0;
					foreach(string fileName in fileEntries) {
						if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,50+75*o,itemWidth,itemHeight),
						"Video " + o.ToString())) {
							print(fileName);
							System.Diagnostics.Process.Start(fileName);
						}
						o++;
					}
					if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,350,itemWidth,itemHeight), "Back")) {
						pane = "/Pause";
					}
					break;
				default:
					if (pane.StartsWith("/Store")) {
						break;
					}
					Debug.Log("Invalid switch - " + pane);
					pane = "/Pause";
					break;
			}
		}
	}
}
