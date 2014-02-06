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
<<<<<<< HEAD
		return (WeaponAsset[])Resources.FindObjectsOfTypeAll(typeof(WeaponAsset));
	}
	
	/*public static Weapon convertAsset (WeaponAsset WA) {
		
		Weapon W = new Weapon();
		
		foreach (FieldInfo f in typeof(Weapon).GetFields()) {
			string name = f.Name;
			
			object obj = typeof(WeaponAsset).GetField(name).GetValue(WA);
			
			typeof(Weapon).GetField(name).SetValue(W, obj);
		}
		
		return W;
	}*/
=======
		UnityEngine.Object[] OBJS = Resources.FindObjectsOfTypeAll(typeof(WeaponAsset));
		Debug.Log("Object found, "+OBJS[0].name);
		return (WeaponAsset[])OBJS;
	}
>>>>>>> 5de83f1f12b1c4cc04d07e95aceb02f51de6a5b5
}
