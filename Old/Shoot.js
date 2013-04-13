public var gun : Transform;
public var gunAngle : int;
public var Hammer : Transform;
public var HammerRotation : int;
public var Slide : Transform;
public var SlideDistance : double;
public var Trigger : Transform;
public var TriggerDistance : double;
//public var Shell : GameObject;
//public var ShellSpeed : int;
public var ShellDestructorDelay : int;
public var Muzzle_Flash : Transform;
public var Muzzle_Flash_time : int;
public var ShotDelay: int = 5;
public var SlideDelay: int = 2;
private var ShotClock : int = 0;
public var clip : int = 6;
public var maxClip : int = 6;

public var crossHair : Transform;
private var aiming = false;

private var ReloadControlClock : int = 0;
public var ReloadgunAngle : int;
public var ReloadgunSway : double;
public var ReloadgunDip : double;
public var ReloadSlideDistance : double;


function Start () {
	Muzzle_Flash.localScale = Vector3 (0,0,0);
	crossHair.position = Vector3 (-1,-1,0);
}

function Update () {
	if (Input.GetKeyDown("r")) {
		clip = maxClip;
	}
	if (ShotClock > 0){
		ShotClock--;
	}
	if ((ShotClock == 0) && (clip > 0)){
		if (Input.GetButtonDown("Fire1")){
			clip--;
			Hammer.Rotate(-HammerRotation, 0, 0);
			gun.Rotate(-gunAngle,0,0);
			Slide.Translate(0,0, SlideDistance);
			Trigger.Translate(0,0, TriggerDistance);
			Muzzle_Flash.localScale = Vector3 (10,10,10);
			ShotClock = ShotDelay;
			//var newShell : GameObject = Instantiate(Shell, transform.position, Shell.transform.rotation);
			//newShell.AddComponent("Rigidbody");
			//newShell.rigidbody.AddForce(Vector3.right * 10);
		}
	}
	if (ShotClock == (ShotDelay - 1)){
		gun.Rotate(gunAngle*0.25,0,0);
	}
	if (ShotClock == (ShotDelay - 2)){
		gun.Rotate(gunAngle*0.25,0,0);
	}
	if (ShotClock == (ShotDelay - 3)){
		gun.Rotate(gunAngle*0.25,0,0);
	}
	if (ShotClock == (ShotDelay - 4)){
		gun.Rotate(gunAngle*0.25,0,0);
		Trigger.Translate(0,0, -TriggerDistance);
	}
	if (ShotClock == (ShotDelay-SlideDelay)){
		Hammer.Rotate(HammerRotation, 0, 0);
		Slide.Translate(0,0, -SlideDistance);
	}
	if (ShotClock == (ShotDelay-Muzzle_Flash_time)){
		Muzzle_Flash.localScale = Vector3 (0,0,0);
	}
	
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
	
	if (Input.GetKeyDown("r")) {
		print("Reload key pressed");
		if (clip < maxClip) {
			ReloadControlClock = 30;
			gun.Translate(0,-ReloadgunDip,0);
			gun.Rotate(0,0,ReloadgunAngle);
			clip = maxClip;
			print("Moved Gun");
		}
	}
	if (ReloadControlClock == 20){
		print("Moved gun Back");
		gun.Translate(0,ReloadgunDip,0);
		gun.Rotate(0,0,-ReloadgunAngle);
	}
	if (ReloadControlClock > 0){
		ReloadControlClock--;
	}
}