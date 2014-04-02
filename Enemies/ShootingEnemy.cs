using UnityEngine;
using System;
using System.Collections.Generic;

public class ShootingEnemy : PathfindingEnemy {
	
	public Weapon weapon;

	public ThoughtProcess TP;

	public float semiAutoFireDelay = 0.25f;
	
	float lastShot = -10f;
	
	public bool pastReady;
	
	public GameObject head;
	
	public AmmoType ammoType;
	public int startingReserveAmmo = 60;
	public int[] ammo;
	
	public WeaponPickup startingWeapon;
	public PickupGrenadeLauncher startingWeaponGL;
	
	public bool isAimed = false;
	
	
	public float rotSpd = 5f;
	public float scopedRotSpd = 1f;
	public float satisfactoryAimInDegrees = 10;
	
	public float lastVisCheck = 0f;
	public float betweenVisCheck = 0.5f;

	// Okay, so, for snipers, this should be really high, since this should be a factor of weapon recoil.
	public float timeBeforeRecenter = 0.25f;


	
	Quaternion rotation;

	bool aimedAtWall = false;
	
	
	
	public override void childStart () {



		if (startingWeaponGL == null) weapon = startingWeapon.thisGun.duplicate();
		if (startingWeaponGL != null) weapon = startingWeaponGL.thisGun.duplicate(); 
		
		weapon.player = false;
		ammo = new int[Enum.GetValues(typeof(AmmoType)).Length];
		ammo[(int)ammoType] = startingReserveAmmo;
		
		//head = transform.FindChild("head").gameObject;
		weapon.create(head, false);
		weapon.withdraw();
		
		targets = listEnemies();
		if (debug) print("Targets: " + targets.Count);
	}
	
	/*public virtual void checkAnyVisible () {
		foreach (Enemy e in targets) {
			if (e.faction != faction) {
				if (debug) print ("Checking to see if alerted by " +  e.name);
				var rayDirection = e.transform.position - transform.position;
				if (Vector3.Angle(rayDirection, transform.forward) < fieldOfViewRadiusInDegrees) {
					if (debug) print ("Angle satisfied.");
					if (Vector3.Distance(transform.position, e.transform.position) < visionRange) {
						if (debug) print ("Distance satisfied.");
						//RaycastHit hitinfo;
						//Physics.Linecast (head.transform.position, e.transform.position, hitinfo);
						//if (hitinfo.transform = e.transform) {
						//	if (debug) print ("Linecast satisfied.");
						alerted = true;
						target = e;
						if (debug) print ("Found target. Starting to kill!");
						//}
					}
				}
			}
		}
	}*/
	
	public override void childFixedUpdate () {
		weapon.AnimUpdate();
		
		float angle = Quaternion.Angle(head.transform.rotation, rotation);
		isAimed = angle < satisfactoryAimInDegrees;
		
		//Look towards predetermined target, and handle crouching
		if (target != null) {
			if (debug) print("Prepping to considering crouching....");
			if (PFNC.currentNode.type == PFNodeType.Crouch && ready) {
				if (debug) print("Considering crouching....");
					// GET DOWN!
					if (weapon.CurAmmo == 0) {	// If my gun is empty, duck while reloading.
						if (debug) print("Crouching....");
						transform.lossyScale.Set(1f,0.5f,1f);	// THIS CROUCHES IN THEORY!
					} else {
						if (debug) print("Not crouching");
						transform.lossyScale.Set(1f,1f,1f);
					}
				}
			
			Vector3 targetPos = target.transform.position;
			Vector3 currentPos = head.transform.position;
			Vector3 relativePos = targetPos - currentPos ;
			rotation = Quaternion.LookRotation(relativePos);
			if (!isAimed){

				if (Time.time > weapon.lastShot + timeBeforeRecenter) head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, Time.deltaTime * rotSpd);

				if (weapon.isAimed) {
					weapon.aim();
				}
			} else {
				if (debug) print (gameObject.name + " is aimed and ready.");
				if (Time.time > weapon.lastShot + timeBeforeRecenter) head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, Time.deltaTime * scopedRotSpd);
				// head.transform.rotation = rotation;
				//if (angle < 0.5) head.transform.rotation = rotation;
				if (!weapon.isAimed) {
					weapon.aim();
				}
			}

			if (lastVisCheck + betweenVisCheck < Time.time) {
				RaycastHit hit;
				if (debug) print ("Checking if aimed at wall.");
				lastVisCheck = Time.time;
				if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, visionRange)) {
					aimedAtWall = hit.collider.gameObject != target.gameObject;
					if (debug) print ("Might be aimed at wall.....");
				}
			}

		}


		
			// Weapon handling.
		if (ready && alerted && isAimed && !aimedAtWall) {
			//target = getNearestEnemy();
			if (weapon.CurAmmo == 0 && weapon.actionHasReset()) {
				weapon.Reload(ammo);
				if (debug) print ("Reloading");
			}
			if (!weapon.Automatic && Time.time > lastShot + semiAutoFireDelay) {
				if (debug) print ("Attempting shot!");
				if (weapon.AIShoot(head, this)) {
					lastShot = Time.time;
					if (debug) print ("Firing!");
				}
			}
			if (weapon.Automatic) {
				if (debug) print ("Holding trigger!");
				weapon.AIShoot(head, this);
			}
			if (debug) print("Targeting "+target.name);
		}
	}

	public Enemy getNearestEnemy() {
		int nearest = 0;
		float closest = 1000;
		
		int i = 0;
		
		// Change plan:
		// The enemies should calculate every iteration of the loop in a different frame to reduce lag, and create realistically slow enemies.
		
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
