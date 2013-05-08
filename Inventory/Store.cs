using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {
	
	public static int lastUID = -1;
	
	public static int generateUID () {
		lastUID ++;
		return lastUID;
	}
	
	[HideInInspector]
	public int UID;
	public string storeName;
	public Inventory inventory;
	public Inventory playerInv;
	public Pause pauseController;
	public StoreMode storeMode = StoreMode.Buy;
	
	public float weaponSlider = 0;
	public float ammoItemSlider = 0;
	
	public int ItemElements = 0;
	public int topItemElement = 0;
	
	public void Interact (ShootObjects player) {
		playerInv = player.inventory;
		Time.timeScale = 0;
		pauseController.pane = "/Store/"+UID.ToString();
	}
	
	void OnGUI () {
		if (Time.timeScale == 0) {
			if (pauseController.pane == "/Store/"+UID.ToString()) {
				
				if (GUI.Button(new Rect(0,25,100,100),"Resume")) {
					pauseController.pane = "/Pause";
					Time.timeScale = 1f;
					storeMode = StoreMode.Buy;
				}
				
				if (GUI.Button(new Rect(Screen.width/2-50,100,50,25), "Buy")) {
					storeMode = StoreMode.Buy;
				}
				if (GUI.Button(new Rect(Screen.width/2,100,50,25), "Sell")) {
					storeMode = StoreMode.Sell;
				}
				
				/*if (storeMode == StoreMode.Buy) {
					weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 15, 200),
						weaponSlider, 8.0F, 0.0F, ((inventory.weapons.Length < 8) ? 8 : inventory.weapons.Length));
					GUI.Box(new Rect(Screen.width/2-165, 125, 165, 200), "");
					ammoItemSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+150, 125, 15, 200),
						ammoItemSlider, 8.0F, 0.0F, ((ItemElements < 8) ? 8 : ItemElements));
					GUI.Box(new Rect(Screen.width/2, 125, 165, 200), "");
					
					int i = 0;
					foreach (Weapon weapon in inventory.weapons) {
						if (weapon.IsValid && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2-165, 125+(25*i), 150, 25), weapon.DisplayName);
						}
						i++;
					}
					i = 0;
					int a = 0;
					foreach (int ammo in inventory.ammo) {
						if (ammo > 0 && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), ((AmmoType)a).ToString());
							i++;
						}
						a++;
					}
					foreach (Grenade grenade in inventory.grenades) {
						if (i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), grenade.name);
						}
						i++;
					}
					
					ItemElements = i;
				}*/
				
				if (storeMode == StoreMode.Sell) {
					weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 15, 200),
						weaponSlider, 8.0F, 0.0F, ((playerInv.weapons.Length < 8) ? 8 : playerInv.weapons.Length));
					GUI.Box(new Rect(Screen.width/2-165, 125, 165, 200), "");
					ammoItemSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+150, 125, 15, 200),
						ammoItemSlider, 8.0F, 0.0F, ((ItemElements < 8) ? 8 : ItemElements));
					GUI.Box(new Rect(Screen.width/2, 125, 165, 200), "");
					
					int i = 0;
					foreach (Weapon weapon in playerInv.weapons) {
						if (weapon.IsValid && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2-165, 125+(25*i), 150, 25), weapon.DisplayName);
						}
						i++;
					}
					i = 0;
					int a = 0;
					foreach (int ammo in playerInv.ammo) {
						if (ammo > 0 && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), ((AmmoType)a).ToString());
							i++;
						}
						a++;
					}
					foreach (Grenade grenade in playerInv.grenades) {
						if (i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), grenade.name);
						}
						i++;
					}
					
					ItemElements = i;
				}
			}
		}
	}
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}

public enum StoreMode {
	Buy,
	Sell
}