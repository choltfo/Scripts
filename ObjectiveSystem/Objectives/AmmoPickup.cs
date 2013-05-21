using UnityEngine;
using System.Collections;

/// <summary>
/// Pickup ammunition by interacting (default 'E') with a collider.
/// </summary>
public class AmmoPickup : Objective {
	
	public AmmoType ammoType	;
	public int Bullets 		= 100;
	public bool infinite 	= false;
	
	/// <summary>
	/// Interact with this pickup, yielding ammo. Returns new ammo seet.
	/// </summary>
	/// <param name='ammo'>
	/// The current amount of ammunition the player has.
	/// </param>
	public int[] Interact (int[] ammo) {
		if (!infinite) {
			Destroy(gameObject);
		}
		Complete();
		ammo[(int)ammoType] += Bullets;
		return ammo;
	}
}