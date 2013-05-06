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
	public GameObject player;
	
	public void start () {}
	
	public void Update () {
		
		if (Time.timeScale == 0) {
			return;
		}
		
		weapons[currentWeapon].AnimUpdate();
		if (weapons[currentWeapon].Automatic == true) {
			if (Input.GetMouseButton((int)controls.fire)) {
				shoot();
			}
		}
		if (weapons[currentWeapon].Automatic == false) {
			if (Input.GetMouseButton((int)controls.fire)) {
				shoot();
			}
		}
		if (Input.GetMouseButtonDown((int)controls.aim)) {
			aim();
		}
		if (Input.GetKeyDown(controls.reload)) {
			reload();
		}
		if (Input.GetKeyDown(controls.interact)) {
			interact();
		}
		if (Input.GetKeyDown(controls.drop)) {
			if (weapons[currentWeapon].IsValid) {
				weapons[currentWeapon].Drop();
				weapons[currentWeapon] = new Weapon();
				weapons[currentWeapon].IsValid = false;
			}
		}
		if (Input.GetKeyDown(controls.weapon0)) {
			if (currentWeapon != 0 || !weapons[0].Exists) {
				currentWeapon = 0;
				if (weapons[0].IsValid) {
					weapons[0].deactivate();
					weapons[0].activate(gameObject);
					weapons[1].deactivate();
				} else {
					currentWeapon = 1;
				}
			}
		}
		if (Input.GetKeyDown(controls.weapon1)) {
			if (currentWeapon != 1 || !weapons[1].Exists) {
				currentWeapon = 1;
				if (weapons[1].IsValid) {
					weapons[1].deactivate();
					weapons[1].activate(gameObject);
					weapons[0].deactivate();
				} else {
					currentWeapon = 0;
				}
			}
		}
		if (Input.GetMouseButtonDown((int)controls.switchWeapons)) {
			if (currentWeapon != 1 || !weapons[1].Exists) {
				currentWeapon = 1;
				if (weapons[1].IsValid) {
					weapons[1].deactivate();
					weapons[1].activate(gameObject);
					weapons[0].deactivate();
				} else {
					currentWeapon = 0;
				}
			} else {
				if (currentWeapon != 0 || !weapons[0].Exists) {
					currentWeapon = 0;
					if (weapons[0].IsValid) {
						weapons[0].deactivate();
						weapons[0].activate(gameObject);
						weapons[1].deactivate();
					} else {
						currentWeapon = 1;
					}
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
						weapons[currentWeapon].deactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (!weapons[currentWeapon].IsValid && !weapons[secondaryWeapon].IsValid) {
						//Neither slot full
						weapons[currentWeapon].deactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					} else if (weapons[currentWeapon].IsValid && !weapons[secondaryWeapon].IsValid) {
						//Just current
						weapons[secondaryWeapon].deactivate();
						weapons[secondaryWeapon].Drop();
						weapons[secondaryWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (secondaryWeapon);
						return true;
					} else if (!weapons[currentWeapon].IsValid && weapons[secondaryWeapon].IsValid) {
						//Just other
						weapons[currentWeapon].deactivate();
						weapons[currentWeapon].Drop();
						weapons[currentWeapon] = ((WeaponPickup)hit.transform.gameObject.GetComponent("WeaponPickup")).interact();
						equip (currentWeapon);
						return true;
					}
				}
				if (hit.transform.gameObject.GetComponent<AmmoPickup>() != null) {
					hit.transform.gameObject.GetComponent<AmmoPickup>().Interact(weapons);
				}
				if (hit.transform.gameObject.GetComponent<PickupObjective>() != null) {
					hit.transform.gameObject.GetComponent<PickupObjective>().Interact();
				}
				if (hit.transform.gameObject.GetComponent<InteractObject>() != null) {
					hit.transform.gameObject.GetComponent<InteractObject>().Interact(player);
				} 
			}
		return false;
	}
	
	public bool equip(int weapon) {
		int original = currentWeapon;
		currentWeapon = weapon;
		if (weapons[weapon] != null) {
			weapons[original].deactivate();
			weapons[weapon].activate(gameObject);
			return true;
		}
		currentWeapon = original;
		return false;
	}

	public void OnGUI () {
		if (weapons[currentWeapon].IsValid) {
			GUI.Box(new Rect(Screen.width-175,Screen.height-100,150,50),"");
			GUI.Label(new Rect(Screen.width-150, Screen.height-100, 150, 40), weapons[currentWeapon].WeaponName);
			GUI.Label(new Rect(Screen.width-150, Screen.height-75, 150, 40),
				weapons[currentWeapon].CurAmmo + "/" + weapons[currentWeapon].MaxAmmo + "/" + weapons[currentWeapon].ReserveAmmo);
			if (!weapons[currentWeapon].isAimed) {
				GUI.Box(new Rect(Screen.width/2-2,Screen.height/2-2,4,4),"");
			}
		}
	}
}