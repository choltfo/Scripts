using UnityEngine;
using System.Collections;

public class SplashingWater : MonoBehaviour {

	public GameObject splashPrefab;
	//Splash noise.
	
	public void Splash (Vector3 Location) {
		Instantiate(splashPrefab, Location, new Quaternion (0,0,0,1));
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
