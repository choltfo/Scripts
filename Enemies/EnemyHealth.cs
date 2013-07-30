using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The health level of any enemy.
/// </summary>

public class EnemyHealth : Objective {
	/// <summary>
	/// The health level of the enemy.
	/// </summary>
	public float Health = 100;
	
	public float crashThreshhold = 50;
	
	Enemy thisEnemy;
	
	void Start() {
		thisEnemy = GetComponent<Enemy>();
	}
	
	void onCollisionEnter(Collision C) {
		if (C.relativeVelocity.magnitude > crashThreshhold) {
			print(damage(Mathf.Pow ((C.relativeVelocity.magnitude - crashThreshhold)/2,2)));
		}
	}
	
	public float damage (float damage, DamageCause COD = DamageCause.Default) {
		if (Health > damage) {
			Health -= damage;
		} else {
			Health = 0;
			kill (COD);
		}
		return damage;
	}
	
	void kill (DamageCause COD = DamageCause.Default) {
		print (gameObject.name + " suffered a death by " + COD.ToString());
		
		if (GetComponent<ShootingEnemy>() != null) {
			GetComponent<ShootingEnemy>().weapon.Drop ();
			GameObject ammo = GameObject.CreatePrimitive(PrimitiveType.Cube);
			ammo.name = "DroppedAmmo";
			ammo.transform.Translate(transform.position);
			ammo.transform.Translate(0f,0.5f,0f);
			ammo.AddComponent(typeof(AmmoPickup));
			ammo.GetComponent<AmmoPickup>().ammoType = GetComponent<ShootingEnemy>().ammoType;//GetComponent<ShootingEnemy>().ammo
			ammo.GetComponent<AmmoPickup>().Bullets = GetComponent<ShootingEnemy>().ammo[(int)GetComponent<ShootingEnemy>().ammoType];
		}
		
		List<Enemy> es = PathfindingEnemy.listEnemies();
		es.Remove(thisEnemy);
		PathfindingEnemy.setTargets(PathfindingEnemy.listEnemies());
		Destroy (gameObject);
	}
}


