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
	int position = 0;
	
	public Objective() {}
	
	void Start () {
		textMesh = (TextMesh)gameObject.GetComponent("TextMesh");
		textMesh.text = "";
	}
	
	void Update () {}
	
	public bool checkCompletion(){
		return complete;
	}
	
	public void Complete(){
		complete = true;
	}
	
	void OnGUI() {
		if (Active && !complete) {
			GUI.Label (new Rect(50,50+(20*position),300,20),objectiveName);
		}
	}
	
	public void Activate (int number) {
		if (Active) {
			return;
		}
		//Debug.Log("STARTING OBJECTIVE " + objectiveName);
		textMesh.text = description;
		position = number;
		Active = true;
	}
	
	public void Disactivate() {
		textMesh.text = "";
		Active = false;
	}
}
