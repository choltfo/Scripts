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
}
