using UnityEngine;
using System.Collections;

public class WeaponPickup : Objective {
	public Weapon thisGun;
	
	virtual public Weapon interact(){
		Complete ();
		gameObject.SetActive(false);
		return thisGun.duplicate();
	}
}