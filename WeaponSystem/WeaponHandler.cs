using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System;

public static class WeaponHandler {

	public static string WeaponAssetPath = @"";

	public static int saveWeapon (Weapon weapon) {
		WeaponAsset WA = getWeapons()[0];

		if (WA.weapons.Contains(weapon)) {
			return weapon.UID = WA.weapons.IndexOf(weapon);
		} else {
			weapon.UID = WA.weapons.Count;
			WA.weapons.Add(weapon);
			return WA.weapons.Count-1;
		}
	}

	public static Weapon getWeapon (int index) {
		WeaponAsset WA = getWeapons()[0]; // This is the hard part...

		return WA.weapons[(index >= WA.weapons.Count ? 0 : index)];
	}

	public static WeaponAsset[] getWeapons () {
		UnityEngine.Object[] OBJS = Resources.FindObjectsOfTypeAll(typeof(WeaponAsset));
		Debug.Log("Object found, "+OBJS[0].name);
		return (WeaponAsset[])OBJS;
	}
}
