using UnityEngine;
using System.Collections;

public class PickupGrenadeLauncher : Objective {
	public GrenadeLauncher thisGun;
	
	virtual public Weapon interact(){
		Complete ();
		gameObject.SetActive(false);
		return (Weapon)thisGun;	
	}
}
