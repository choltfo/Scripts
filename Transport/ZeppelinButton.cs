using UnityEngine;
using System.Collections;

public class ZeppelinButton : InteractObject {
	public Zeppelin zp;

	public override void Interact (GameObject player, ShootObjects SOPlayer) {
		zp.activate(player);
	}
}