using UnityEngine;
using System.Collections;

public class SubtitleController : MonoBehaviour {
	
	public SubtitleLine[] lines;
	public GUIStyle textStyle;
	int lineNumber = 0;
	float setStartTime;
	float lastStartTime;
	
	public void setLines(SubtitleLine[] l_lines) {
		lines = l_lines;
		lineNumber = 0;
		lastStartTime = Time.time;
		setStartTime = Time.time;
	}
	
	void OnGUI () {
		if (lineNumber == lines.Length) {
			return;
		}
		GUI.Label(new Rect(100,Screen.height-20,Screen.width,20), lines[lineNumber].text);
		if (Time.time > lastStartTime + lines[lineNumber].time) {
			lineNumber ++;
		}
	}
}
