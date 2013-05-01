using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public AudioSource painSoundSource;
	public CameraFade fader;
	public AudioClip[] painSounds;
	public Color fadeColor;
	public Color transparent;
	public float fadeTime = 0.5f;
	
	public int MaxHealth = 100;
	public float HealthLevel = 100;
	public Texture DeathScreen;
	public Texture bloodSplatter;
	public float healDelay;
	public float healRate;
	GUIStyle style;
	float lastInjury = 0;
	public Pause pauseController;
	
	public void Start() {
		style = new GUIStyle();
		style.normal.background = (Texture2D)bloodSplatter;
	}
	
	public void Update() {
		/*if (Time.time - lastInjury > healDelay) {
			if (HealthLevel < MaxHealth && HealthLevel > 0) {
				HealthLevel += healRate;
			}
		}*/
	}
	
	public bool Damage(float damage, bool force) {
		return true;
	}
	
	public bool Damage(float damage) {
		lastInjury = Time.time;
		painSoundSource.PlayOneShot(painSounds[(int)(Random.value * painSounds.Length)-1]);
		fader.SetScreenOverlayColor(fadeColor);
		fader.StartFade(transparent, fadeTime);
		
		if (damage >= HealthLevel) {
			HealthLevel = HealthLevel - damage;
			print("Debug: Dead.");
			pauseController.pane = "/Dead";
			Time.timeScale = 0;
			return false;
		} else {
			HealthLevel = HealthLevel - damage;
			return true;
		}
	}
	
	public void OnGUI() {
		GUI.Box(new Rect(Screen.width-320,20,300,20),"");
		GUI.Box(new Rect(Screen.width-320,20,300*(HealthLevel/MaxHealth),20),"");
		if (Time.timeScale == 0) {
			return;
		}
		/*if (HealthLevel < 1) {
		//	GUI.Box(new Rect(0, 0, Screen.width, Screen.height), DeathScreen);
		//}*/
		//style. color = new Color (0,0,0,(HealthLevel/MaxHealth)*255);
		//style.normal.background.
		if (HealthLevel < (0.25*MaxHealth)) {
			//GUI.Box(new Rect(0,0, Screen.width, Screen.height), bloodSplatter, style);
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bloodSplatter);
		}
	}
}

