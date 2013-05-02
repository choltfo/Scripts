using UnityEngine;
using System;

/// <summary>
/// Holds a health-affecting effect, and it's details.
/// </summary>
[System.Serializable]

public class DrugEffect {
	/// <summary>
	/// The effect of the drug.
	/// </summary>
	public NegativeEffect effect;
	/// <summary>
	/// The UID of this DrugEffect.
	/// </summary>
	public int UID;
	/// <summary>
	/// The base strength of this DrugEffect.
	/// </summary>
	public int baseStrength;
	/// <summary>
	/// The time remaining for this DrugEffect.
	/// </summary>
	public float timeRemaining;
	/// <summary>
	/// The total time from the application to finish.
	/// </summary>
	public float totalTime;
	//[HideInInspector]
	/// <summary>
	/// The multiplier of this DrugEffect.
	/// </summary>
	public float multiplier;
	/// <summary>
	/// The last UID assigned.
	/// </summary>
	static int lastUID = 0;
	
	/// <summary>
	/// Generates a unique ID.
	/// </summary>
	/// <returns>
	/// A new unique ID for a drug effect.
	/// </returns>
	public static int generateUID() {
		lastUID ++;
		return lastUID - 1;
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="DrugEffect"/> class.
	/// </summary>
	/// <param name='l_effect'>
	/// Effect type of new effect.
	/// </param>
	/// <param name='l_multiplier'>
	/// Multiplier of new effect.
	/// </param>
	/// <param name='l_baseStrength'>
	/// Base strength of new effect.
	/// </param>
	/// <param name='l_timeRemaining'>
	/// The time remaining.
	/// </param>
	/// <param name='l_totalTime'>
	/// The total time.
	/// </param>
	public DrugEffect (NegativeEffect l_effect, float l_multiplier,
		int l_baseStrength, float l_timeRemaining, float l_totalTime) {
		
		UID = generateUID ();
		effect = l_effect;
		multiplier = l_multiplier;
		baseStrength = l_baseStrength;
		timeRemaining = l_timeRemaining;
		totalTime = l_totalTime;
	}
	
	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="DrugEffect"/>.
	/// </summary>
	/// <returns>
	/// A <see cref="System.String"/> that represents the current <see cref="DrugEffect"/>.
	/// </returns>
	public override string ToString () {	
		return "Effect #"+UID.ToString()+". "+effect.ToString()+" effect. Strength: "+ baseStrength.ToString()
			+". TimeRemining: "+timeRemaining.ToString()
			+". Multiplier: " +multiplier.ToString()+".";
	}
	
	/// <summary>
	/// Duplicate this instance.
	/// </summary>
	public DrugEffect Duplicate(){
		return new DrugEffect(effect, multiplier, baseStrength, timeRemaining, totalTime);
	}
}

