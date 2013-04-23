using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public Controls controls;
	public bool isPaused = false;
	void Update () {
		if (Input.GetKeyDown(controls.pause)) {
			switch (isPaused) {
				case false:
					Time.timeScale = 0f;
					isPaused = true;
					break;
				case true:
					Time.timeScale = 1f;
					isPaused = false;
					break;
			}
		}
	}
	void OnGUI () {
		if (isPaused) {
			//If game is paused, draw pause menu.
			Time.timeScale = 1f;
			isPaused = false;
		}
	}
}
