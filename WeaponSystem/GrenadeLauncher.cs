using UnityEngine;
using System.Collections;
[System.Serializable]
public class GrenadeLauncher : Weapon {
	public Grenade grenade;
	public Vector3 spawnPos;
	public float ShotForce;
	
	public override bool Shoot(Camera camera) {
		return Shoot (camera.gameObject);
	}
	
	public bool Shoot (GameObject camera) {
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
		newGrenade.transform.parent = null;
		CurAmmo -= 1;
		return true;
	}
	
	public override bool AIShoot(GameObject camera) {
		return Shoot (camera.gameObject);
	}
}