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
	
	public GameObject instantiableObject;
	public Vector3 place;
	public Vector3 tolerance;
	public int number;
	
	public List<AudioClip> RadioSounds;
	public Radio radio;
	public bool radioState;
	public editSettings radioWriteStyle;
	
	
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
		if (instantiableObject!=null){
			for (int i = 0; i < number; i++) {
				MonoBehaviour.Instantiate(instantiableObject, (Random.value * tolerance) + place, new Quaternion(0,0,0,1));
			}
		}
		
		if (radioWriteStyle == editSettings.append) {
			foreach (AudioClip clip in RadioSounds) radio.clips.Add(clip);
		}
		if (radioWriteStyle == editSettings.overwrite) {
			radio.clips = RadioSounds;
		}
	}
}

public enum editSettings {
	append,
	overwrite
}