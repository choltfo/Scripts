using System;
using UnityEngine;
using System.Collections.Generic;

public class DrugDosage : MonoBehaviour{
	
	public List<DrugEffect> effects;
	public CharacterControls controls;
	float originalSpeed = 0;
	
	public float Slowness;
	public float Recoil;
	public float SlownessLevel; //Max is 100 = dead.
	public Health healthManager;
	public List<int> condemn;
	
	void Start(){
		effects = new List<DrugEffect>();
		originalSpeed = controls.speed;
	}
	
	void Update(){
		if (Recoil > 0) {
			Recoil -= 1;
		} else {
			Recoil = 0;
		}
		if (Slowness > 0) {
			Slowness -= 1;
		} else {
			Slowness = 0;
		}
		condemn = new List<int>();
		int currentIndex = 0;
		foreach (DrugEffect effect in effects) {
			print("Applying " + effect.ToString());
			switch (effect.effect) {
			case NegativeEffect.Recoil :
				Reccoil += effect.baseStrength/100 * effect.multiplier/100;
				break;
			case NegativeEffect.Slowness :
				Slowness += effect.baseStrength/100 * effect.multiplier/100;
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
		
		//controls.speed = (originalSpeed/(1/Slowness))+1;                                                           
	}
	
	public void addEffect(DrugEffect effect) {
		effects.Add(effect);
	}

	void OnGUI () {}
}
                       
                                               

                                                
                                          