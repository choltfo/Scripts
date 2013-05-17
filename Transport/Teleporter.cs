using UnityEngine;
using System.Collections;

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;  // The teleporter this is linked to
	public Vector3 relativePosition;  // How far away the player is moved when teleported to this teleporter
	public CameraFade fader;
	public Color fadeColor;
	public Color transparency;
	public float fadeTime;
	public bool isPortal = false;
	
	bool faded = true;
	float beginFadeAt;

	public override void Interact (GameObject player) {
		if (isPortal) {
			return;
		}
		player.transform.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
		fader.SetScreenOverlayColor(fadeColor);
		beginFadeAt = Time.time;
		faded = false;
	}
	public void OnTriggerEnter(Collider thing){
		if (isPortal) {
			thing.transform.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
		}		
	}
	public void Update () {
		if (beginFadeAt + fadeTime > Time.time && !faded) {
			fader.StartFade(transparency,fadeTime);
			faded = true;
		}
	}
}