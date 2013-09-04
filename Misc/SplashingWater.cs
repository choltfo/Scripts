using UnityEngine;
using System.Collections;

public class SplashingWater : MonoBehaviour {

	public GameObject splashPrefab;
	//Splash noise.
	
	public void Splash (Vector3 Location) {
		Instantiate(splashPrefab, Location, new Quaternion (0,0,0,1));
	}
	
	void OnCollisionEnter (Collision c) {
		foreach (ContactPoint cP in c.contacts) {
			Instantiate(splashPrefab, cP.point, new Quaternion (0,0,0,1));
		}
	}
}
