using UnityEngine;
using System.Collections;

public class WeaponPickup : Objective {
	public Weapon thisGun;
	
	public Weapon interact(){
		foreach( Transform trans in gameObject.transform) {
			Destroy(trans.gameObject);
		}
		Complete ();
		return thisGun;
	}
	
}