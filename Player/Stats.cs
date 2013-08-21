using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	/// <summary>
	/// The satiation food percent.
	/// 100 is full, 0 is empty.
	/// </summary>
	public float satiationFoodPercent;
	/// <summary>
	/// The satiation drink percent.
	/// 100 is full, 0 is empty.
	/// </summary>
	public float satiationDrinkPercent;
	
	public float satiationFoodDeclineRate;
	public float satiationDrinkDeclineRate;
	
	/// <summary>
	/// Alter this depending on what the character is doing.
	/// e.g., sprinting should use up food faster than walking, than driving. 
	/// </summary>
	public float satiationDeclinePercentage;
	
	
	public float IdealMaxHealth = 100f;
	public AnimationCurve curveMaxHealth;
	public float acualMaxHealth;
	public float idealHealDelay;
	public float idealHealRate;
	public AnimationCurve curveHealDelay;
	public AnimationCurve curveHealRate;
	public float actualHealDelay;
	public float actualHealRate;
	
	
	public float idealSpeed;
	public float generalResistance;
	public float recoilFactor;
	
	public int xp;
	
	public CharacterControls CControls;
	public Health health;

    void Start() {
        //CControls = this.gameObject.GetComponent<CharacterControls>();
        health = this.gameObject.GetComponent<Health>();
    }
	
	void update
}