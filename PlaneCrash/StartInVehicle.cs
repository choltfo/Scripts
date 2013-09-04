using UnityEngine;
using System.Collections;

public class StartInVehicle : MonoBehaviour {
	
	public GameObject playerCam;
	public Vehicle v;
	
	// Use this for initialization
	void Start () {
		v.activate(playerCam);
	}
}
