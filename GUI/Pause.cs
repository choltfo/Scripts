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
	public CampaignDisplay campaign;
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
	/// 
	/// 	
	
	
	void Update () {
		
		if (Input.GetKeyDown(controls.pause)) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
				if (pane == "/Inventory") {
					GetComponent<ShootObjects>().reset();
					Weapon SelectedWeapon = null;
					HardPoint SelectedModSlot = null;
					WeaponAttachment SelectedAttachment = null;
				}
			}
			pane = "/Pause";
		}
		
		if (Input.GetKeyDown(controls.inventory)) {
			pane = "/Inventory";
			if (Time.timeScale != 0) {
				Time.timeScale = 0f;
			} else {
				Time.timeScale = 1f;
				GetComponent<ShootObjects>().reset();
				Weapon SelectedWeapon = null;
				HardPoint SelectedModSlot = null;
				WeaponAttachment SelectedAttachment = null;
			}
		}
		
		
		Screen.lockCursor = !(Time.timeScale == 0f);
		Screen.showCursor = (Time.timeScale == 0f);
		Screen.fullScreen = true;
	}
	
	void Start () {
		Screen.fullScreen = true;
	}
	
	KeyCode ChangeKey (KeyCode origKey) {
		bool t = true;
		while (t) {
			Event e = Event.current;
			if (e.isKey) {
				print(e.keyCode);
				if (e.keyCode == KeyCode.Escape) {
					t = false;
					return origKey;
				}
				else {
					t = false;
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
		return origKey;
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
					//if (GUI.Button(new Rect((Screen.width/2)-itemWidth/2,275,itemWidth,itemHeight), "Reload")) {
					//	Time.timeScale = 1f;
					//	LevelSerializer.LoadSavedLevelFromFile("SaveGame");
					//}
					break;
				case "/Inventory":
					inventoryView();
					break;
				case "/Objective":
					objectiveView ();
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
					if (pane.StartsWith("/Convo")) {
						break;
					}
					Debug.Log("Invalid switch - " + pane);
					pane = "/Pause";
					break;
			}
		}
	}
	
	float attachmentSlider = 0;
	float weaponSlider = 0;
	int ItemElements = 0;
	
	Weapon SelectedWeapon = null;
	HardPoint SelectedModSlot = null;
	WeaponAttachment SelectedAttachment = null;
	
	int pos = 0;
	int selection = -1; 
	
	void objectiveView () {
		
		weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+235, 125, 15, 200),
			weaponSlider, 8.0F, 0.0F, ((pos < 8) ? 8 : pos));
		GUI.Box(new Rect(Screen.width/2-250, 125, 500, 200), "");
		
		GUI.Label(new Rect(Screen.width/2-250, 100, 500, 25), "Missions");
		
		pos = 0;
		
		for (int i = 0; i < campaign.campaign.missions.Length; i++) {
			Mission m = campaign.campaign.missions[i];
			if (campaign.campaign.currentMission >= i) {
				if (m.complete) {
					GUI.Box(new Rect(Screen.width/2-250, 125+(25*(pos-(int)weaponSlider)) , 485, 25), "");
				}
				if (GUI.Toggle(new Rect(Screen.width/2-250, 125+(25*(pos-(int)weaponSlider)), 485, 25), (selection == i), m.missionName)) {
					if (i <= campaign.campaign.currentMission) {
						selection = i;
					}
					for (int o = 0; o < m.objectives.Length; o++) {
						pos++; // Increment the position so later elements get aligned properly.
						Objective ob = m.objectives[o];
						GUI.Label(new Rect(Screen.width/2-220, 125+(25*(pos-(int)weaponSlider)), 455, 25), ob.name + (ob.complete ? " \u2713" : "") +  (ob.Active ? " \u2190" : ""));
						
						
					}
				}
				
				pos ++; // Increment the position so later elements get aligned properly.
			}
		}
	} 
	
	void inventoryView () {
		Inventory inventory = GetComponent<ShootObjects>().inventory;
		
		weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 15, 200),
			weaponSlider, 8.0F, 0.0F, ((inventory.weapons.Length < 8) ? 8 : inventory.weapons.Length));
		GUI.Box(new Rect(Screen.width/2-215, 125, 215, 200), "");
		
		attachmentSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+200, 125, 15, 200),
		attachmentSlider, 8.0F, 0.0F, ((ItemElements < 8) ? 8 : ItemElements));
		GUI.Box(new Rect(Screen.width/2, 125, 215, 200), "");
		
		int i = 0;
		Weapon transferredWeapon= new Weapon();
		int soldWeaponSlot = -1;
		GUI.Label(new Rect(Screen.width/2-250, 100, 200, 25), "Weapons");
		GUI.Label(new Rect(Screen.width/2, 100, 150, 25), "Grenades, Attachments");
		foreach (Weapon weapon in inventory.weapons) {
			if (weapon.IsValid && i < 8 + (int)weaponSlider && i >= (int)weaponSlider) {
				if (GUI.Button(new Rect(Screen.width/2-265, 125+(25*(i-(int)weaponSlider)), 250, 25), weapon.DisplayName)) {
					
					SelectedWeapon = weapon;
					SelectedModSlot = null;
					SelectedAttachment = null;
					
				}
				if (SelectedWeapon == weapon) {
					int p = 1;
					
					for (int o = 0; o<weapon.attachments.Length; o++) {
						if (weapon.attachments[o].attachment.isValid) {
							if (GUI.Button(new Rect(Screen.width/2-250, 125+(25*(i+1-(int)weaponSlider)), 235, 25),
									weapon.attachments[o].name + ": " + weapon.attachments[o].attachment.railType.ToString()+
									" "+weapon.attachments[o].attachment.type.ToString())) {
								
								// Pluck thingy
								inventory.attachments.Add(weapon.attachments[o].attachment);
								weapon.attachments[o].attachment = new WeaponAttachment();
								
								
							}
							p++;
						} else {
							if (GUI.Button(new Rect(Screen.width/2-250, 125+(25*(i+1-(int)weaponSlider)), 235, 25),
									weapon.attachments[o].name + ": " + weapon.attachments[o].connectionType.ToString()+
									". Open.")) {
								// Prepare to attach thingy
								SelectedModSlot = weapon.attachments[o];
								
							}
							p++;
						}
						i++;
					} // End for each hardpoint.
					
				}
				i++;
			} // End for each gun.
		}
		
		int a = 0;
		foreach (WeaponAttachment att in inventory.attachments) {
			if (i < 8 + (int)attachmentSlider && i >= (int)attachmentSlider) {
				//GUI.Box(new Rect(Screen.width/2+150, 125+(25*i), 50, 25),  "$"+AmmoPrice.Get((AmmoType)a).ToString());
				if (GUI.Button(new Rect(Screen.width/2, 125+(25*(a-(int)attachmentSlider)), 200, 25),
					att.railType.ToString()+" "+att.type.ToString())) {
					
					if (SelectedModSlot != null) {
						if (SelectedModSlot.connectionType == att.railType) {
							
							// Afix part.
							SelectedModSlot.attachment = att;
							
							// Clean up.
							SelectedModSlot = null;
							SelectedAttachment = att;
						}
					}
				}
			}
			a++;
		}
		
		if (SelectedAttachment != null) {
			inventory.attachments.Remove(SelectedAttachment);
			SelectedAttachment = null;
		}
		
	}
}
