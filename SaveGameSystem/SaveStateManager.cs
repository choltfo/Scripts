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
				Debug.log("LOAD: " + line);
				loadPerGO (line);
				//Read the next line
				line = sr.ReadLine();
			}
				//close the file
			sr.Close();
		}
		catch(Exception e) {
			Console.WriteLine("Exception: " + e.Message);
		}
   		finally {
			Console.WriteLine("Executing finally block.");
		}
	}
	
	public void loadPerGO (string S) {
		
		string[] parts = S.Split("$");		// Seperates path and each data block.
											// Each type of compoenent must have a specific code section programmed below.
		string path = parts[0];
		string[] objs = new string[parts.Length-1];
		Array.copy(parts, 1, objs, 0, parts.Length-2);	// Copy each part of the string that isn't the path to an array.
		
		GameObject go = GameObject.Find(path);
		
		if (go != null) {
			foreach (String s in objs) {	// For each component declaration in the string...
				string[] vals = val.Split(":");	// Get each relevant set of values...
				string type = vals[0];			// And the type.
				
				Component c = go.GetComponent(type);
				
				switch (typeof(c)) {
					case typeof(Transform) :
						string position = vals[1];
						string rotation = vals[2];
						string scale    = vals[3];
						
						string[] posStrings = position.Split(",");
						
						float xpos = float.Parse(posStrings[0]);
						float ypos = float.Parse(posStrings[1]);
						float zpos = float.Parse(posStrings[2]);
					
						gameObject.transform.localPosition = new Vector3(xpos, ypos, zpos);
						// IMPORTANT: THIS MUST BE LocalEulerAngles!
						string[] rotStrings = rotation.Split(",");
						
						float xrot = float.Parse(rotStrings[0]);
						float yrot = float.Parse(rotStrings[1]);
						float zrot = float.Parse(rotStrings[2]);
					
						gameObject.transform.Rotate(xrot, yrot, zrot);
						
						
					break;
				}
				
			}
		}
		
		return;	// Why not?
	
	}
}
