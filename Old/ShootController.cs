// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootController : MonoBehaviour {
	
	public Controls controls;
	
	public ShootObjects shootObjects;
	
	public string DefaultGun = "P220";
	public string[] Weapons;
	public bool[] IsInPossession;
	public AudioSource[] SoundFXs;
	public Transform[] Guns;
	public Transform[] Hammers;
	public Transform[] Slides;
	public Transform[] Triggers;
	public Transform[] Flashes;
	public int[] maxClips;
	public Vector3[] ModelHoldLocations;
	public Vector3 ModelHideLocation;
	
	public int CameraClimb;
	public MouseLook FPSCamera;
	public Transform gun;
	public float gunAngle;
	public Transform Hammer;
	public float HammerRotation;
	public Transform Slide;
	public float SlideDistance;
	public Transform Trigger;
	public float TriggerDistance;
	public GameObject Shell;
	public float ShellSpeed;
	public int ShellDestructorDelay;
	public Transform Muzzle_Flash;
	public int Muzzle_Flash_time;
	public int ShotDelay = 5;
	public int SlideDelay = 2;
	private int ShotClock = 0;
	public int clip = 6;
	public int maxClip = 6;

	public Transform crossHair;
	public bool aiming = false;
	
	private int ReloadControlClock = 0;
	public float ReloadgunAngle;
	public float ReloadgunSway;
	public float ReloadgunDip;
	public float ReloadSlideDistance;
	
void  Start (){
	Muzzle_Flash.localScale = new Vector3 (0,0,0);
	crossHair.Translate(-1,-1,0);
	SelectWeapon(DefaultGun);
}

void  Update (){
	if (ShotClock > 0){
		ShotClock--;
	}
	if ((ShotClock == 0) && (clip > 0)){
		if (Input.GetMouseButtonDown(controls.fire)){
			clip--;
			Hammer.Rotate(-HammerRotation, 0, 0);
			gun.Rotate(-gunAngle,0,0);
			Slide.Translate(0,0, SlideDistance);
			Trigger.Translate(0,0, TriggerDistance);
			Muzzle_Flash.localScale = new Vector3 (10,10,10);
			ShotClock = ShotDelay;
			FPSCamera.rotationY += CameraClimb;
		}
	}
	if (ShotClock == (ShotDelay - 1)){
		gun.Rotate(gunAngle*0.25f,0,0);
	}
	if (ShotClock == (ShotDelay - 2)){
		gun.Rotate(gunAngle*0.25f,0,0);
	}
	if (ShotClock == (ShotDelay - 3)){
		gun.Rotate(gunAngle*0.25f,0,0);
	}
	if (ShotClock == (ShotDelay - 4)){
		gun.Rotate(gunAngle*0.25f,0,0);
		Trigger.Translate(0,0, -TriggerDistance);
	}		
	if (ShotClock == (ShotDelay-SlideDelay)){
		Hammer.Rotate(HammerRotation, 0, 0);
		Slide.Translate(0,0, -SlideDistance);
	}
	if (ShotClock == (ShotDelay-Muzzle_Flash_time)){
		Muzzle_Flash.localScale = new Vector3 (0,0,0);
	}
	
	if(Input.GetMouseButtonDown(controls.aim)){
		if (aiming){
			gun.localPosition = new Vector3 (gun.localPosition.x - 0.5f, gun.localPosition.y + 0.3f, gun.localPosition.z);
			//crossHair.position = new Vector3 (-1,-1,0);
			crossHair.Translate((float)-0.5,(float)-0.5,0);
			aiming = false;
			Debug.Log("Moved weapon forward");
		} else {
			gun.localPosition = new Vector3 (gun.localPosition.x + 0.5f, gun.localPosition.y - 0.3f, gun.localPosition.z);
			//crossHair.position = new Vector3 (0.5f,0.5f,0);
			crossHair.Translate((float)0.5,(float)0.5,0);
			aiming = true;
			Debug.Log("Moved weapon back");
		}
	}
	
	if (Input.GetKeyDown(controls.reload)) {
		print("Reload key pressed");
		if (clip < maxClip) {
			print("moving");
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

public void SelectWeapon (string gunName) {
	bool exists = false;
	int number = 0; 
	for (int i = 0; i < Weapons.Length; i++) {
		if (gunName == Weapons[i]) {
			exists = true;
			number = i;
		}
	}
	if (exists) {
		print ("Gun " + gunName + " Is in the weapons list. # " + number.ToString());
		SelectWeapon(number);
	} else {
		print ("Gun " + gunName + " Is not in the weapons list.");
	}
}

public void SelectWeapon (int number) {
	if (Weapons.Length <= number) {
		print ("Weapon number " + number + " Does not exist.");
		return;
	}
	if (IsInPossession[number] == false){
			print ("Player does not have weapon number " + number);
			return;
	}
	aiming = false;
	gun.localPosition = ModelHideLocation;
	gun = Guns[number];
	Hammer = Hammers[number];
	Slide = Slides[number];
	Trigger = Triggers[number];
	Muzzle_Flash = Flashes[number];
	maxClip = maxClips[number];
	shootObjects.GunSoundFX = SoundFXs[number];
	gun.localPosition = ModelHoldLocations[number];
	ShotClock = 0;
}
}