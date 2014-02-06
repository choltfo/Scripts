using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

public class WeaponCreationHandler : EditorWindow {

	public WeaponPickup WP = null;
	public WeaponAsset WA = null;
	int UID = 0;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/Weapon editor")]
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(WeaponCreationHandler));

	}

	void OnGUI() {
		GUILayout.Label ("Select pickup and DB", EditorStyles.boldLabel);
		UID = EditorGUILayout.IntField("UID: ",UID);
		WP = (WeaponPickup)EditorGUILayout.ObjectField("Pickup:",WP, typeof(WeaponPickup));
		WA = (WeaponAsset)EditorGUILayout.ObjectField("Asset List:",WA, typeof(WeaponAsset));

		if (GUILayout.Button("Add weapon to DB")) {
			UID = WeaponHandler.saveWeapon(WP.thisGun);
		}
	}
}
