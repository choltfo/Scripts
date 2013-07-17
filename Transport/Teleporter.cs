using UnityEngine;
using System.Collections;

/// <summary>
/// A teleporter
/// Transports the player to the
/// Other location
/// </summary>
public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;  // The teleporter this is linked to
	public Vector3 relativePosition;  // How far away the player is moved when teleported to this teleporter
	public CameraFade fader;
	public Color fadeColor;
	public Color transparency;
	public float fadeTime;

	/// <summary>
	/// A boolean var
	/// Whether activated on
	/// Collision event
	/// </summary>
	public bool isPortal = false;
	
	bool faded = true;
	float beginFadeAt;

	/// <summary>
	/// When interacted
	/// By the player clicking 'E'
	/// This is what happens
	/// </summary>
	public override void Interact (GameObject player) {
		//if (isPortal) {
		//	return;
		//}
		player.transform.root.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
		fader.SetScreenOverlayColor(fadeColor);
		beginFadeAt = Time.time;
		faded = false;
	}

	/*
	/// <summary>
	///	Teleports player
	/// On a collision event
	/// If isPortal true
	/// </summary>
	public void OnTriggerEnter(Collider thing){
		if (isPortal) {
			thing.transform.root.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
		}		
	}*/
		
	public void Update () {
		if (beginFadeAt + fadeTime > Time.time && !faded) {
			fader.StartFade(transparency,fadeTime);
			faded = true;
		}
	}
}