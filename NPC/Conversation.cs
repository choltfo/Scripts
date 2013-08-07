using UnityEngine;
using System.Collections;

[System.Serializable]
public class Conversation {
	public Option option;
	public void Init() {
		option.Init();
	}
}

[System.Serializable]
public class Option {
	
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