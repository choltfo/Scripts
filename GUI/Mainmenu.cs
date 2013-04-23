using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI () {
		if (GUI.Button(new Rect(100, 300, 100, 100), "Level 1")) {
			Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(200, 300, 100, 100), "Level 2")) {
			Application.LoadLevel(2);
		}
		if (GUI.Button(new Rect(300, 300, 100, 100), "Another Button")) {
			
		}
	}
}
