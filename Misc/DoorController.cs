using UnityEngine;
using System.Collections;

public class DoorController : InteractObject {
	
	public Door door; 
	
	public override void Interact (GameObject player, ShootObjects SOPlayer) {
		door.setState(!door.isOpen);
	}
}
