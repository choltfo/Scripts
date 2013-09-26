using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System;

public static class WeaponHandler {
	public static Weapon getWeapon (int UID) {
		WeaponAsset[] weapons = getWeapons();
		foreach (WeaponAsset WA in weapons) {
			if (WA.UID == UID) {
				return convertAsset(WA);
			}
		}
		Debug.LogError("Invalid getWeapon call! No UID #" + UID + " found!");
		return new Weapon();
	}
	
	public static Weapon getWeapon (string name) {
		WeaponAsset[] weapons = getWeapons();
		foreach (WeaponAsset WA in weapons) {
			if (WA.WeaponName == name || WA.DisplayName == name) {
				return convertAsset(WA);
			}
		}
		Debug.LogError("Invalid getWeapon call! No weapon " + name + " found!");
		return new Weapon();
	}
	
	public static WeaponAsset[] getWeapons () {
		return (WeaponAsset[])Resources.FindObjectsOfTypeAll(typeof(WeaponAsset));
	}
	
	public static Weapon convertAsset (WeaponAsset WA) {
		
		Weapon W = new Weapon();
		
		foreach (FieldInfo f in typeof(Weapon).GetFields()) {
			string name = f.Name;
			
			object obj = typeof(WeaponAsset).GetField(name).GetValue(WA);
			
			typeof(Weapon).GetField(name).SetValue(W, obj);
		}
		
		return new Weapon();
	}
}
