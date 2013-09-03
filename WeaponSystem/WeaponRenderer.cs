using UnityEngine;
using System.Collections;

public class WeaponRenderer : MonoBehaviour {
	
	public Camera parentCamera;

	// Update is called once per frame
	void Update () {
		camera.fieldOfView = parentCamera.fieldOfView;
	}
}
