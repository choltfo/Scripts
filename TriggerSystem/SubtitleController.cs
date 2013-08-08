using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubtitleController : MonoBehaviour {
	
	public List<SubtitleLine> lines;
	public GUIStyle textStyle;
	public int lineNumber = 0;
	float setStartTime;
	float lastStartTime;
	
	public void setLines(List<SubtitleLine> l_lines) {
		lines = l_lines;
		lineNumber = 0;
		lastStartTime = Time.time;
		setStartTime = Time.time;
	}
	
	public void setLine(SubtitleLine line) {
		lines.Clear();
		lines.Add(line);
		lineNumber = 0;
		lastStartTime = Time.time;
		setStartTime = Time.time;
	}
	
	void OnGUI () {
		if (lineNumber == lines.Count) {
			return;
		}
		GUI.Label(new Rect(100,Screen.height-20,Screen.width,20), lines[lineNumber].text);
		if (Time.time > lastStartTime + lines[lineNumber].time) {
			lineNumber ++;
			lastStartTime = Time.time;
		}
	}
}
