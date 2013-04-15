using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int MaxHealth = 100;
	public float HealthLevel = 100;
	public Texture DeathScreen;
	public Texture bloodSplatter;
	public float healDelay;
	public float healRate;
	GUIStyle style;
	float lastInjury = 0;
	
	public void Start() {
		style = new GUIStyle();
		style.normal.background = (Texture2D)bloodSplatter;
	}
	
	public void Update() {
		if (Time.time - lastInjury > healDelay) {
			
		}
	}
	
	public bool Damage(float damage) {
		lastInjury = Time.time;
		if (damage < HealthLevel) {
			HealthLevel = HealthLevel - damage;
			return true;
		} else {
			HealthLevel = HealthLevel - damage;
			print("Debug: Dead.");
			
			return false;
		}
	}
	
	public void OnGUI() {
		if (HealthLevel < 1) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), DeathScreen);
		}
		//style. color = new Color (0,0,0,(HealthLevel/MaxHealth)*255);
		//style.normal.background.
		if (HealthLevel < (0.25*MaxHealth)) {
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), bloodSplatter, style);
		}
		GUI.Box(new Rect(Screen.width-320,20,300,20),"");
		GUI.Box(new Rect(Screen.width-320,20,300*(HealthLevel/MaxHealth),20),"");
	}
}

