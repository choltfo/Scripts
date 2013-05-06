using UnityEngine;
using System.Collections;

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;

	void Start () {}
	void Update () {}

	public override void Interact (GameObject player) {
		player.transform.position = linkedTeleporter.rigidbody.position;
	}
}