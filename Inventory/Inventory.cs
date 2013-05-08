using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]

public class Inventory {
	public float cash; 
	Weapon[] tempWeapons = new Weapon[2];
	public List<Weapon> weapons = new List<Weapon>(tempWeapons);
	public int[] ammo = new int[Enum.GetNames(typeof(AmmoType)).Length];
	public List<Grenade> grenades = new List<Grenade>();
	public Items[] items;
}
