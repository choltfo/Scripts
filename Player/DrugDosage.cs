using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// controls how down a player is after getting poisoned or something.
/// </summary>
public class DrugDosage : MonoBehaviour{
	/// <summary>
	/// The drugs currently in the players system.
	/// </summary>
	public List<DrugEffect> effects;
	/// <summary>
	/// The CharacterController to inhibit.
	/// </summary>
	public CharacterControls charControls;
	float originalSpeed = 0;
	/// <summary>
	/// How slowed the player is.
	/// </summary>
	public float Slowness = 0;
	/// <summary>
	/// How much more to let the player's gun kick back.
	/// </summary>
	public float Recoil = 0;
	/// <summary>
	/// How much slower, as a percentage, the player is.
	/// </summary>
	public float SlownessLevel; //Max is 100 = dead.
	/// <summary>
	/// The slowness level VS slowness.
	/// </summary>
	public AnimationCurve SlownessLevelVSSlowness;
	/// <summary>
	/// The health level to modify.
	/// </summary>
	public Health healthManager;
	List<int> condemn;
	
	void Start(){
		effects = new List<DrugEffect>();
		originalSpeed = charControls.speed - 1;
	}
	
	void Update(){
		if (Recoil > 0) {
			Recoil -= 0.5f;
		} else {
			Recoil = 0;
		}
		if (SlownessLevel > 0) {
			SlownessLevel -= 0.5f;
		} else {
			SlownessLevel = 0;
		}
		condemn = new List<int>();
		int currentIndex = 0;
		foreach (DrugEffect effect in effects) {
			//print("Applying " + effect.ToString());
			switch (effect.effect) {
			case NegativeEffect.Recoil :
				Recoil += effect.baseStrength * effect.multiplier/100;
				break;
			case NegativeEffect.Slowness :
				SlownessLevel += (effect.baseStrength * effect.multiplier) /100;
				break;
			default:
				print("HOW DID THIS EVEN HAPPEN?!");
				break;
			}
			effect.timeRemaining -= Time.deltaTime;
			if (effect.timeRemaining <= 0) {
				condemn.Add(currentIndex);
			}
			currentIndex ++;
		}
		foreach (int index in condemn) {
			effects.RemoveAt(index);
		}
		
		Slowness = SlownessLevelVSSlowness.Evaluate(SlownessLevel);
		
		charControls.speed = (originalSpeed/(Slowness+1))+1;                                                           
	}
	
	/// <summary>
	/// Add another effect to the player.
	/// </summary>
	/// <param name='effect'>
	/// The effect to add.
	/// </param>
	public void addEffect(DrugEffect effect) {
		effects.Add(effect);
	}

	void OnGUI () {
		//GUI.Box(new Rect(Screen.width-320,40,300,20),"");
		//GUI.Box(new Rect(Screen.width-320,40,300*(SlownessLevel/100),20),"");
		//GUI.Box(new Rect(Screen.width-320,60,300,20),"");
		//GUI.Box(new Rect(Screen.width-320,60,300*(Slowness/100),20),"");
	}
}