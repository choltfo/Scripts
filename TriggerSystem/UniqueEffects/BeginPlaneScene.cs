using UnityEngine;
using System.Collections;

public class BeginPlaneScene : UniqueEffect {
	
	public CameraFade fader;
	public Color fadeOutColour;
	public Color transparency;
	public float fadeTime;
	public float delayTime;
	public float splashTime;
	public Texture SplashScreen;
	
	public InteractNPC pilot;
	
	float lastResequenceTime = 0f;
	
	bool on = false;
	public IS state = IS.Waiting;
	
	public override void trigger () {
		on = true;
		lastResequenceTime = Time.time;
		state = IS.DisplayingSplash;
		transparency = new Color (0,0,0,0);
		fadeOutColour = new Color (0,0,0,1);
	}
	
	void OnGUI () {
		if (!on) return;
		
		if (lastResequenceTime
				+ (state == IS.DisplayingSplash ? splashTime : 0)
				+ (state == IS.BackToBlack ? fadeTime : 0)
				< Time.time) {

			state++;
			lastResequenceTime = Time.time;
			if (state == IS.BackToTrans) {
				fader.StartFade(transparency, fadeTime);
			}
			if (state == IS.BackToBlack) {
				fader.StartFade(fadeOutColour, fadeTime);
			}
		}
		
		if (state == IS.DisplayingSplash) {
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), SplashScreen);
		}
		if (state == IS.BackToBlack) {
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), SplashScreen);
		}
		if (state == IS.Done) {
			fader.SetScreenOverlayColor(transparency);
			on = false;
			pilot.talking = true;
		}
		
	}
}

public enum IS {
	Waiting,
	DisplayingSplash,
	BackToBlack,
	BackToTrans,
	Done
}