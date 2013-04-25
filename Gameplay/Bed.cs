using UnityEngine;
using System.Collections;

public class Bed : Interact {
	
	public Material dayMaterial;
	public Material nightMaterial;
	public CameraFade fader;
	public float fadeTime = 5;
	float lastFade;
	bool faded = true;
	public Color fadeColor;
	public Color clearColor;
	
	public override void interact () {
		if (RenderSettings.skybox == dayMaterial){
			fader.StartFade(fadeColor, fadeTime);
			lastFade = Time.realtimeSinceStartup;
			faded = false;
			Time.timeScale = 0.1f;
		} else {
			fader.StartFade(fadeColor, fadeTime);
			lastFade = Time.realtimeSinceStartup;
			faded = false;
			Time.timeScale = 0.1f;
		}
	}
	
	void toggleSkybox() {
		if (RenderSettings.skybox == dayMaterial){
			RenderSettings.skybox = nightMaterial;
		} else {
			RenderSettings.skybox = dayMaterial;
		}
	}

	void FixedUpdate () {
		if (Time.realtimeSinceStartup > fadeTime + lastFade && !faded) {
			//fader.SetScreenOverlayColor(fadeColor);
			fader.StartFade(clearColor, fadeTime);
			toggleSkybox();
			Time.timeScale = 1;
			faded = true;
		}
	}
}
