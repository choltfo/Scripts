using UnityEngine;
using System;

/*
 * Holds a health-affecting effect, and it's details.
 */
[System.Serializable]

public class DrugEffect {
	
	public NegativeEffect effect;
	public int UID;
	public int baseStrength;
	public float timeRemaining;
	public float totalTime;
	[HideInInspector]
	public int multiplier;
	static int lastUID = 1;
	
	public static int generateUID() {
		lastUID ++;
		return lastUID - 1;
	}
	
	public DrugEffect (NegativeEffect l_effect, int l_multiplier,
		int l_baseStrength, float l_timeRemaining, float l_totalTime) {
		
		UID = generateUID ();
		effect = l_effect;
		multiplier = l_multiplier;
		baseStrength = l_baseStrength;
		timeRemaining = l_timeRemaining;
		totalTime = l_totalTime;
	}

	public override string ToString () {	
		return "Effect #"+UID.ToString()+". "+effect.ToString()+" effect. Strength: "+ baseStrength.ToString()
			+". TimeRemining: "+timeRemaining.ToString()
			+". Multiplier: " +multiplier.ToString()+".";
	}
	
	public DrugEffect Duplicate(){
		return new DrugEffect(effect, multiplier, baseStrength, timeRemaining, totalTime);
	}
}

