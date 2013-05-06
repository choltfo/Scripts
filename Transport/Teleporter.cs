using UnityEngine;
using System.Collections;

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;
	public Vector3 relativePosition;
	public const bool isTrampoline = true;

	void Start () {}
	void Update () {}

	public override void Interact (GameObject player) {
		player.transform.position = linkedTeleporter.transform.position + linkedTeleporter.relativePosition;
	}
}