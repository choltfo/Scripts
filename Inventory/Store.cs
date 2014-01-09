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
	public float saleMarkup;
	public float buyMarkup;
	public string storeName;
	public Inventory inventory;
	public ShootObjects player;
	public Inventory playerInv;
	public Pause pauseController;
	public StoreMode storeMode = StoreMode.Buy;
	
	public float weaponSlider = 0;
	public float ammoItemSlider = 0;
	
	public int ItemElements = 0;
	public int topItemElement = 0;
	
	public AudioClip openingSound;
	public AudioSource AS;
	
	Weapon transferredWeapon = new Weapon();

	public void Interact (ShootObjects l_player) {
		if (GetComponent<InteractNPC>() != null) return;
		AS.PlayOneShot(openingSound);
		playerInv = l_player.inventory;
		Open (l_player);
		
	}
	
	public void Open (ShootObjects l_player) {
		playerInv = l_player.inventory;
		player = l_player;
		pauseController.pane = "/Store/"+UID.ToString();
		Time.timeScale = 0;
	}
	
	void OnGUI () {
		
		if (Time.timeScale == 0) {
			if (pauseController.pane.Equals("/Store/"+UID.ToString()) ) {
				
				//player.inventory = playerInv;
				
				if (GUI.Button(new Rect(0,25,100,100),"Resume")) {
					pauseController.pane = "/Pause";
					Time.timeScale = 1f;
					storeMode = StoreMode.Buy;
				}
				
				if (GUI.Button(new Rect(Screen.width/2+215,125,50,25), "Buy")) {
					storeMode = StoreMode.Buy;
				}
				if (GUI.Button(new Rect(Screen.width/2+215,150,50,25), "Sell")) {
					storeMode = StoreMode.Sell;
				}
					
				if (storeMode == StoreMode.Buy) {	// Moving items from store to player
					weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 15, 200),
						weaponSlider, 8.0F, 0.0F, ((inventory.weapons.Length < 8) ? 8 : inventory.weapons.Length));
					GUI.Box(new Rect(Screen.width/2-215, 125, 215, 200), "");
					
					ammoItemSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+200, 125, 15, 200),
						ammoItemSlider, 8.0F, 0.0F, ((ItemElements < 8) ? 8 : ItemElements));
					GUI.Box(new Rect(Screen.width/2, 125, 215, 200), "");
					
					int i = 0;
					transferredWeapon.IsValid = false;
					foreach (Weapon weapon in inventory.weapons) {
						if (weapon.IsValid && i < 8 + (int)weaponSlider && i >= (int)weaponSlider) {
							GUI.Box(new Rect(Screen.width/2-65, 125+(25*(i-(int)weaponSlider)), 50, 25), "$"+(weapon.price*saleMarkup).ToString());
							if (GUI.Button(new Rect(Screen.width/2-215, 125+(25*(i-(int)weaponSlider)), 150, 25), weapon.DisplayName)) {
								if (checkForEmptyWeaponSlot(player.inventory.weapons)) {
									player.inventory.cash -= weapon.price*buyMarkup;

									weapon.destroy();	// Removes the gameobject from existence, if possible. 
									weapon.IsValid = true;
									player.inventory.weapons[findWeaponSlot(player.inventory.weapons)] = weapon;
									inventory.weapons[i] = new Weapon();
									player.ensuredSwitch();

								} else print ("No slot found in player inventory for weapon " + weapon.WeaponName + ". Cancelling transaction.");
							}
							i++;
						}
					}
					
					i = 0;
					int a = 0;
					bool transferredAnyAmmo = false;
					AmmoType transferredAmmo = AmmoType.Parabellum9x19mm;
					foreach (int ammo in inventory.ammo) {
						if (ammo > 0 && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Box(new Rect(Screen.width/2+150, 125+(25*i), 50, 25),  "$"+(AmmoPrice.Get((AmmoType)a)*saleMarkup).ToString());
							if (GUI.RepeatButton(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), ((AmmoType)a).ToString())) {
								transferredAnyAmmo = true;
								transferredAmmo = (AmmoType)a;
								player.inventory.cash -= AmmoPrice.Get((AmmoType)a)*saleMarkup;
							}
							i++;
						}
						a++;
					}
					Grenade transferredGrenade = null;
					foreach (Grenade grenade in inventory.grenades) {
						if (i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Box(new Rect(Screen.width/2+150, 125+(25*(i-(int)ammoItemSlider)), 50, 25), "$"+(grenade.price*saleMarkup).ToString());
							if (GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), grenade.name)) {
								transferredGrenade = grenade;
								player.inventory.cash -= (grenade.price*saleMarkup);
							}
						}
						i++;
					}
					
					if (transferredAnyAmmo) player.inventory.ammo[(int)transferredAmmo] ++;
					if (transferredAnyAmmo) inventory.ammo[(int)transferredAmmo] --;
					if (transferredGrenade != null) {
						inventory.grenades.Remove(transferredGrenade);
						player.inventory.grenades.Add(transferredGrenade);
					}
					
					ItemElements = i;
				}
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////
				if (storeMode == StoreMode.Sell) {	// Moving items for player to store.
					weaponSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2-15, 125, 15, 200),
						weaponSlider, 8.0F, 0.0F, ((player.inventory.weapons.Length < 8) ? 8 : player.inventory.weapons.Length));
					GUI.Box(new Rect(Screen.width/2-215, 125, 215, 200), "");
					
					ammoItemSlider = GUI.VerticalScrollbar(new Rect(Screen.width/2+200, 125, 15, 200),
						ammoItemSlider, 8.0F, 0.0F, ((ItemElements < 8) ? 8 : ItemElements));
					GUI.Box(new Rect(Screen.width/2, 125, 215, 200), "");
					
					int i = 0;
					Weapon transferredWeapon = new Weapon();
					int soldWeaponSlot = -1;
					foreach (Weapon weapon in player.inventory.weapons) {
						if (weapon.IsValid && i < 8 + (int)weaponSlider && i >= (int)weaponSlider) {
							GUI.Box(new Rect(Screen.width/2-65, 125+(25*(i-(int)weaponSlider)), 50, 25), "$"+(weapon.price*buyMarkup).ToString());
							if (GUI.Button(new Rect(Screen.width/2-215, 125+(25*(i-(int)weaponSlider)), 150, 25), weapon.DisplayName)) {
								if (checkForEmptyWeaponSlot(inventory.weapons)) {
									player.inventory.cash += weapon.price*saleMarkup;
									
									weapon.destroy();

									weapon.IsValid = true;
									inventory.weapons[findWeaponSlot(inventory.weapons)] = weapon;
									player.inventory.weapons[i] = new Weapon();
									player.ensuredSwitch();
								}
							}
							i++;
						}
					}
					
					i = 0;
					int a = 0;
					bool transferredAnyAmmo = false;
					AmmoType transferredAmmo = AmmoType.Parabellum9x19mm;
					foreach (int ammo in player.inventory.ammo) {
						if (ammo > 0 && i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Box(new Rect(Screen.width/2+150, 125+(25*i), 50, 25),  "$"+(AmmoPrice.Get((AmmoType)a)*buyMarkup).ToString());
							if (GUI.RepeatButton(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), ((AmmoType)a).ToString())) {
								transferredAnyAmmo = true;
								transferredAmmo = (AmmoType)a;
								player.inventory.cash += (AmmoPrice.Get((AmmoType)a)*buyMarkup);
							}
							i++;
						}
						a++;
					}
					Grenade transferredGrenade = null;
					foreach (Grenade grenade in player.inventory.grenades) {
						if (i < 8 + (int)ammoItemSlider && i >= (int)ammoItemSlider) {
							GUI.Box(new Rect(Screen.width/2+150, 125+(25*(i-(int)ammoItemSlider)), 50, 25), "$"+(grenade.price*buyMarkup).ToString());
							if (GUI.Button(new Rect(Screen.width/2, 125+(25*(i-(int)ammoItemSlider)), 150, 25), grenade.name)) {
								transferredGrenade = grenade;
								player.inventory.cash += (grenade.price*buyMarkup);
							}
						}
						i++;
					}
					
					if (transferredAnyAmmo) player.inventory.ammo[(int)transferredAmmo] --;
					if (transferredAnyAmmo) inventory.ammo[(int)transferredAmmo] ++;
					if (transferredGrenade != null) {
						inventory.grenades.Add(transferredGrenade);
						player.inventory.grenades.Remove(transferredGrenade);
					}

					
					ItemElements = i;
				}
			}
		}
	}
	
	public static bool checkForEmptyWeaponSlot (Weapon[] weapons) {
		foreach (Weapon weapon in weapons) {
			if (!weapon.IsValid) {
				return true;
			}
		}
		return false;
	}
	
	public static int findWeaponSlot (Weapon[] weapons) {
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].IsValid == false) {
				return i;
			}
		}
		return -1;
	}
}

public enum StoreMode {
	Buy,
	Sell
}