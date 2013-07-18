using UnityEngine;
using System.Collections.Generic.List;
using System;

public class ShootingEnemy : PathfindingEnemy {
	
	public Weapon weapon;
	public float semiAutoFireDelay = 0.25f;
	
	float lastShot = -10f;
	
	public Enemy target;
	
	public List<Enemy> targets;
	
	public bool pastReady;
	
	public GameObject head;
	
	public AmmoType ammoType;
	public int startingReserveAmmo = 60;
	public int[] ammo;
	
	public WeaponPickup startingWeapon;
	
	public bool debug = false;
	
	public override void childStart () {
		Debug.Log ("childStart - ShootingEnemy");
		
		weapon = startingWeapon.thisGun.duplicate();
		
		weapon.player = false;
		ammo = new int[Enum.GetValues(typeof(AmmoType)).Length];
		ammo[(int)ammoType] = startingReserveAmmo;
		
		head = transform.FindChild("head").gameObject;
		weapon.create(head);
		weapon.withdraw();
		
		listEnemies();
	}
	
	public override void childFixedUpdate () {
		weapon.AnimUpdate();
		if (ready) {
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
		}
		
		head.transform.LookAt(getNearestEnemy().transform.position);
	}
	
	Enemy getNearestEnemy() {
		int nearest = 0;
		float closest = 1000;
		
		for (int i = 0; i < targets.Length; i++) {
			if (Vector3.Distance(gameObject.transform.position, targets[i].transform.position) < closest) {
				nearest = i;
				closest = Vector3.Distance(gameObject.transform.position, targets[i].transform.position);
			}
		}
		
	}
	
	/// <summary>
	/// Lists the enemies.
	/// </summary>
	void listEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Combatant");
		targets = new Enemy[enemies.Length];
		int i = 0;
		foreach (GameObject go in enemies) {
			targets[i] = go.GetComponent<Enemy>();
			i++;
		}
	}
}
