using UnityEngine;
using System.Collections;


/// <summary>
/// An NPC that you can talk to.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class InteractNPC : InteractObject {
	
	//public Conversation[] convos;
	public Conversation convo;
	public bool pauseTime = true;
	
	public SubtitleController STController;
	
	SpeechPlaybackState SPState = SpeechPlaybackState.Normal;
	int currentSpeech = 0;
	
	bool talking = false;
	
	public override void Interact(GameObject interacter) {
		talking = true;
	}
	
	// Use this for initialization
	void Start () {
		convo.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		if (talking) {
			drawConvo ();
		}
	}
	
	void drawConvo () {
		// Play Speech clip for Conversation's option, and wait for the track to stop playing.
		// After that, draw buttons corresponding to the associated options,
		// set the current option to the selected option, and restart.
		
		if (SPState == SpeechPlaybackState.Normal) {	// Nothing happening.
			audio.Stop ();
			audio.loop = false;
			audio.clip = convo.currentOption.speechs[currentSpeech].audio;
			audio.Play();
			STController.setLine(convo.currentOption.speechs[currentSpeech].line);
			currentSpeech++;
			SPState = SpeechPlaybackState.Showing;
		}
		if (SPState == SpeechPlaybackState.Showing) {	// Playing back speech.
			SPState = audio.isPlaying ? SpeechPlaybackState.Showing : SpeechPlaybackState.Normal;
			if (currentSpeech == convo.currentOption.speechs.Length) SPState = SpeechPlaybackState.Waiting; 
		}
		if (SPState == SpeechPlaybackState.Waiting) {	// Waiting for input
			
		}
	}
}
