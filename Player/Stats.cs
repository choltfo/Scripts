using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	/// <summary>
	/// The satiation food percentage.
	/// 100 is full, 0 is empty.
	/// </summary>
	public float satiationFoodPercent;
	/// <summary>
	/// The satiation drink percentage.
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
	
	
	public AnimationCurve curveMaxHealth;
	public float acualMaxHealth;
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
	
	void Update () {
		actualHealDelay = curveHealDelay.Evaluate((satiationFoodPercent + satiationDrinkPercent)/2);
		actualHealRate = curveHealRate.Evaluate((satiationFoodPercent + satiationDrinkPercent)/2);
		acualMaxHealth = curveMaxHealth.Evaluate((satiationFoodPercent + satiationDrinkPercent)/2);
		
		float sateDec = satiationDeclinePercentage/100;
		
		if (satiationFoodDeclineRate * Time.deltaTime * sateDec > 100 - satiationFoodDeclineRate) {
			satiationFoodPercent = 100;
		} else {
			satiationFoodPercent -= satiationFoodDeclineRate * Time.deltaTime * sateDec;
		}
		
		if (satiationDrinkDeclineRate * Time.deltaTime * sateDec > 100 - satiationDrinkDeclineRate) {
			satiationDrinkPercent = 100;
		} else {
			satiationDrinkPercent -= satiationDrinkDeclineRate * Time.deltaTime * sateDec;
		}
	}
}