using UnityEngine;
using UnityEditor;
using System.IO;

public class Food : ScriptableObject {
	public string name = "Bread Slice";
	
	public float foodSatiationPercent = 10;
	public float drinkSatiationPercent = 0;
	
	public DrugEffect DE;
	
	[MenuItem("Assets/Create/FoodStuff")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<Food> ();
	}
	
	public FoodStuff createFoodStuff () {
		return new FoodStuff (foodSatiationPercent, drinkSatiationPercent, name, DE);
	}
}

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
		DE = (DrugEffect)de.MemberwiseClone();
	}
}

