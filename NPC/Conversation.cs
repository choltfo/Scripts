using UnityEngine;
using System.Collections;

[System.Serializable]
public class Conversation {
	public Option option;
	
}

[System.Serializable]
public class Option {
	public string resultS;
	public AudioClip resultA;
	public bool possible;
	public bool returnToTop;
	public Option[] options;
	
}