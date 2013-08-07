using UnityEngine;
using System.Collections;

/// <summary>
/// An NPC that you can talk to.
/// </summary>
public class InteractNPC : InteractObject {
	
	public Conversation[] convos;
	public bool pauseTime = true;
	
	bool talking = false;
	
	public override void Interact(GameObject interacter) {
		talking = true;
	}
	
	// Use this for initialization
	void Start () {
		foreach (Conversation convo in convos) {
			convo.Init();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
