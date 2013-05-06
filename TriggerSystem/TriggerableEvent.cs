using UnityEngine;
using System.Timers;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class TriggerableEvent {
	
	public TriggerableEvent[] events;
	public Detonator[] explosions;
	public AudioSource soundSource;
	public AudioClip sound;
	public bool showText = false;
	public SubtitleLine[] Text;
	
	
	public void Trigger(SubtitleController TextDisplay) {
		foreach (Detonator explosion in explosions) {
			explosion.Explode();
		}
		foreach (TriggerableEvent TEvent in events) {
			TEvent.Trigger(TextDisplay);
		}
		if (sound != null && soundSource != null) {
			soundSource.PlayOneShot(sound);
		}
		if (showText) {
			TextDisplay.setLines(Text);
		}
	}
}