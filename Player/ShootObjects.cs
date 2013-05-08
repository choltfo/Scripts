using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ShootObjects : MonoBehaviour {
	
	public Controls controls;
	
	public Inventory inventory;
	
	public EnterKey vehicle;
	public float pickupDistance = 5;
	public int currentWeapon = 0;
	//public  Weapon[] weapons = {new Weapon(), new Weapon()};
	//public int[] ammo;
	//public List<Grenade> grenades  = new List<Grenade>();
	
	public void Start () {
		inventory.ammo = new int[Enum.GetNames(typeof(AmmoType)).Length];
		print("inventory.ammo types: " + Enum.GetNames(typeof(AmmoType)).Length);
		for (int i = 0; i < Enum.GetNames(typeof(AmmoType)).Length; i++) {
			inventory.ammo[i] = 0;
		}	
	
	}
	
	public void Update () {
		
		if (Time.timeScale == 0) {
			return;
		}
		
		inventory.weapons[currentWeapon].AnimUpdate();
		if (inventory.weapons[currentWeapon].Automatic == true) {
			if (Input.GetMouseButton((int)controls.fire)) {
				shoot();
			}
		}
		if (inventory.weapons[currentWeapon].Automatic == false) {
			if (Input.GetMouseButtonDown((int)controls.fire)) {
				shoot();
			}
		}
		if (Input.GetMouseButtonDown((int)controls.aim)) {
			aim();
		}
		
		if (Input.GetKeyDown(KeyCode.G)) {
			throwGrenade();
		}
		
		if (Input.GetKeyDown(controls.reload)) {
			reload();
		}
		if (Input.GetKeyDown(controls.interact)) {
			interact();
		}
		if (Input.GetKeyDown(controls.drop)) {
			if (inventory.weapons[currentWeapon].IsValid) {
				inventory.weapons[currentWeapon].Drop();
				inventory.weapons[currentWeapon] = new Weapon();
				inventory.weapons[currentWeapon].IsValid = false;
			}
		}
		if (Input.GetKeyDown(controls.weapon0)) {
			if (currentWeapon != 0 || !inventory.weapons[0].Exists) {
				currentWeapon = 0;
				if (inventory.weapons[0].IsValid) {
					inventory.weapons[0].deactivate();
					inventory.weapons[0].activate(gameObject);
					inventory.weapons[1].deactivate();
				} else {
					currentWeapon = 1;
				}
			}
		}
		if (Input.GetKeyDown(controls.weapon1)) {
			if (currentWeapon != 1 || !inventory.weapons[1].Exists) {
				currentWeapon = 1;
				if (inventory.weapons[1].IsValid) {
					inventory.weapons[1].deactivate();
					inventory.weapons[1].activate(gameObject);
					inventory.weapons[0].deactivate();
				} else {
					currentWeapon = 0;
				}
			}
		}
		if (Input.GetMouseButtonDown((int)controls.switchWeapons)) {
			if (currentWeapon != 1 || !inventory.weapons[1].Exists) {
				currentWeapon = 1;
				if (inventory.weapons[1].IsValid) {
					inventory.weapons[1].deactivate();
					inventory.weapons[1].activate(gameObject);
					inventory.weapons[0].deactivate();
				} else {
					currentWeapon = 0;
				}
			} else {
				if (currentWeapon != 0 || !inventory.weapons[0].Exists) {
					currentWeapon = 0;
					if (inventory.weapons[0].IsValid) {
						inventory.weapons[0].deactivate();
						inventory.weapons[0].activate(gameObject);
						inventory.weapons[1].deactivate();
					} else {
						currentWeapon = 1;
					}
				}
			}
		}
	}

	public bool aim() {		
		inventory.weapons[currentWeapon].aim ();
		return false;
	}
	
	public void reload() {
		inventory.ammo = inventory.weapons[currentWeapon].Reload(inventory.ammo);
	}
	
	public bool shoot() {
		if (inventory.weapons[currentWeapon].IsValid) {
			return inventory.weapons[currentWeapon].Shoot(camera);
		}
		return false;
	}
	
	public bool throwGrenade () {
		if (inventory.grenades.Count == 0) {
			return false;
		}
		inventory.grenades[0].throwGrenade(100,transform);
		inventory.grenades.RemoveAt(0);
		return true;
	}
	
	/*public bool shootUnderbarrel() {
		if (inventory.weapons[currentWeapon].underbarrel.IsValid) {
			return inventory.weapons[currentWeapon].underbarrel.Shoot(inventory.weapons[currentWeapon].mainObject);
		}
		return false;	
	}*/
	
	public bool interact() {
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;
			if (Physics.Raycast(ray, out hit, pickupDistance)){
				if (hit.transform.gameObject.GetComponent<WeaponPickup>() != null) {
					int secondaryWeapon = 1;
					if (currentWeapon == 1) {
						secondaryWeapon = 0;
					} else if (currentWeapon == 0) {
						secondaryWeapon = 1;
					}
					if (inventory.weapons[currentWeapon].IsValid && inventory.weapons[secondaryWeapon].IsValid) {
						//Both slots full
						inventory.weapons[currentWeapon].deactivate();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (!inventory.weapons[currentWeapon].IsValid && !inventory.weapons[secondaryWeapon].IsValid) {
						//Neither slot full
						inventory.weapons[currentWeapon].deactivate();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (inventory.weapons[currentWeapon].IsValid && !inventory.weapons[secondaryWeapon].IsValid) {
						//Just current
						inventory.weapons[secondaryWeapon].deactivate();
						inventory.weapons[secondaryWeapon].Drop();
						inventory.weapons[secondaryWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (secondaryWeapon);
						return true;
					} else if (!inventory.weapons[currentWeapon].IsValid && inventory.weapons[secondaryWeapon].IsValid) {
						//Just other
						inventory.weapons[currentWeapon].deactivate();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					}
				}
				if (hit.transform.gameObject.GetComponent<AmmoPickup>() != null) {
					inventory.ammo = hit.transform.gameObject.GetComponent<AmmoPickup>().Interact(inventory.ammo);
				}
				if (hit.transform.gameObject.GetComponent<PickupObjective>() != null) {
					hit.transform.gameObject.GetComponent<PickupObjective>().Interact();
				}
				if (hit.transform.gameObject.GetComponent<InteractObject>() != null) {
					hit.transform.gameObject.GetComponent<InteractObject>().Interact(gameObject.transform.parent.gameObject);
				}
				if (hit.transform.gameObject.GetComponent<Vehicle>() != null) {
					hit.transform.gameObject.GetComponent<Vehicle>().activate(gameObject);
				} 
			}
		return false;
	}
	
	public bool equip(int weapon) {
		int original = currentWeapon;
		currentWeapon = weapon;
		if (inventory.weapons[weapon] != null) {
			inventory.weapons[original].deactivate();
			inventory.weapons[weapon].activate(gameObject);
			return true;
		}
		currentWeapon = original;
		return false;
	}

	public void OnGUI () {
		if (inventory.weapons[currentWeapon].IsValid) {
			GUI.Box(new Rect(Screen.width-175,Screen.height-100,150,50),"");
			GUI.Label(new Rect(Screen.width-150, Screen.height-100, 150, 40), inventory.weapons[currentWeapon].WeaponName);
			GUI.Label(new Rect(Screen.width-150, Screen.height-75, 150, 40),
				inventory.weapons[currentWeapon].CurAmmo + "/" + inventory.weapons[currentWeapon].MaxAmmo + "/" + inventory.weapons[currentWeapon].ReserveAmmo);
			if (!inventory.weapons[currentWeapon].isAimed) {
				GUI.Box(new Rect(Screen.width/2-2,Screen.height/2-2,4,4),"");
			}
		}
	}
}