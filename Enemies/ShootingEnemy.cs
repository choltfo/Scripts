using UnityEngine;
using System;
using System.Collections.Generic;

public class ShootingEnemy : PathfindingEnemy {
	
	public Weapon weapon;
	public float semiAutoFireDelay = 0.25f;
	
	float lastShot = -10f;
	
	public bool pastReady;
	
	public GameObject head;
	
	public AmmoType ammoType;
	public int startingReserveAmmo = 60;
	public int[] ammo;
	
	public WeaponPickup startingWeapon;
	
	public bool isAimed = false;
	
	
	public float rotSpd = 5;
	
	public static bool debug = true;
	
	
	Quaternion rotation;
	
	
	
	public override void childStart () {
		
		weapon = startingWeapon.thisGun.duplicate();
		
		weapon.player = false;
		ammo = new int[Enum.GetValues(typeof(AmmoType)).Length];
		ammo[(int)ammoType] = startingReserveAmmo;
		
		head = transform.FindChild("head").gameObject;
		weapon.create(head);
		weapon.withdraw();
		
		targets = listEnemies();
		print("Targets: " + targets.Count);
	}
	
	public override void childFixedUpdate () {
		weapon.AnimUpdate();
		
		if (!alerted) checkAnyVisible();
		
		if (ready && alerted && isAimed) {
			//target = getNearestEnemy();
			if (weapon.CurAmmo == 0 && weapon.AnimClock == 0) {
				weapon.Reload(ammo);
				if (debug) print ("Reloading");
			}
			if (!weapon.Automatic && Time.time > lastShot + semiAutoFireDelay) {
				if (debug) print ("Attempting shot!");
				if (weapon.AIShoot(head)) {
					if (debug) print ("Firing!");
				}
			}
			if (weapon.Automatic) {
				if (debug) print ("Holding trigger!");
				weapon.AIShoot(head);
			}
			if (debug) print("Targeting "+target.name);
		}
		
		//Look towards predetermined target
		if (target != null) {
			Vector3 targetPos = target.transform.position;
			Vector3 currentPos = head.transform.position;
			Vector3 relativePos = targetPos - currentPos ;
			rotation = Quaternion.LookRotation(relativePos);
			if (!isAimed) head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, Time.deltaTime * rotSpd);
		}
																	// Satisfactory aiming criteria. In degrees.
		isAimed = Quaternion.Angle(head.transform.rotation, rotation) < 5f;
	}
	
	Enemy getNearestEnemy() {
		int nearest = 0;
		float closest = 1000;
		
		int i = 0;
		
		// So, warning, this will make your enemy shoot himself after killing all other enemies. 
		
		foreach (Enemy e in targets) {
			if (Vector3.Distance(gameObject.transform.position, targets[i].transform.position) < closest &&
					!e.faction.Equals(faction) && e != this) {
				
				if (debug) print ("Contemplating " + e.name);
				nearest = i;
				closest = Vector3.Distance(gameObject.transform.position, targets[i].transform.position);
			}
			
			i ++;
		}
		if (debug) print("Nearest enemy is " + targets[nearest].name);
		return targets[nearest];
	}
}
