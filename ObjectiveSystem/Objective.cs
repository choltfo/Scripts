using UnityEngine;
using System.Collections;

[System.Serializable]
[RequireComponent (typeof (TextMesh))]

public class Objective : MonoBehaviour {
	
	public string objectiveName = "Objective name";
	public string description = "Objective description";
	public bool complete = false;
	TextMesh textMesh;
	public bool Active = false;
	public int position = 0;
	public GUIStyle labelStyle;
	
	public Objective() {}
	
	void Start () {
		textMesh = gameObject.GetComponent<TextMesh>();
		textMesh.text = "";
	}
	
	void Update () {}
	
	public void Complete(){
		complete = true;
	}
	
	void OnGUI() {
		if (Active && !complete) {
			GUI.Label (new Rect(50,50+(20*position),300,20),objectiveName, labelStyle);
		}
	}
	
	public void Activate (int number) {
		position = number;
		if (Active) {
			return;
		}
		//Debug.Log("STARTING OBJECTIVE " + objectiveName);
		textMesh.text = description;
		Active = true;
	}
	
	public void Deactivate() {
		textMesh.text = "";
		Active = false;
	}
}
