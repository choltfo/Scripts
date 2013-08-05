using UnityEngine;
using System.Collections;

[System.Serializable]
public class Speech {
	
	public String trannscript;
	public AudioClip audio;
	
	SubtitleLine line = new SubtitleLine();
	
	// This is called by the holding class to make the subtitle line not blank.
	public void Init () {
		line.text = transcript;
		line.time = audio.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
