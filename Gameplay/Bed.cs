using UnityEngine;
using System.Collections;

public class Bed : Interact {
	//Staticy bits
	public static int timeOfDay = 0;
	public static bool Day = true;
	public static float lastSwitch = 0;
	public static float interval = 15;
	
	public static void UpdateTOD() {
		if (Time.time > lastSwitch + interval && Time.time != lastSwitch) {
			Day = !Day;
		}
	}
	
	//Per-bed bits
	public Material dayMaterial;
	public Material nightMaterial;
	public GameObject nightlight;
	public GameObject daylight;
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
			//Time.timeScale = 0.1f;
		} else {
			fader.StartFade(fadeColor, fadeTime);
			lastFade = Time.realtimeSinceStartup;
			faded = false;
			//Time.timeScale = 0.1f;
		}
	}
	
	void toggleSkybox() {
		if (RenderSettings.skybox == dayMaterial){
			RenderSettings.skybox = nightMaterial;
			nightlight.SetActive(true);
			daylight.SetActive(false);
		} else {
			RenderSettings.skybox = dayMaterial;
			nightlight.SetActive(false);
			daylight.SetActive(true);
		}
	}

	void FixedUpdate () {
		//UpdateTOD();
		if (Time.realtimeSinceStartup > fadeTime + lastFade && !faded) {
			fader.SetScreenOverlayColor(fadeColor);
			fader.StartFade(clearColor, fadeTime);
			toggleSkybox();
			//Time.timeScale = 1;
			faded = true;
		}
	}
}
