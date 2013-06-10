using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuickTimeSpamEvent {
	
	public int requiredPresses;
	public float time;
	
	public TriggerableEvent failureResult;
	public TriggerableEvent successResult;
}
