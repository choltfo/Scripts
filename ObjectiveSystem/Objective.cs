using UnityEngine;
using System.Collections;

[System.Serializable]
//[RequireComponent (typeof (TextMesh))]
/// <summary>
/// Represents an Objective inside a <see cref="Mission"/>, inside a <see cref="Campaign"/>.
/// </summary>
public class Objective : MonoBehaviour {
	
	/// <summary>
	/// The name of this objective.
	/// </summary>
	public string objectiveName = "Objective name";
	/// <summary>
	/// The description.
	/// </summary>
	public string description = "Objective description";
	/// <summary>
	/// Whether this is complete.
	/// </summary>
	public bool complete = false;
	/// <summary>
	/// The text mesh attached to this object.
	/// </summary>
	TextMesh textMesh;
	/// <summary>
	/// Whether this is active.
	/// </summary>
	public bool Active = false;
	/// <summary>
	/// The position to draw this at in the objective list.
	/// </summary>
	public int position = 0;
	/// <summary>
	/// The label style.
	/// </summary>
	public GUIStyle labelStyle;
	public TriggerableEvent[] Events;
	
	//void Start () {
		//textMesh = gameObject.GetComponent<TextMesh>();
		//textMesh.text = "";
	//
	
	/// <summary>
	/// Complete this instance.
	/// </summary>
	public bool Complete(){
		if (!complete && Active) {
			complete = true;
			foreach (TriggerableEvent TEvent in Events) {
					TEvent.Trigger();
			}
			return true;
		}
		return false;
	}
	
	void OnGUI() {
		if (Active && !complete && Time.timeScale!=0) {
			GUI.Label (new Rect(50,50+(20*position),300,20),objectiveName, labelStyle);
		}
	}
	/// <summary>
	/// Activate this objective, and set its position to a number.
	/// </summary>
	/// <param name='number'>
	/// This objectives new position.
	/// </param>
	public void Activate (int number) {
		position = number;
		if (Active) {
			return;
		}
		//Debug.Log("STARTING OBJECTIVE " + objectiveName);
		//textMesh.text = description;
		Active = true;
	}
	
	/// <summary>
	/// Deactivate this instance.
	/// </summary>
	public void Deactivate() {
		//textMesh.text = "";
		Active = false;
	}
}
