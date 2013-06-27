using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ShootObjects : MonoBehaviour {
	
	public Controls controls;
	public Melee punchController;
	public Inventory inventory;
	
	
	public float throwForce;
	public float throwTime = 0;
	public float throwMax = 50;

	public EnterKey vehicle;
	public float pickupDistance = 5;
	public int currentWeapon = 0;

	public void Start () {
		print("inventory.ammo types: " + Enum.GetNames(typeof(AmmoType)).Length);
	}
	
	public void Update () {
		
		if (Time.timeScale == 0) {
			return;
		}
		
		inventory.weapons[0].AnimUpdate();
		inventory.weapons[1].AnimUpdate();
		if (inventory.weapons[currentWeapon].Automatic) {
			if (Input.GetMouseButton((int)controls.fire)) {
				shoot();
			}
		}
		if (!inventory.weapons[currentWeapon].Automatic) {
			if (Input.GetMouseButtonDown((int)controls.fire)) {
				shoot();
			}
		}
		if (Input.GetMouseButtonDown((int)controls.aim)) {
			aim();
		}
		
		if (Input.GetKeyDown(controls.grenade)) {
			throwTime = Time.time;
		}

		if (Input.GetKeyUp(controls.grenade)) {
			throwForce = (Time.time - throwTime) * 20 + 10;
			if (throwForce > throwMax) {
				throwForce = throwMax;
			}
			throwGrenade();
		}
		
		if (Input.GetKeyDown(controls.flashlight)) {
			toggleFlashLights();
		}
		
		if (Input.GetKeyDown(controls.laser)) {
			toggleLasers();
		}
		
		if (Input.GetKeyDown(controls.melee)) {
			melee();
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
					inventory.weapons[0].stow();
					inventory.weapons[0].withdraw();
					inventory.weapons[1].stow();
				} else {
					currentWeapon = 1;
				}
			}
		}
		if (Input.GetKeyDown(controls.weapon1)) {
			if (currentWeapon != 1 || !inventory.weapons[1].Exists) {
				currentWeapon = 1;
				if (inventory.weapons[1].IsValid) {
					inventory.weapons[1].stow();
					inventory.weapons[1].withdraw();
					inventory.weapons[0].stow();
				} else {
					currentWeapon = 0;
				}
			}
		}
		
		if (Input.GetMouseButtonDown((int)controls.switchWeapons)) {
			ensuredSwitch ();
		}
	}
	
	public void toggleFlashLights() {
		foreach (HardPoint hp in inventory.weapons[currentWeapon].attachments) {
			if (hp.attachment.type == AttachmentType.Flashlight) hp.attachment.toggle();
		}
	}
	
	public void toggleLasers() {
		foreach (HardPoint hp in inventory.weapons[currentWeapon].attachments) {
			if (hp.attachment.type == AttachmentType.Laser) hp.attachment.toggle();
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
			if ((inventory.weapons[currentWeapon] as GrenadeLauncher) != null) {
				return ((GrenadeLauncher)inventory.weapons[currentWeapon]).Shoot(camera);
			}
			return inventory.weapons[currentWeapon].Shoot(camera);
		}
		return false;
	}
	
	public bool throwGrenade () {
		if (inventory.grenades.Count == 0) {
			return false;
		}
		inventory.grenades[0].throwGrenade(throwForce,transform);
		inventory.grenades.RemoveAt(0);
		return true;
	}
	
	public bool interact() {
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;
			if (Physics.Raycast(ray, out hit, pickupDistance)){
				if (hit.transform.gameObject.GetComponent<WeaponPickup>() != null ||
				hit.transform.gameObject.GetComponent<PickupGrenadeLauncher>() != null) {
					Weapon picked = (hit.transform.gameObject.GetComponent<PickupGrenadeLauncher>() != null) ?
						hit.transform.gameObject.GetComponent<PickupGrenadeLauncher>().interact():
						hit.transform.gameObject.GetComponent<WeaponPickup>().interact();
					int secondaryWeapon = 1;
					if (currentWeapon == 1) {
						secondaryWeapon = 0;
					} else if (currentWeapon == 0) {
						secondaryWeapon = 1;
					}
					if (inventory.weapons[currentWeapon].IsValid && inventory.weapons[secondaryWeapon].IsValid) {
						//Both slots full
						inventory.weapons[currentWeapon].destroy();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = picked;
						equip (currentWeapon);
						return true;
					} else if (!inventory.weapons[currentWeapon].IsValid && !inventory.weapons[secondaryWeapon].IsValid) {
						//Neither slot full
						inventory.weapons[currentWeapon].destroy();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = picked;
						equip (currentWeapon);
						return true;
					} else if (inventory.weapons[currentWeapon].IsValid && !inventory.weapons[secondaryWeapon].IsValid) {
						//Just current
						inventory.weapons[secondaryWeapon].destroy();
						inventory.weapons[secondaryWeapon].Drop();
						inventory.weapons[secondaryWeapon] = picked;
						equip (secondaryWeapon);
						return true;
					} else if (!inventory.weapons[currentWeapon].IsValid && inventory.weapons[secondaryWeapon].IsValid) {
						//Just other
						inventory.weapons[currentWeapon].destroy();
						inventory.weapons[currentWeapon].Drop();
						inventory.weapons[currentWeapon] = picked;
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
				if (hit.transform.gameObject.GetComponent<Store>() != null) {
					hit.transform.gameObject.GetComponent<Store>().Interact(this);
				} 
			}
		return false;
	}
	
	public bool equip(int weapon) {
		int original = currentWeapon;
		currentWeapon = weapon;
		if (inventory.weapons[weapon].IsValid) {
			inventory.weapons[original].stow();
			inventory.weapons[weapon].create(gameObject);
			inventory.weapons[weapon].withdraw();
			return true;
		}
		currentWeapon = original;
		return false;
	}
	
	public bool ensuredSwitch () {
		if (inventory.weapons[currentWeapon].IsValid) {
			inventory.weapons[currentWeapon].stow();
		}
		if (!inventory.weapons[0].IsValid && !inventory.weapons[1].IsValid) {
			return false;
		}
		if (inventory.weapons[0].IsValid && !inventory.weapons[1].IsValid) {
			currentWeapon = 0;
			inventory.weapons[currentWeapon].withdraw();
			return true;
		}
		if (!inventory.weapons[0].IsValid && inventory.weapons[1].IsValid) {
			currentWeapon = 1;
			inventory.weapons[currentWeapon].withdraw();
			return true;
		}
		if (inventory.weapons[0].IsValid && !inventory.weapons[1].IsValid) {
			currentWeapon = 0;
			inventory.weapons[currentWeapon].withdraw();
			return true;
		}
		if (inventory.weapons[0].IsValid && inventory.weapons[1].IsValid) {
			currentWeapon = (currentWeapon == 0) ? 1 : 0;
			inventory.weapons[currentWeapon].withdraw();
			return true;
		}
		return false;
	}
	
	public bool switchWeapons () {
		inventory.weapons[currentWeapon].stow();
		if (currentWeapon != 1 || !inventory.weapons[1].Exists) {
			currentWeapon = 1;
			if (inventory.weapons[1].IsValid) {
				inventory.weapons[1].stow();
				inventory.weapons[1].withdraw();
				inventory.weapons[0].stow();
				return true;
			} else {
				currentWeapon = 0;
				inventory.weapons[currentWeapon].withdraw();
				return false;
			}
		} else {
			if (currentWeapon != 0 || !inventory.weapons[0].Exists) {
				currentWeapon = 0;
				if (inventory.weapons[0].IsValid) {
					inventory.weapons[0].stow();
					inventory.weapons[0].withdraw();
					inventory.weapons[1].stow();
					return true;
				} else {
					currentWeapon = 1;
					inventory.weapons[currentWeapon].withdraw();
					return false;
				}
			}
			return false;
		}	
	}
	
	public void melee() {
		//punchController.GAH!
	}

	public void OnGUI () {
		if (inventory.weapons[currentWeapon].IsValid) {
			GUI.Box(new Rect(Screen.width-175,Screen.height-100,150,50),"");
			GUI.Label(new Rect(Screen.width-150, Screen.height-100, 150, 40), inventory.weapons[currentWeapon].WeaponName);
			GUI.Label(new Rect(Screen.width-150, Screen.height-75, 150, 40),
				inventory.weapons[currentWeapon].CurAmmo + "/" + inventory.weapons[currentWeapon].MaxAmmo + "/" + inventory.ammo[(int)inventory.weapons[currentWeapon].ammoType]);
			if (!inventory.weapons[currentWeapon].isAimed) {
				GUI.Box(new Rect(Screen.width/2-2,Screen.height/2-2,4,4),"");
			}
		}
	}
}