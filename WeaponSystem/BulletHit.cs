using UnityEngine;
using System.Collections;

public class BulletHit {
	
	public float Damage;
	public RaycastHit hit;
	public float maxRange;
	public Enemy shooter;
	public Vector3 origin; // For calculating drag.
	public AmmoType caliber = AmmoType.Parabellum9x19mm;	// For calculating penetration.
	
	public GameObject DirtSpray;
	public GameObject BloodSpray;
	public GameObject BulletHole;
	
	public float HitStrength;
	
	
	public static bool debug = false; 
	
	/// <summary>
	/// Calculates the collision of a bullet and a penetrable object, e.g. cover.
	/// </summary>
	/// <returns>
	/// The bullet the bounces off or passes through the target.
	/// </returns>
	public BulletHit calcHit () {
		return this;
	}
	
	public void calculateDamage () {
		
		
		
		
		
		//Debug.Log("Hit " + hit.collider.gameObject.name);
		
		if (hit.collider.gameObject.Equals(shooter.gameObject)) return;
		
		Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);	
			if (hit.transform.gameObject.GetComponent<Rigidbody>() != null) {
				hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(hit.normal * -HitStrength);
			}					 
			if (hit.transform.tag == "Explosive") {
				if ((Detonator)hit.transform.gameObject.GetComponent("Detonator") != null) {
					Detonator target = (Detonator)hit.transform.gameObject.GetComponent("Detonator");
					try {
						target.Explode();
					} catch (System.SystemException e) {
						Debug.LogError("Detonator failed inside Weapon");
						Debug.LogError (e.ToString());
					}
				}
				if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
					AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
					targetSound.Play();
				}
				if (hit.transform.gameObject.GetComponent<ExplosiveDamage>() != null) {
					hit.transform.gameObject.GetComponent<ExplosiveDamage>().explode();	
				}
			} else if (hit.transform.tag == "Combatant") {
				if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
					AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
					targetSound.Play();
				}
				if (hit.transform.gameObject.GetComponent("EnemyHealth") != null) {
					EnemyHealth enemyHealth = (EnemyHealth)hit.transform.gameObject.GetComponent("EnemyHealth");
					enemyHealth.damageAsCombatant(Damage, shooter, DamageCause.Shot);
					if (debug) MonoBehaviour.print("Dealt " + Damage.ToString() + " Damage to " + hit.transform.gameObject.name);
				}
				if (hit.transform.gameObject.GetComponent<Health>() != null) {
					Health enemyHealth = hit.transform.gameObject.GetComponent<Health>();
					enemyHealth.Damage(Damage, DamageCause.Shot);
					if (debug) MonoBehaviour.print("Dealt " + Damage.ToString() + " Damage to " + hit.transform.gameObject.name);
				}
				if (hit.transform.FindChild("Camera") != null) {
					if (hit.transform.FindChild("Camera").gameObject.GetComponent<Health>() != null) {
						Health enemyHealth = hit.transform.FindChild("Camera").gameObject.GetComponent<Health>();
						enemyHealth.Damage(Damage, DamageCause.Shot);
						if (debug) MonoBehaviour.print("Dealt " + Damage.ToString() + " Damage to " + hit.transform.gameObject.name);
					}
				}
				GameObject newBlood = (GameObject)MonoBehaviour.Instantiate(BloodSpray, hit.point, hitRotation);
				newBlood.transform.parent = hit.transform;
				newBlood.transform.Translate(0,(float)0.05,0);
			} else if (hit.transform.gameObject.GetComponent<ShatterableGlass>() != null) {
				ShatterableGlass pane = hit.transform.gameObject.GetComponent<ShatterableGlass>();
				pane.shoot(hit, HitStrength);
				if (debug) MonoBehaviour.print("Shot glass pane " + pane.name);
			} else if (hit.transform.gameObject.GetComponent<SplashingWater>() != null) {
				SplashingWater pane = hit.transform.gameObject.GetComponent<SplashingWater>();
				pane.Splash(hit.point);
				if (debug) MonoBehaviour.print("Shot Water " + pane.name);
			} else if (hit.transform.gameObject.GetComponent<PenetrableCover>() != null) {
				if (debug) MonoBehaviour.print("Shot penetrable target " + hit.transform.gameObject.name);
				BulletHit b = hit.transform.gameObject.GetComponent<PenetrableCover>().catchBullet(this);
				//this = hit.transform.gameObject.GetComponent<PenetrableCover>().catchBullet(this);
				
			} else {
				
				
				if (debug) MonoBehaviour.print("Shot " + hit.transform.name);
				
				
				GameObject newBulletHole = (GameObject)MonoBehaviour.Instantiate(BulletHole, hit.point, hitRotation);
				newBulletHole.transform.parent = hit.transform;
				newBulletHole.transform.Translate(0,(float)0.05,0);
				hitRotation.x = hitRotation.x + 270;
			
				GameObject newDust = (GameObject)MonoBehaviour.Instantiate(DirtSpray, hit.point, hitRotation);
				newDust.transform.parent = hit.transform;
				newDust.transform.Translate(0,(float)0.05,0);
			}
	}
}
