using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

[System.Serializable]
public class WeaponAsset : ScriptableObject {
	public List<Weapon> weapons = new List<Weapon>();

	[MenuItem("Assets/Create/New Weapon")]
	public static void CreateAsset ()
	{
		CreateAsset<WeaponAsset> ();
	}

	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T> () where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
 
		string filePath = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (filePath == "") 
		{
			filePath = "Assets";
		} 
		else if (Path.GetExtension (filePath) != "") 
		{
			filePath = filePath.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
 
		string assetfilePathAndName = AssetDatabase.GenerateUniqueAssetPath (filePath + "/New " + typeof(T).ToString() + ".asset");
 
		AssetDatabase.CreateAsset (asset, assetfilePathAndName);
 
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
	
}
