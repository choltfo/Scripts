using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;  // The teleporter this is linked to
	public Vector3 relativePosition;  // How far away the player is moved when teleported to this teleporter
	public bool isTrampoline = true;  // Whether this is a trampoline
	public CameraFade fader;
	public Color fadeColor;
	public Color transparency;
	public float fadeTime;
	
	bool faded = true;
	float beginFadeAt;

	public override void Interact (GameObject player) {
		player.transform.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
		fader.SetScreenOverlayColor(fadeColor);
		beginFadeAt = Time.time;
		faded = false;
		if (isTrampoline) {
			print("This is a trampoline");
			relativePosition.y += 100;
		}
		else {
			print("This is not a trampoline");
		}

	}
	public void Update () {
		if (beginFadeAt + fadeTime > Time.time && !faded) {
			fader.StartFade(transparency,fadeTime);
			faded = true;
		}
	}
}