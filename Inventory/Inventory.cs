using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]

public class Inventory {
	public float cash;
	public Weapon[] weapons = new Weapon[2];
	public int[] ammo = new int[Enum.GetNames(typeof(AmmoType)).Length];
	public List<Grenade> grenades = new List<Grenade>();
	public Items[] items;
	public List<WeaponAttachment> attachments;
	public List<FoodStuff> foods;
	
	
	// Receives all the data sections after 'Shootobjects:'
	public void load (string[] data) {
		
		return;
	}
	
	public string save () {
		string output = "ShootObjects:";
		
		int i = 0;
		foreach (int am in ammo) {
			output += am.ToString();
			if (i != ammo.Length-1) output += ",";
			i ++;
		}
		output += ":";
		
		
		
		return output;
	}
	
	public string saveGrenades () {
		string output;
		
		return output;
	}
	
	public void loadFrenades (string input) {
		
	}
}
