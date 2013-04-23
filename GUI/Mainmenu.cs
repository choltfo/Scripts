using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI () {
		if (GUI.Button(new Rect(100, 300, 100, 100), "New Game")) {
			Application.LoadLevel(1);
		}
	}
}
