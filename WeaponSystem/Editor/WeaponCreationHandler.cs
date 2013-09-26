using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using System;

public class WeaponCreationHandler : EditorWindow {
	
	public WeaponPickup WP = null;
	public WeaponAsset WA = null;
	
	string myString = "Hello World";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;
	
	int UID = 0;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/Weapon editor")]
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(WeaponCreationHandler));
	}

	void OnGUI() {
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		UID = EditorGUILayout.IntField(UID);
		
		WP = (WeaponPickup)EditorGUILayout.ObjectField(WP, typeof(WeaponPickup));
		WA = (WeaponAsset)EditorGUILayout.ObjectField(WA, typeof(WeaponAsset));
		if (GUILayout.Button("Save data!")) {
			WP.thisGun.UID = UID;
			weaponTranScript();
		}
		if (GUILayout.Button("Get # of weapons")) {
			UnityEngine.Object[] GUNS = Resources.FindObjectsOfTypeAll(typeof(WeaponAsset));
			Debug.Log(GUNS.Length);
			foreach (UnityEngine.Object g in GUNS) {
				Debug.Log(((WeaponAsset)g).DisplayName, g);
			}
		}
	}
	
	public void weaponTranScript () {
		foreach (FieldInfo f in typeof(Weapon).GetFields()) {
			string name = f.Name;
			
			object obj = typeof(Weapon).GetField(name).GetValue(WP.thisGun);
			
			typeof(WeaponAsset).GetField(name).SetValue(WA, obj);
		}
	}
	
	public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
{
    return ((MemberExpression)memberAccess.Body).Member.Name;
}
}
