using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {
	
	public GameObject InstantiableObject;
	public GameObject InstantiablePickup;
	public GameObject BulletHole;
	public GameObject DirtSpray;
	public GameObject BloodSpray;
	public Vector3 Position;
	public Vector3 ScopedPosition;
	public bool CanScope;
	public int Damage;
	public int FireRateAsPercent;
	double fireRate;
	public int MaxAmmo;
	public int CurAmmo;
	public string WeaponName;
	public string DisplayName;
	public string Path = ".../";
	public bool IsValid = true;
	public bool Automatic;
	//TODO: Add things for making blood splatters, dirt sprays, and bullet holes. 
	
	GameObject camera; 
	bool isAimed = false;
	public bool Exists = false;
	bool isFiring = false;
	GameObject mainObject = null;
	GameObject flash = null;
	int AnimClock = 0;
	
	Transform Hammer;
	Transform Slide;
	Transform Trigger;
	
	public float CameraClimb = 2f;
	public float maxCameraSway = 2f;
	public float gunAngle = 5f;
	public float gunDistance = 0.05f;
	public float HammerRotation = 2f;
	public float SlideDistance = 0.05f;
	public float TriggerDistance = 0.05f;
	
	public int SlideDelay;
	int ShotDelay;
	
	//TODO Add reloading animations.
	
	public Weapon(GameObject MainObject, Vector3 Position, Vector3 ScopedPosition,
		bool CanScope, int MaxAmmo, string WeaponName, int Damage, string Path, float FireRate, bool Automatic) {
		
		this.InstantiableObject = MainObject;
		this.Position = Position;
		this.ScopedPosition = ScopedPosition;
		this.CanScope = CanScope;
		this.MaxAmmo = MaxAmmo;
		CurAmmo = MaxAmmo;
		this.WeaponName = WeaponName;
		this.IsValid = true;
		this.Damage = Damage;
		this.Path = Path;
		this.fireRate = FireRate;
		this.Automatic = Automatic;
	}
	
	public Weapon() {}
	
	
	/*
	 * Instantiates the gun in front of the player (camera), so that is can be usefully animated.
	 */
	public void activate(GameObject player){
		camera = player;
		GameObject Gun = (GameObject)MonoBehaviour.Instantiate(InstantiableObject, new Vector3 (0,0,0), player.transform.rotation);
		Gun.transform.parent = player.transform;
		Gun.transform.localPosition = Position;
		mainObject = Gun;
		flash = GameObject.Find(mainObject.transform.name + "/" + Path + "Flash");
		if (flash != null) {
			Debug.Log("Found flash for " + WeaponName + " at " + mainObject.transform.name +  "/" + Path + flash.name);
			flash.transform.localScale = new Vector3 (0,0,0);
		} else {
			Debug.Log("Didn't find flash for " + WeaponName + " at " + mainObject.transform.name + "/" + Path + "Flash");
		}
		flash.transform.localScale = new Vector3 (0,0,0);
		//MonoBehaviour.print("Added " + WeaponName);
		AnimIdentify();
		isAimed = false;
		Exists = true;
	}
	
	public void disactivate() {
		if (mainObject != null) {
			MonoBehaviour.Destroy(mainObject);
			//MonoBehaviour.print("Destryoed " + WeaponName);
			Exists = false;
		}
	}
	
	public void aim() {
		if (!IsValid || !Exists) {
			return;
		}
		if (isAimed) {
			mainObject.transform.localPosition = Position;
			isAimed = false;
		} else {
			mainObject.transform.localPosition = ScopedPosition;
			isAimed = true;
		}
	}
	
	public bool Shoot(Camera camera) {
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;
		if (CurAmmo > 0/*TODO Modify:  && !vehicle.riding*/ && !isFiring){
			CurAmmo -= 1;
			((AudioSource)mainObject.GetComponent("AudioSource")).Play();
			
			//Debug.Log("Setting anim clock to " + ((int)(30/(7.5*FireRateAsPercent / 100))) + 
			//	", or " + (30/(7.5*FireRateAsPercent / 100)));
			//Debug.Log("Acheived via (30/(7.5*(" + FireRateAsPercent + "/100)))" );
			//Debug.Log("Acheived via " + 30/(7.5*FireRateAsPercent/100));
			AnimClock = (int)(30/(7.5 * FireRateAsPercent / 100));
			ShotDelay = (int)(30/(7.5 * FireRateAsPercent / 100));
			
			isFiring = true;
			
			if( Physics.Raycast( ray, out hit, 100 ) ){
				//PART OF BULLET HOLES			var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				
				if (hit.transform.tag == "Explosive") {
					if ((Detonator)hit.transform.gameObject.GetComponent("Detonator") != null) {
						Detonator target = (Detonator)hit.transform.gameObject.GetComponent("Detonator");
						target.Explode();
					}
					if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
						AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
						targetSound.Play();
					}
				} else if (hit.transform.tag == "Enemy") {
					if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
						AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
						targetSound.Play();
					}
					if (hit.transform.gameObject.GetComponent("EnemyHealth") != null) {
						EnemyHealth enemyHealth = (EnemyHealth)hit.transform.gameObject.GetComponent("EnemyHealth");
						enemyHealth.Health -= Damage;
						MonoBehaviour.print("Dealt " + Damage.ToString() + " Damage to " + hit.transform.gameObject.name);
					}
					Modify: GameObject newBlood = (GameObject)MonoBehaviour.Instantiate(BloodSpray, hit.point, hitRotation);
					Modify: newBlood.transform.parent = hit.transform;
					Modify: newBlood.transform.Translate(0,(float)0.05,0);
				} else {
					GameObject newBulletHole = (GameObject)MonoBehaviour.Instantiate(BulletHole, hit.point, hitRotation);
					newBulletHole.transform.parent = hit.transform;
					newBulletHole.transform.Translate(0,(float)0.05,0);
					hitRotation.x = hitRotation.x + 270;
					GameObject newDust = (GameObject)MonoBehaviour.Instantiate(DirtSpray, hit.point, hitRotation);
					newDust.transform.parent = hit.transform;
					newDust.transform.Translate(0,(float)0.05,0);
				}
			}
			return true;
		}
		return false;
	}

	public void Drop() {
		if (!IsValid) {
			return;
		}
		GameObject pickup = (GameObject)MonoBehaviour.Instantiate(InstantiablePickup, mainObject.transform.position, mainObject.transform.rotation);
		pickup.AddComponent("WeaponPickup");
		pickup.AddComponent("Rigidbody");
		((WeaponPickup)pickup.GetComponent("WeaponPickup")).thisGun = this;
		MonoBehaviour.Destroy(mainObject);
		Debug.Log(this.ToString());
	}
	
	public void AnimIdentify() {
		Hammer = GameObject.Find(mainObject.name + "/" + Path + "Hammer").transform;
		Slide = GameObject.Find(mainObject.name + "/" + Path + "Slide").transform;
		Trigger = GameObject.Find(mainObject.name + "/" + Path + "Trigger").transform;
		/* 						DEBUG
		if (Hammer != null) {
			Debug.Log("Found Hammer for " + WeaponName + " at default location.");
		} else {
			Debug.Log("Did not find Hammer for " + WeaponName + " at default location.");
		}
		if (Slide != null) {
			Debug.Log("Found slide for " + WeaponName + " at default location.");
		} else {
			Debug.Log("Did not find Slide for " + WeaponName + " at default location.");
		}
		if (Trigger != null) {
			Debug.Log("Found Trigger for " + WeaponName + " at default location.");
		} else {
			Debug.Log("Did not find Trigger for " + WeaponName + " at default location.");
		}*/
	}
	
	void findFlash() {
		flash = GameObject.Find(mainObject.transform.name + "/" + Path + "Flash");
		//if (flash != null) {
		//	Debug.Log("Found flash for " + WeaponName + " at " + mainObject.transform.name +  "/" + Path + flash.name);
		//	flash.transform.localScale = new Vector3 (0,0,0);
		//} else {
		//	Debug.Log("Didn't find flash for " + WeaponName + " at " + mainObject.transform.name + "/" + Path + "Flash");
		//}
	}
	
	public void AnimUpdate() {
		if (isFiring) {
			//Debug.Log("AnimClock Reads " + AnimClock.ToString());
			//Debug.Log("ShotDelay Reads " + ShotDelay.ToString());
			if (!Exists){
				return;
			}
			//Debug.Log("Animating " + mainObject.name);
			
			if (Slide == null ||  Hammer == null || Trigger == null) {
				AnimIdentify();
			}
			if (flash == null) {
				findFlash();
			}
			if (AnimClock == ShotDelay){
				Hammer.Rotate(-HammerRotation, 0, 0);
				mainObject.transform.Rotate(-gunAngle,0,0);
				mainObject.transform.Translate(0,0,-gunDistance);
				Slide.Translate(0,0, SlideDistance);
				Trigger.Translate(0,0, TriggerDistance);
				flash.transform.localScale = new Vector3 (10,10,10);
				((MouseLook)mainObject.transform.parent.gameObject.GetComponent("MouseLook")).rotationY
					+= CameraClimb;
				float x = mainObject.transform.parent.parent.localEulerAngles.x;
				float y = mainObject.transform.parent.parent.localEulerAngles.y;
				float z = mainObject.transform.parent.parent.localEulerAngles.z;
				
				mainObject.transform.parent.parent.localEulerAngles = new Vector3(x,y+Random.Range(-maxCameraSway, maxCameraSway),z);
				//+= Random.Range(maxCameraSway, -maxCameraSway)
			}
			if (AnimClock == (ShotDelay - 1)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance*0.25f);
				flash.transform.localScale = new Vector3 (0,0,0);
				((MouseLook)mainObject.transform.parent.gameObject.GetComponent("MouseLook")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 2)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance*0.25f);
				((MouseLook)mainObject.transform.parent.gameObject.GetComponent("MouseLook")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 3)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance*0.25f);
				((MouseLook)mainObject.transform.parent.gameObject.GetComponent("MouseLook")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 4)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance*0.25f);
				Trigger.Translate(0,0, -TriggerDistance);
			}		
			if (AnimClock == (ShotDelay-SlideDelay)){
				Hammer.Rotate(HammerRotation, 0, 0);
				Slide.Translate(0,0, -SlideDistance);
			}
			if (AnimClock == 0) {
				isFiring = false;
			}
			if (AnimClock > 0){
				AnimClock--;
			}
		}
	}
}