using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ShootObjects : MonoBehaviour {
	
	public Controls controls;

	public EnterKey vehicle;
	public float pickupDistance = 5;
	public  Weapon[] weapons = {new Weapon(), new Weapon()};
	public int currentWeapon = 0;
	
	public void start () {}
	
/*	public void OnGUI () {
		if (weapons[currentWeapon] != null) {
			GUI.label(new Rect(Screen.width-weapons[currentWeapon].WeaponName.toStringArray().Length*20,50,400,20), weapons[currentWeapon].WeaponName);
			GUI.label(new Rect(), weapons[currentWeapon].CurAmmo + "/" + weapons[currentWeapon].MaxAmmo);
		}
	}
	*/
	public void Update () {
		
		if (Time.timeScale == 0) {
			return;
		}
		
		weapons[currentWeapon].AnimUpdate();
		if (weapons[currentWeapon].Automatic == true) {
			if (Input.GetKey(controls.fire)) {
				shoot();
			}
		}
		if (weapons[currentWeapon].Automatic == false) {
			if (Input.GetKeyDown(controls.fire)) {
				shoot();
			}
		}
		if (Input.GetKeyDown(controls.aim)) {
			aim();
		}
		if (Input.GetKeyDown(controls.reload)) {
			reload();
		}
		if (Input.GetKeyDown(controls.interact)) {
			interact();
		}
		if (Input.GetKeyDown(controls.weapon0)) {
			if (currentWeapon != 0 || !weapons[0].Exists) {
				currentWeapon = 0;
				if (weapons[0].IsValid) {
					weapons[0].disactivate();
					weapons[0].activate(gameObject);
					weapons[1].disactivate();
				} else {
					currentWeapon = 1;
				}
			}
		}
		if (Input.GetKeyDown(controls.weapon1)) {
			if (currentWeapon != 1 || !weapons[1].Exists) {
				currentWeapon = 1;
				if (weapons[1].IsValid) {
					weapons[1].disactivate();
					weapons[1].activate(gameObject);
					weapons[0].disactivate();
				} else {
					currentWeapon = 0;
				}
			}
		}
	}

	public bool aim() {		
		weapons[currentWeapon].aim ();
		return false;
	}
	
	public void reload() {
		weapons[currentWeapon].Reload();
	}
	
	public bool shoot() {
		if (weapons[currentWeapon].IsValid) {
			return weapons[currentWeapon].Shoot(camera);
		}
		return false;
	}
	
	/*public bool shootUnderbarrel() {
		if (weapons[currentWeapon].underbarrel.IsValid) {
			return weapons[currentWeapon].underbarrel.Shoot(weapons[currentWeapon].mainObject);
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
					if (weapons[currentWeapon].IsValid && weapons[secondaryWeapon].IsValid) {
						//Both slots full
						weapons[currentWeapon].disactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (!weapons[currentWeapon].IsValid && !weapons[secondaryWeapon].IsValid) {
						//Neither slot full
						weapons[currentWeapon].disactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (weapons[currentWeapon].IsValid && !weapons[secondaryWeapon].IsValid) {
						//Just current
						weapons[secondaryWeapon].disactivate();
						weapons[secondaryWeapon].Drop();
						weapons[secondaryWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (secondaryWeapon);
						return true;
					} else if (!weapons[currentWeapon].IsValid && weapons[secondaryWeapon].IsValid) {
						//Just other
						weapons[currentWeapon].disactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					}
				}
				if (hit.transform.gameObject.GetComponent<PickupObjective>() != null) {
					hit.transform.gameObject.GetComponent<PickupObjective>().Interact();
				}
				if (hit.transform.gameObject.GetComponent<Interact>() != null) {
					hit.transform.gameObject.GetComponent<Interact>().interact();
				} 
			}
		return false;
	}
	
	public bool equip(int weapon) {
		int original = currentWeapon;
		currentWeapon = weapon;
		if (weapons[weapon] != null) {
			weapons[original].disactivate();
			weapons[weapon].activate(gameObject);
			return true;
		}
		currentWeapon = original;
		return false;
	}

	public void OnGUI () {
		if (weapons[currentWeapon].IsValid) {
			GUI.Box(new Rect(Screen.width-200,Screen.height-100,200,100),"");
			GUI.Label(new Rect(Screen.width-200, Screen.height-100, 200, 40), weapons[currentWeapon].WeaponName);
			GUI.Label(new Rect(Screen.width-200, Screen.height-50, 200, 40),
				weapons[currentWeapon].CurAmmo + "/" + weapons[currentWeapon].MaxAmmo + "/" + weapons[currentWeapon].ReserveAmmo);
		}
	}
}