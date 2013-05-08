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
				
				//Weapons
				weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 25, 125),
					weaponSlider, 1.0F, 0.0F, 10.0F);
				
				GUI.Box(new Rect(Screen.width/2-165, 125, 165, 125), "");
				
				//Items/ammo
				ammoItemSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+165, 125, 25, 125),
					ammoItemSlider, 1.0F, 0.0F, 10.0F);
				
				GUI.Box(new Rect(Screen.width/2, 125, 165, 125), "");
				
				if (storeMode == StoreMode.Buy) {
					int i = 0;
					foreach (Weapon weapon in inventory.weapons) {
						GUI.Button(new Rect(Screen.width/2-165, 125+(25*i), 150, 25), weapon.DisplayName);
						i++;
					}
				}
				if (storeMode == StoreMode.Sell) {
					int i = 0;
					foreach (Weapon weapon in playerInv.weapons) {
						GUI.Button(new Rect(Screen.width/2-165, 125+(25*i), 150, 25), weapon.DisplayName);
						i++;
					}
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