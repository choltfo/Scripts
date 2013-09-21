using UnityEngine;
using System.Collections;
using System.Text;

public class SaveStateManager : MonoBehaviour {
	
	public string[] savePaths;
	
	public string savePathsPath = @"SaveGames\Index.txt";
	
	public void findFiles () {
		savePaths = System.IO.File.ReadAllLines(savePathsPath);
	}
	
	public void load (int index, out object obj) {
		obj = DeSerialize(System.IO.File.ReadAllText(savePathsPath) +
			GetGameObjectPath(((Component)obj).gameObject) + obj.GetType().ToString;
	}
	
	public void save (string path) {
		Object[] objects = Object.FindObjectsOfType(typeof(GameObject));
		XmlDocument doc = new XmlDocument();
		foreach (Object obj in objects) {
			if (obj is GameObject) {
				GameObject go = (GO)obj;
				if (!go.isStatic) {
					Component[] comps =  go.GetComponents<Component>();
					
					foreach (Component comp in comps) {
						
					}
					
				}
			}
		}
	}
	
	
	public static string GetGameObjectPath(GameObject obj) {
		string path = "/" + obj.name;
		while (obj.transform.parent != null) {
			obj = obj.transform.parent.gameObject;
			path = "/" + obj.name + path;
		}
		return path;
	}
	
	
	/*
	 * 		The plan:
	 * 		Have an iteration through all objects, storing them in a flatfile with a
	 * 		path and component type, followed by a colon and the serialized value.
	 * 
	 */
	
	
	
	// This bit by RShackleton on the Unity3D forums! 
	public static string Serialize (object obj)
{
    StringBuilder xml = new StringBuilder();
    XmlSerializer serializer = new XmlSerializer(obj.GetType());
    using (TextWriter textWriter = new StringWriter(xml))
    {
        serializer.Serialize(textWriter, obj);
    }
    return xml.ToString();
}
 

public static object DeSerialize (string xml, Type type)
{
    object obj;
    XmlSerializer deserializer = new XmlSerializer(type);
    using (TextReader textReader = new StringReader(xml))
    {
        obj = deserializer.Deserialize(textReader);
    }
		return obj;
}
		// duck on unity answers
		public static string GetGameObjectPath(GameObject obj)
{
    string path = "/" + obj.name;
    while (obj.transform.parent != null)
    {
        obj = obj.transform.parent.gameObject;
        path = "/" + obj.name + path;
    }
    return path;
}
	
}
