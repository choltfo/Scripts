using UnityEngine;
using System.Collections;

[System.Serializable]
public class Conversation {
	public Option option;
	public bool done;
	
	[HideInInspector]
	public Option currentOption;
	
	public void Init() {
		option.Init();
		currentOption = option;
	}
}

[System.Serializable]
public class Option {
	
	public string name;
	
	public void Init() {
		foreach (Speech spc in speechs) {
			spc.Init();
		}
	}
	
	public Speech[] speechs;
	public bool possible;
	public bool returnToTop;
	public Option[] options;
	
}

public enum SpeechPlaybackState {
	/// <summary>
	/// Ready.
	/// </summary>
	Normal,
	/// <summary>
	/// Showing user subtitles and playing audio.
	/// </summary>
	Showing,
	/// <summary>
	/// Waiting for user input.
	/// </summary>
	Waiting 
}