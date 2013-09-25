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
		int dataPoint = 0;
		for (int i = 0; i < ammo.Length; i++) {	// Pull all the ammo data, and continue.
			ammo[i] = int.Parse(data[i]);
			dataPoint ++;
		}
		
		loadWeapons(data[dataPoint+1], data[dataPoint+2]);
		
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
		string weaponZero = weapons[0].SerializeXml();
		string[] weaponZeroAsArray = weaponZero.Split('\n');
		weaponZero = String.Join("",weaponZeroAsArray);
		weaponZeroAsArray = weaponZero.Split('\r');
		weaponZero = String.Join("",weaponZeroAsArray);
		
		string weaponOne = weapons[0].SerializeXml();
		string[] weaponOneAsArray = weaponZero.Split('\n');
		weaponOne = String.Join("",weaponZeroAsArray);
		weaponOneAsArray = weaponZero.Split('\r');
		weaponOne = String.Join("",weaponZeroAsArray);
		
		return "WEAPONS#"+weaponZero+"#"+weaponOne;
	}
	
	public void loadWeapons (string W1, string W0) {
		loadWeapon(W0, 0);
		loadWeapon(W1, 1);
	}
	
	public void loadWeapon (string xml, int index) {
		weapons[index] = XmlSupport.DeserializeXml<Weapon>(xml);
	}
	
	public string saveGrenades () {
		string output = "";
		
		
		
		return output;
	}
	
	public void loadGrenades (string input) {
		
	}
}
