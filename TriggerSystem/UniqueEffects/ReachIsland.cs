using UnityEngine;
using System.Collections;

public class ReachIsland : UniqueEffect {
	
	public CameraFade fader;
	public Color fadeOutColour;
	public Color transparency;
	public float fadeTime;
	public float delayTime;
	public float splashTime;
	public Texture SplashScreen;
	
	float lastResequenceTime = 0f;
	
	bool on = false;
	public RISC state = RISC.Waiting;
	
	public override void trigger () {
		on = true;
		lastResequenceTime = Time.time;
		fader.StartFade(fadeOutColour, fadeTime);
		state = RISC.FadingToBlack;
		transparency = new Color (0,0,0,0);
		lastResequenceTime = Time.time;
	}
	
	void OnGUI () {
		if (!on) return;
		
		if (lastResequenceTime + 
			(state == RISC.FadingToBlack ? fadeTime : 0) +
			(state == RISC.DisplayingSplash ? splashTime : 0) +
			(state == RISC.Delay ? delayTime : 0) < Time.time) {
			state++;
			lastResequenceTime = Time.time;
		}
		
		if (state == RISC.DisplayingSplash) {
			fader.SetScreenOverlayColor(transparency);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), SplashScreen);
		}
		if (state == RISC.Done) {
			fader.SetScreenOverlayColor(fadeOutColour);
			Application.LoadLevel(2);
		}
		
	}
}

public enum RISC {
	Waiting,
	FadingToBlack,
	Delay,
	DisplayingSplash,
	Done
}