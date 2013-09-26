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
	//public List<FoodStuff> foods;
	
	
	// Receives all the data sections after 'Shootobjects:'
	public void load (string[] data) { 
		int dataPoint = 2;	// So that it skips over the 'ShootObjects' declarator, and the 'AMMO' declarator.
		string[] newAmmo = (data[dataPoint]).Split(',');
		for (int i = 0; i < ammo.Length; i++) {	// Pull all the ammo data, and continue.
			Debug.Log("Ammo type " + i.ToString() + " = " + newAmmo[i]);
			ammo[i] = int.Parse(newAmmo[i]);
		}
		dataPoint ++;
		
		foreach(var item in data)
  		  Debug.Log(item.ToString());
		
		string[] weapons = data[dataPoint].Split('#');
		
		Debug.Log("Weapon UIDs: " + weapons[1] + " and " + weapons[2]);
		loadWeapons(weapons[1], weapons[2]);
		
		dataPoint+=2;
		
		// The next one
		return;
	}
	
	public string save () {
		string output = "ShootObjects:AMMO:";
		
		int i = 0;
		foreach (int am in ammo) {
			output += am.ToString();
			if (i != ammo.Length-1) output += ",";
			i ++;
		}
		output += ":";
		
		output += saveWeapons() + ":";
		
		
		return output;
	}
	
	public string saveWeapons () {
		return "WEAPONS#"+weapons[0].UID+"#"+weapons[1].UID;
	}
	
	public void loadWeapons (string W1, string W0) {
		loadWeapon(int.Parse(W0), 0);
		loadWeapon(int.Parse(W1), 1);
	}
	
	public void loadWeapon (int UID, int index) {
		weapons[index] = WeaponHandler.getWeapon(UID);
	}
	
	public string saveGrenades () {
		string output = "";
		
		
		
		return output;
	}
	
	public void loadGrenades (string input) {
		
	}

	
}
