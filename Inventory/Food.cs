using UnityEngine;
using System.Collections;

public class FoodStuff {
	public string name = "Bread Slice";
	
	public float foodSatiationPercent = 10;
	public float drinkSatiationPercent = 0;
	
	public DrugEffect DE;
	
	public FoodStuff () {}
	
	public FoodStuff (float foodPercent, float drinkPercent, string Name, DrugEffect de) {
		name = Name;
		foodSatiationPercent = foodSatiationPercent;
		drinkSatiationPercent = drinkSatiationPercent;
		DE = de;	// MAY BE A PROBLEM!
	}
	
	public FoodStuff (Food f) {
		name = f.name;
		foodSatiationPercent = f.foodSatiationPercent;
		drinkSatiationPercent = f.drinkSatiationPercent;
		DE = f.DE;	// MAY BE A PROBLEM!
	}
}

public class Food : ScriptableObject {
	public string name = "Bread Slice";
	
	public float foodSatiationPercent = 10;
	public float drinkSatiationPercent = 0;
	
	public DrugEffect DE;
}

