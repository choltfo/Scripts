using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class TriggerableEvent {
	
	public Detonator[] explosions;
	public AudioSource soundSource;
	public AudioClip sound;
	
	
	public virtual void Trigger() {
		foreach (Detonator explosion in explosions) {
			explosion.Explode();
		}
		if (sound != null && soundSource != null) {
			soundSource.PlayOneShot(sound);
		}
	}
}