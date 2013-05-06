using UnityEngine;
using System.Collections;

/// <summary>
/// Controls beds
/// </summary>
public class Sleep : InteractObject {
	//Staticy bits
	
	/// <summary>
	/// The time of day.
	/// </summary>
	public static int timeOfDay = 0;
	/// <summary>
	/// Day?
	/// </summary>
	public static bool Day = true;
	/// <summary>
	/// Timestamp of last day/night switch.
	/// </summary>
	public static float lastSwitch = 0;
	/// <summary>
	/// The interval between day/night switches.
	/// </summary>
	public static float interval = 15;
	
	/// <summary>
	/// Updates time of day.
	/// </summary>
	public static void UpdateTOD() {
		if (Time.time > lastSwitch + interval && Time.time != lastSwitch) {
			Day = !Day;
		}
	}
	
	//Per-bed bits
	
	/// <summary>
	/// The day skybox material.
	/// </summary>
	public Material dayMaterial;
	/// <summary>
	/// The night skybox material.
	/// </summary>
	public Material nightMaterial;
	/// <summary>
	/// The light for nighttime.
	/// </summary>
	public GameObject nightlight;
	/// <summary>
	/// The light for daytime.
	/// </summary>
	public GameObject daylight;
	/// <summary>
	/// The <see cref="CamerFader"/> instance to fade the GUI with.
	/// </summary>
	public CameraFade fader;
	/// <summary>
	/// The fade time.
	/// </summary>
	public float fadeTime = 5;
	float lastFade;
	bool faded = true;
	/// <summary>
	/// The color of the fade.
	/// </summary>
	public Color fadeColor;
	/// <summary>
	/// The default colour after switching from day to night or vice-versa.
	/// Use transparent.
	/// </summary>
	public Color clearColor;
	
	/// <summary>
	/// Interact with this instance.
	/// </summary>
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
