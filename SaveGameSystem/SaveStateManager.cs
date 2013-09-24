using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class SaveStateManager : MonoBehaviour {
	public string savePath = @"Index.txt";
	
	public void load () {
		String line;
		try {
			//Pass the file path and file name to the StreamReader constructor
			StreamReader sr = new StreamReader(savePath);
			//Read the first line of text
			line = sr.ReadLine();

			//Continue to read until you reach end of file
			while (line != null) {
				//write the lie to console window
				Debug.Log("LOAD: " + line);
				if (!line.StartsWith("//")) loadPerGO (line);
				//Read the next line
				line = sr.ReadLine();
			}
				//close the file
			sr.Close();
		}
		catch(Exception e) {
			Debug.Log("Exception: " + e.Message);
		}
   		finally {
			Debug.Log("Executing finally block.");
		}
	}
	
	public void loadPerGO (string S) {
		
		string[] parts = S.Split('$');		// Seperates path and each data block.
											// Each type of compoenent must have a specific code section programmed below.
		string path = parts[0];
		//string[] objs = new string[parts.Length-1];
		//Array.Copy(parts, 1, objs, 0, parts.Length-2);	// Copy each part of the string that isn't the path to an array.
		
		GameObject go = GameObject.Find(path);
		
		print("Found GameObject, attempting variable changes....");
		
		if (go != null) {
			print ("GameObject at '"+path+"' was not null, continuing...");
			for (int i = 1; i < parts.Length; i++) {		// For each component declaration in the string...
				string[] vals = parts[i].Split(':');	// Get each relevant set of values...
				string type = vals[0];			// And the type.
				
				//Component c = go.GetComponent(type);
				print (type);
				switch (type) {
					case "Transform" :
						print ("Tweaking transform....");
						string position = vals[1];
						string rotation = vals[2];
						string scale    = vals[3];
						
						string[] posStrings = position.Split(',');
						
						float xpos = float.Parse(posStrings[0]);
						float ypos = float.Parse(posStrings[1]);
						float zpos = float.Parse(posStrings[2]);
					
						go.transform.localPosition = new Vector3(xpos, ypos, zpos);
					
						// IMPORTANT: THIS MUST BE LocalEulerAngles!
						string[] rotStrings = rotation.Split(',');
						
						float xrot = float.Parse(rotStrings[0]);
						float yrot = float.Parse(rotStrings[1]);
						float zrot = float.Parse(rotStrings[2]);
					
						go.transform.Rotate(xrot, yrot, zrot);
						
						
						string[] scaleString = scale.Split(',');
						
						float xsca = float.Parse(scaleString[0]);
						float ysca = float.Parse(scaleString[1]);
						float zsca = float.Parse(scaleString[2]);
					
						go.transform.localScale = new Vector3(xsca, ysca, zsca);
						
						
					break;
					
					// ADD NEW CASES HERE!
					
					//case "Health" :
						
					//break;
				}
				
				
				
			}
		}
		
		return;	// Why not?
	
	}

	public void save () {
		// Write the string to a file.
		System.IO.StreamWriter file = new System.IO.StreamWriter(savePath);
		
		
		object[] allObjects = FindObjectsOfTypeAll(typeof(GameObject));
		foreach(object thisObject in allObjects) {
			if (((GameObject) thisObject).activeInHierarchy && !((GameObject) thisObject).isStatic) {
				print(thisObject+" is an active object") ;
				file.WriteLine(savePerGO((GameObject)thisObject));
			}
		}
		file.Close();
	}
	
	public String savePerGO (GameObject go) {
		String output = GetGameObjectPath(go);
		foreach (Component c in go.GetComponents<Component>()) {
			String CompOut = "";
			if (c is Transform) {
				Transform t = (Transform)c;
				CompOut += "Transform:"+
					t.localPosition.x+","+
					t.localPosition.y+","+
					t.localPosition.z+":"+
						
					t.localEulerAngles.x+","+
					t.localEulerAngles.y+","+
					t.localEulerAngles.z+":"+
						
					t.localPosition.x+","+
					t.localPosition.y+","+
					t.localPosition.z;
				output += "$" + CompOut;
			}
		}
		return output;
	}
	
	
	// Thanks to 'duck' on unity answers!
	public static string GetGameObjectPath(GameObject obj) {
		string path = "/" + obj.name;
		while (obj.transform.parent != null) {
			obj = obj.transform.parent.gameObject;
			path = "/" + obj.name + path;
		}
		return path;
	}
}
