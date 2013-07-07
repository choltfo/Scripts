using UnityEngine;
using System.Collections;

public class ShootingEnemy : PathfindingEnemy {
	
	public Weapon weapon;
	public float semiAutoFireDelay = 0.25f;
	
	float lastShot = -10f;
	
	public Enemy target;
	
	public Enemy[] targets;
	
	public bool pastReady;
	
	public GameObject head;
	
	public AmmoType ammoType;
	public int startingReserveAmmo = 60;
	public int[] ammo;
	
	public WeaponPickup startingWeapon;
	
	public override void childStart () {
		Debug.Log ("childStart - ShootingEnemy");
		
		weapon = startingWeapon.thisGun;
		
		weapon.player = false;
		ammo = new int[7];
		//This is not going to last;
		//ammo = new int[typeof(AmmoType)];
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
				print ("Reloading");
			}
			if (!weapon.Automatic && Time.time > lastShot + semiAutoFireDelay) {
				print ("Attempting shot!");
				if (weapon.AIShoot(head)) {
					print ("Firing!");
				}
			}
			if (weapon.Automatic) {
				print ("Holding trigger!");
				weapon.AIShoot(head);
			}
		}
	}
	
	/// <summary>
	/// Lists the enemies.
	/// </summary>
	void listEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Combatant");
		targets = new Enemy[enemies.Length];
		int i =0;
		foreach (GameObject go in enemies) {
			targets[i] = go.GetComponent<Enemy>();
			i++;
		}
	}
}
