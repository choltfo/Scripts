using UnityEngine;
using System.Collections;

public class WeaponPickup : Objective {
	public Weapon thisGun;
	
	public Weapon interact(){
		Complete ();
		foreach( Transform trans in gameObject.transform) {
			Destroy(trans.gameObject);
		}
		Destroy(gameObject.GetComponent<BoxCollider>());
		Destroy(gameObject.GetComponent<MeshRenderer>());
		Destroy(gameObject.GetComponent<MeshFilter>());
		return thisGun;
		return thisGun;
	}
}