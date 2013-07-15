using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class Radio : InteractObject {

	public List<AudioClip> clips;
	public bool radioOn;
	
	static bool debug = true;
	
	public TriggerableEvent[] events;
	public SubtitleController SubTitleController;
	bool triggered = false;
	
	// Use this for initialization
	void Start () {
		if (radioOn) {
			audio.clip = clips[Random.Range(0,clips.ToArray().Length)];
			audio.Play();
			if (debug) print("Starting Radio");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (radioOn && !audio.isPlaying) {
			if (debug) print("Playing new clip, as audio.isPlaying = " + audio.isPlaying);
			audio.clip = clips[Random.Range(0,clips.ToArray().Length)];
			audio.Play();
		}
		if (!radioOn && audio.isPlaying) {
			audio.Stop();
			if (debug) print("Turning off radio.");
		}
	}
	
	public override void Interact (GameObject player) {
		radioOn = !radioOn;
		
		if (!triggered) {
			foreach (TriggerableEvent e in events) e.Trigger(SubTitleController);
		}
		if (debug) print("Toggling radio");
	}
}
