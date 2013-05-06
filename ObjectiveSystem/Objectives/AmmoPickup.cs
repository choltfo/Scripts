using UnityEngine;
using System.Collections;

public class AmmoPickup : Objective {
	
	public AmmoType ammoType	;
	public int Bullets 		= 100;
	public bool infinite 	= false;
	
	public int[] Interact (int[] ammo) {
		if (!infinite) {
			Destroy(gameObject);
		}
		Complete();
		ammo[(int)ammoType] += Bullets;
		return ammo;
	}
}