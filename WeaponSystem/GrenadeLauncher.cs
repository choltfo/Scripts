using UnityEngine;
using System.Collections;

[System.Serializable]
public class GrenadeLauncher : Weapon {
	public Grenade grenade;
	public Vector3 spawnPos;
	public float ShotForce;
	public bool blowback;
	public Vector3 blowbackOutput;
	public float blowbackDistance;
	public float maxBlowbackDamage;
	public float blowbackFieldAngle;
	
	public override bool Shoot(Camera camera, Enemy shooter) {
		return Shoot (camera.gameObject, shooter);
	}
	
	/// <summary>
	/// Instantiate the weapon pickup for this gun, and let it go.
	/// </summary>
	public override void Drop() {
		if (!IsValid) {
			return;
		}
		GameObject pickup = (GameObject)MonoBehaviour.Instantiate(InstantiablePickup, mainObject.transform.position, mainObject.transform.rotation);
		pickup.SetActive(true);
		pickup.GetComponent<PickupGrenadeLauncher>().thisGun = this;
		destroy();
		IsValid = false;
	}
	
	public bool Shoot (GameObject camera, Enemy shooter) {
		if (!IsValid || CurAmmo == 0) return false;
		GameObject newGrenade = MonoBehaviour.Instantiate(grenade.instantiableGrenade, camera.transform.position, camera.transform.rotation) as GameObject;
		newGrenade = grenade.convert(newGrenade);
		newGrenade.transform.parent = camera.transform;
		newGrenade.transform.Translate(spawnPos);
		if (newGrenade.GetComponent<Rigidbody>()) {
			newGrenade.GetComponent<Rigidbody>().AddRelativeForce(0,0,ShotForce);
		} else {
			newGrenade.AddComponent<Rigidbody>();
			newGrenade.GetComponent<Rigidbody>().AddRelativeForce(0,0,ShotForce);
		}
		
		if (blowback) {
			Debug.Log("Performing blowback Raycast...");
			RaycastHit hit;
			if (Physics.Raycast( blowbackOutput, VectorF.RotateY(((Weapon)this).mainObject.transform.eulerAngles, 180), out hit, blowbackDistance)) {
				GameObject g = hit.transform.gameObject; 
				Debug.Log("Hit " + g.name);
				if (g.GetComponent<EnemyHealth>() != null) {
					Debug.Log("Hit an enemy, killing it...");
					g.GetComponent<EnemyHealth>().damage((hit.distance/blowbackDistance)*maxBlowbackDamage, DamageCause.Blowback);
				} else {
					Debug.Log("Hit a wall, killing you...");
					((Weapon)this).mainObject.transform.parent.gameObject.GetComponent<Health>().
						Damage((hit.distance/blowbackDistance)*maxBlowbackDamage, DamageCause.Blowback);
				}
			} else {
				Debug.Log("Didn't hit anything.");
			}
		}
		
		newGrenade.transform.parent = null;
		CurAmmo -= 1;
		return true;
	}
	
	public override bool AIShoot(GameObject camera, Enemy shooter) {
		return Shoot (camera.gameObject, shooter);
	}
}