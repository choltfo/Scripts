using UnityEngine;
using System;

[System.Serializable]

public class Inventory {
	public Weapon[] weapons = new Weapon[2];
	public int[] ammo = new int[Enum.GetNames(typeof(AmmoType)).Length];
	public Grenade[] grenades;
	public Items[] items;
}
