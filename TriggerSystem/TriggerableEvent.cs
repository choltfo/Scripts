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
	public QuickTimeSpamEvent Spam;
	public QuickTimeSpamEventController QTSEController;
	public StatResult statMod;
	
	
	public void Trigger(SubtitleController TextDisplay) {
		foreach (Detonator explosion in explosions) {
			if (explosion != null) explosion.Explode();
		}
		foreach (TriggerableEvent TEvent in events) {
			TEvent.Trigger(TextDisplay);
		}
		if (QTSEController != null) QTSEController.Queue(Spam);
		if (sound != null && soundSource != null) {
			soundSource.PlayOneShot(sound);
		}
		if (showText) {
			TextDisplay.setLines(Text);
		}
	}
}