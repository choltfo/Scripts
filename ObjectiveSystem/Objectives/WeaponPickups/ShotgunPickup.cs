using UnityEngine;
using System.Collections;

public class ShotgunPickup : WeaponPickup {
	public Shotgun thisShotgun;
	
	public Shotgun interact(){
		Complete ();
		foreach( Transform trans in gameObject.transform) {
			Destroy(trans.gameObject);
		}
		Destroy(gameObject.GetComponent<BoxCollider>());
		Destroy(gameObject.GetComponent<MeshRenderer>());
		Destroy(gameObject.GetComponent<MeshFilter>());
		return thisShotgun;
	}
}