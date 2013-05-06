using UnityEngine;
using System.Collections;

public class Teleporter : InteractObject {
	public Teleporter linkedTeleporter;
	public GameObject player;

	void Start () {}
	void Update () {}

	public override void Interact () {
		player.transform.position = linkedTeleporter.rigidbody.position;
	}
}