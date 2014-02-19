using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the level of health for the player.
/// </summary>
public class Health : MonoBehaviour {
	/// <summary>
	/// The audio source to play the painsounds from
	/// </summary>
	public AudioSource painSoundSource;
	/// <summary>
	/// A list of sounds to play from
	/// </summary>
	public CameraFade fader;
	public bool playSounds = true;
	public AudioClip[] painSounds;
	public Color fadeColor;
	public Color transparent;
	public float fadeTime = 0.5f;
	
	public float HealthLevel = 100;
	public bool drawTextures;
	public Texture DeathScreen;
	public Texture2D bloodSplatter;

	GUIStyle style;
	float lastInjury = 0;
	public Pause pauseController;
	public Stats statistics;
	
	DamageCause lastCOD;
	
	
	public void Start() {
		style = new GUIStyle();
		style.normal.background = (Texture2D)bloodSplatter;
	}
	
	void Update () {
		if (Time.time > lastInjury + statistics.actualHealDelay) {
			if (statistics.actualHealRate * Time.deltaTime > statistics.acualMaxHealth - HealthLevel) {
				HealthLevel = statistics.acualMaxHealth;
			} else {
				HealthLevel += statistics.actualHealRate * Time.deltaTime;
			}
		}
	}
	
	/// <summary>3
	/// Deal damage to player.ss
	/// </summary>
	/// <param name='damage'>
	/// the amount of damage received.
	/// </param>
	/// <returns>
	/// Whether the player survived.
	/// </returns>
	public bool Damage(float damage, DamageCause COD = DamageCause.Default) {
		lastInjury = Time.time;
		if (playSounds) painSoundSource.PlayOneShot(painSounds[(int)(Random.value * (painSounds.Length-1))]);
		fader.SetScreenOverlayColor(fadeColor);
		fader.StartFade(transparent, fadeTime);
		lastCOD = COD;
		if (damage >= HealthLevel) {
			Die ();
			return false;
		} else {
			HealthLevel = HealthLevel - damage;
			return true;
		}
		
	}
	
	public void Die () {
		HealthLevel = 0;
		pauseController.pane = "/Dead";
		Time.timeScale = 0;
		print ("You have died of " + lastCOD.ToString());
	}
	
	public void OnGUI() {
		GUI.Box(new Rect(Screen.width-320,20,300,20),"");
		if (HealthLevel >= 4) {
			GUI.Box(new Rect(Screen.width-320,20,300*(HealthLevel/statistics.acualMaxHealth),20),"");
		}
		if (Time.timeScale == 0 && pauseController.pane.Equals("/Dead")) {
			GUI.Label(new Rect((Screen.width/2) - 200,(Screen.height/2) - 20, 400 ,40), "YOU ARE DEAD!");
			GUI.Label(new Rect((Screen.width/2) - 200,(Screen.height/2) - 00, 400 ,40), DeathMessage.getMsg(lastCOD));
					
			return;
		}																			//MAYBE
					
		/*if (HealthLevel < 1) {
		//	GUI.Box(new Rect(0, 0, Screen.width, Screen.height), DeathScreen);
		//}*/
		//style. color = new Color (0,0,0,(HealthLevel/statistics.acualMaxHealth)*255);
		//style.normal.background.
		if (HealthLevel < (0.25*statistics.acualMaxHealth) && drawTextures) {
			//GUI.Box(new Rect(0,0, Screen.width, Screen.height), bloodSplatter, style);
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), bloodSplatter);
		}
	}
}

public static class DeathMessage {
	public static string getMsg (DamageCause cause) {
		switch (cause) {
		case DamageCause.Blowback :
			return "You died of blowback. Some grenade launchers will cause you massive harm if you are standing too close to the a wall.";
		case DamageCause.EnemyHit :
			return "You were shot by an enemy. Watch for enemy fire, and make use of cover.";
		case DamageCause.Shot :
			return "You got shot down. Make sure to check your flank.";
		case DamageCause.Poisoned :
			return "You died of a poison. Try to get the antidote next time.";
		case DamageCause.Explosion :
			return "You were killed by an explosion. Watch for grenades and barrels";
		case DamageCause.VehicularMisadventure :
			return "You drove into something less likely to yield than yourself. Watch out.";
		case DamageCause.RunDown :
			return "You got hit by a car. Avoid being too far from something very solid.";
		case DamageCause.Radiation :
			return "Most water is radioactive. Try to avoid it.";
		case DamageCause.Fire :
			return "You burned to death.";
		case DamageCause.KineticInjury :
			return "You giot hit to death. Avoid letting things hit you very hard by standing in cover.";
		}
		return "";
	}
}

public enum DamageCause {
	Default,				// It just happened.
	EnemyHit,				// Ran into an enemy.
	Shot,					// Got shot.
	Poisoned,				// Failed to find the antidote.
	Explosion,				// Got blown to smithereens.
	Blowback,				// Was standing too close to a wall when firing a grenade launcher.
	VehicularMisadventure,	// Crashed.
	RunDown,				// Crashed into.
	Radiation,				// Took a swim in the river of death.
	Fire,					// Got lit literally.
	KineticInjury			// Hit something, either vertically or horizontally.
}
