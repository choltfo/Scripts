using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {
	
	public GUIStyle button;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI () {
		if (GUI.Button(new Rect(100, 300, 200, 36), "New Game", button)) {
			Application.LoadLevel(1);
		}
	}
}
