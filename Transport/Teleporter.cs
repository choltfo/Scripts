using UnityEngine;
using System.Collections;

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;
	public Vector3 relativePosition;
	public const bool isTrampoline = false;
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
	}
	public void Update () {
		if (beginFadeAt + fadeTime > Time.time && !faded) {
			fader.StartFade(transparency,fadeTime);
			faded = true;
		}
	}
}