using UnityEngine;
using System.Collections;
/// <summary>
/// The main menu.
/// </summary>
public class Mainmenu : MonoBehaviour {
	/// <summary>
	/// The button style.
	/// </summary>
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
