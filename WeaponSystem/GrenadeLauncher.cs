using UnityEngine;
using System.Collections;
[System.Serializable]
public class GrenadeLauncher : Weapon {
	public GameObject rocket;
	public Vector3 spawnPos;
	public float ShotForce;
	
	public virtual bool shoot (Camera camera) {
		if (!IsValid || CurAmmo == 0) return false;
		GameObject newGrenade = MonoBehaviour.Instantiate(rocket, camera.transform.position, camera.transform.rotation) as GameObject;
		newGrenade.transform.parent = camera.transform;
		newGrenade.transform.Translate(spawnPos);
		if (newGrenade.GetComponent<Rigidbody>()) {
			newGrenade.GetComponent<Rigidbody>().AddForce(0,0,ShotForce);
		} else {
			newGrenade.AddComponent<Rigidbody>();
			newGrenade.GetComponent<Rigidbody>().AddForce(0,0,ShotForce);
		}
		newGrenade.transform.parent = null;
		return true;
	}
}
