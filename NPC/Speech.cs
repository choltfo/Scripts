using UnityEngine;
using System.Collections;

[System.Serializable]
public class Speech {
	
	public string transcript;
	public AudioClip audio;
	public float overrideTime;
	
	SubtitleLine line = new SubtitleLine();
	
	// This is called by the holding class to make the subtitle line not blank.
	public void Init () {
		line.text = transcript;
		if (audio != null) line.time = audio.length; else line.time = overrideTime;
	}
}
