public var gun : Transform;
public var crossHair : Transform;
private var aiming = false;

function Start() {
		crossHair.position = Vector3 (-1,-1,0);
}

function Update() {
	if(Input.GetButtonDown("Fire2")){
		if (aiming){
			gun.localPosition = Vector3 (gun.localPosition.x - 0.5, gun.localPosition.y + 0.3, gun.localPosition.z);
			crossHair.position = Vector3 (-1,-1,0);
			aiming = false;
			Debug.Log("Moved weapon forward");
		} else {
			gun.localPosition = Vector3 (gun.localPosition.x + 0.5, gun.localPosition.y - 0.3, gun.localPosition.z);
			crossHair.position = Vector3 (0.5,0.5,0);
			aiming = true;
			Debug.Log("Moved weapon back");
		}
	}
}