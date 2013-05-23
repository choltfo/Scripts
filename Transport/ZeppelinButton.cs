using UnityEngine;
using System.Collections;

public class ZeppelinButton : InteractObject {
	public Zeppelin zp;

	public override void Interact (GameObject player) {
		zp.activate(player);
	}
}