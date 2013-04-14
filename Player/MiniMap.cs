using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	
	public string MiniMapKey = "m";
	public Camera MiniMapCamera;
	public Camera DefaultCamera;	
	
	void Update () {
		if (Input.GetKeyDown(MiniMapKey)) {
			DefaultCamera.enabled = false;
			MiniMapCamera.enabled = true;
		}
		if (Input.GetKeyUp(MiniMapKey)) {
			DefaultCamera.enabled = true;
			MiniMapCamera.enabled = false;
		}
	}
}
