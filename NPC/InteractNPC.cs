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
	public Pause PauseController;
	
	SpeechPlaybackState SPState = SpeechPlaybackState.Normal;
	int currentSpeech = 0;
	
	bool talking = false;
	GameObject interactee;
	ShootObjects SO;
	
	public override void Interact(GameObject player, ShootObjects SOPlayer) {
		talking = true;
		interactee = player;
		currentSpeech = 0;
		SO = SOPlayer;
		print (player.name);
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
			if (SPState == SpeechPlaybackState.Waiting) {
				Time.timeScale = 0f;
				PauseController.pane = "/Convo";
			}
		}
	}
	
	void drawConvo () {
		// Play Speech clip for Conversation's option, and wait for the track to stop playing.
		// After that, draw buttons corresponding to the associated options,
		// set the current option to the selected option, and restart.
		
		if (SPState == SpeechPlaybackState.Showing) {	// Playing back speech.
			SPState = audio.isPlaying ? SpeechPlaybackState.Showing : SpeechPlaybackState.Normal;
			if (currentSpeech == convo.currentOption.speechs.Length) SPState = SpeechPlaybackState.Waiting; 
		}
		if (SPState == SpeechPlaybackState.Normal) {	// Nothing happening.
			audio.Stop ();
			audio.loop = false;
			audio.clip = convo.currentOption.speechs[currentSpeech].audio;
			audio.Play();
			STController.setLine(convo.currentOption.speechs[currentSpeech].line);
			currentSpeech++;
			SPState = SpeechPlaybackState.Showing;
		}
		if (SPState == SpeechPlaybackState.Waiting) {	// Waiting for input
			for (int i = 0; i < convo.currentOption.options.Length; i++) {
				if (GUI.Button(new Rect(Screen.width/2-200, (Screen.height/2)-(100+25*i), 100, 25),
						convo.currentOption.options[i].name)) {
					
					convo.currentOption.options[i].TEvent.Trigger(STController);
					currentSpeech = 0;
					
					switch (convo.currentOption.options[i].type) {
					case OptionAction.Exit :
						convo.reset();
						talking = false;
						interactee = null;
						break;
					case OptionAction.JustTrigger: break;
					case OptionAction.Normal :
						convo.currentOption = convo.currentOption.options[i];
						SPState = SpeechPlaybackState.Normal;
						break;
					case OptionAction.OpenStore :
						talking = false;
						GetComponent<Store>().Open (SO);
						convo.reset();
						interactee = null;
						break;
					case OptionAction.ReturnToTop :
						convo.reset();
						break;
					}
					
					
				}
			}
		}
	}
}

/// <summary>
/// The state of a Speech playback system.
/// </summary>
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