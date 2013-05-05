using UnityEngine;
using System.Collections;

public class AmmoPickup : Objective {
	
	public string gunType	= "SCAR-H";
	public int Bullets 		= 100;
	public bool infinite 	= false;
	
	public void Interact (Weapon[] weapons) {
		if (!infinite) {
			Destroy(gameObject);
		}
		Complete();
		if (weapons[0].WeaponName == gunType) {
			weapons[0].ReserveAmmo += Bullets;
			return;
		}
		
		if (weapons[1].WeaponName == gunType) {
			weapons[1].ReserveAmmo += Bullets;
			return;
		}
		return;
	}
}
