public var ShootController : Shoot;
private var ControlClock : int = 0;
public var Gun : Transform;
public var GunAngle : int;
public var GunSway : double;
public var GunDip : double;
public var Slide : Transform;
public var SlideDistance : double;


function Start () {
}

function Update () {
	if (Input.GetKeyDown("r")) {
		print("Reload key pressed");
		if (ShootController.clip < ShootController.maxClip) {
			ControlClock = 30;
			Gun.Translate(0,-GunDip,0);
			Gun.Rotate(0,0,GunAngle);
		}
		ShootController.clip = ShootController.maxClip;
	}
	if (ControlClock == 20){
		print("Moved gun Back");
		Gun.Translate(0,GunDip,0);
	}
	if (ControlClock > 0){
		ControlClock--;
	}
}