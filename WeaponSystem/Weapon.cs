using UnityEngine;
using System.Collections;

[System.Serializable]

/// <summary>
/// Represents a weapon that can be fired by <see cref="Shootobjects"/>.
/// </summary>
public class Weapon {
	/// <summary>
	/// The prefab that should
	/// Be created when this gun
	/// Is activated.
	/// </summary>
	public GameObject InstantiableObject;
	/// <summary>
	/// The prefab that should
	/// Be instantiated when
	/// This gun is released.
	/// </summary>
	public GameObject InstantiablePickup;
	public float price;
	/// <summary>
	/// The bullet hole that is placed when you shoot something.
	/// </summary>
	public GameObject BulletHole;
	/// <summary>
	/// The dirt spray that is placed when you shoot something.
	/// </summary>
	public GameObject DirtSpray;
	/// <summary>
	/// The blood spray that is placed when you shoot something.
	/// </summary>
	public GameObject BloodSpray;
	/// <summary>
	/// The position, relative to the camera,
	/// that the gun is held at when not scoping.
	/// </summary>
	public Vector3 Position;
	/// <summary>
	/// The position, relative to the camera,
	/// that the gun should be held at when scoping.
	/// </summary>
	public Vector3 ScopedPosition;
	public Vector3 StowedPosition;
	public Vector3 StowedRotationAsEulerAngles;
	public float switchSpeed = 1f;
	public float AimSpeed = 10f;
	/// <summary>
	/// Whether or not the player can look down the irons on this gun.
	/// </summary>
	public bool CanScope;
	/// <summary>
	/// The scope zoom.
	/// </summary>
	public float ScopeZoom;
	/// <summary>
	/// The zoom smoothing.
	/// </summary>
	public float NormalZoom;
	/// <summary>
	/// The amount, in degrees, to (in/de)crease the the FOV per update when (un)scoping 
	/// </summary>
	public float zoomSmoothing;
	/// <summary>
	/// The damage per bullet imparted to enemies when shot.
	/// </summary>
	public int Damage;
	/// <summary>
	/// The strength of the force applied to objects shot with this gun.
	/// </summary>
	public float HitStrength = 50;
	/// <summary>
	/// The fire rate as a percentage. The maximum (100%) is 7.5 shots per second.
	/// </summary>
	public int FireRateAsPercent;
	[HideInInspector]
	public double fireRate;
	/// <summary>
	/// The type of bullet fired.
	/// </summary>
	public AmmoType ammoType;
	/// <summary>
	/// The maximum number of bullets in a clip of this gun.
	/// </summary>
	public int MaxAmmo;
	/// <summary>
	/// The number of bullets in the clip of this gun.
	/// </summary>
	public int CurAmmo;
	/// <summary>
	/// The name of the weapon.
	/// </summary>
	public string WeaponName;
	/// <summary>
	/// The display name of the weapon (Because SIG P220 doesn't roll off the tongue).
	/// </summary>
	public string DisplayName;
	/// <summary>
	/// The path to the animated objects, e.g muzzle flare and slide.
	/// </summary>
	public string Path = ".../";
	//[HideInInspector]
	/// <summary>
	/// Whether or not the gun is valid. Not visible in Inspector.
	/// </summary>
	public bool IsValid = false;
	/// <summary>
	/// Whether or not the gun is automatic (pewpewpew!) or semi automatic (pew! pew!).
	/// </summary>
	public bool Automatic;
	/// <summary>
	/// The distance this weapon can fire, and hit something.
	/// </summary>
	public float Range = 300f;
	/// <summary>
	/// The number of shots fired per shot. For shotguns, or something.
	/// </summary>
	public int numOfShots = 200;
	/// <summary>
	/// The x spread on rounds.
	/// </summary>
	public float xSpread = 15;
	/// <summary>
	/// The y spread on rounds.
	/// </summary>
	public float yspread = 15;
	
	
	[HideInInspector]
	/// <summary>
	/// The camera gameobject.
	/// </summary>
	public GameObject camera;
	[HideInInspector]
	/// <summary>
	/// Whether the gun is aimed.
	/// </summary>
	public bool isAimed = false;
	/// <summary>
	/// Whether the gun exists.
	/// </summary>
	public bool Exists = false;
	[HideInInspector]
	/// <summary>
	/// Whether the gun is firing. Not visible in Inspector.
	/// </summary>
	public bool isFiring = false;
	[HideInInspector]
	/// <summary>
	/// The main object of the gun. Not visible in Inspector.
	/// </summary>
	public GameObject mainObject = null;
	[HideInInspector]
	/// <summary>
	/// The flash gameobject. Not visible in Inspector.
	/// </summary>
	public GameObject flash = null;
	
	public bool flashLightOn = false;
	public GameObject flashLight = null;
	[HideInInspector]
	/// <summary>
	/// The animation clock. Not visible in Inspector.
	/// </summary>
	public int AnimClock = 0;
	public weaponAnimType curAnim = weaponAnimType.None;
	
	[HideInInspector]
	/// <summary>
	/// The hammer transform. Not visible in Inspector.
	/// </summary>
	public Transform Hammer;
	[HideInInspector]
	/// <summary>
	/// The Slide transform. Not visible in Inspector.
	/// </summary>
	public Transform Slide;
	[HideInInspector]
	/// <summary>
	/// The Trigger transform. Not visible in Inspector.
	/// </summary>
	public Transform Trigger;
	/// <summary>
	/// The angle, in degrees, that the camera moves up after each shot.
	/// </summary>
	public float CameraClimb = 2f;
	/// <summary>
	/// The angle, in degrees, that the camera moves left
	/// and right after each shot.
	/// </summary>
	public float maxCameraSway = 2f;
	/// <summary>
	/// The angle, in degrees, that the gun pitches up.
	/// Still slightly broken.
	/// </summary>
	public float gunAngle = 5f;
	/// <summary>
	/// The distance the gun moves backwards.
	/// </summary>
	public float gunDistance = 0.05f;
	/// <summary>
	/// The hammer rotation, in degrees.
	/// </summary>
	public float HammerRotation = 2f;
	/// <summary>
	/// The slide kickback distance.
	/// </summary>
	public float SlideDistance = 0.05f;
	/// <summary>
	/// The trigger slide distance.
	/// </summary>
	public float TriggerDistance = 0.05f;
	/// <summary>
	/// The slide's delay before moving backwards.
	/// </summary>
	public int SlideDelay;
	[HideInInspector]
	/// <summary>
	/// The delay, in updates, between shots. Not visible in Inspector.
	/// </summary>
	public int ShotDelay;
	
	public HardPoint[] attachments;
	float lastAim;

	public bool animate = true;
	public bool isOut = false;
	
	
	public GameObject SmokePuff;
	public Vector3 SmokePuffPosition;
	
	
	//public UnderbarrelAttachment underbarrel;
	
	//TODO Add reloading animations.
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Weapon"/> class with the specified attrtibutes.
	/// </summary>
	/// <param name='MainObject'>
	/// Main object to instantiate.
	/// </param>
	/// <param name='Position'>
	/// Position when holding at hip.
	/// </param>
	/// <param name='ScopedPosition'>
	/// Scoped position.
	/// </param>
	/// <param name='CanScope'>
	/// Can scope?
	/// </param>
	/// <param name='MaxAmmo'>
	/// Max ammo in clip.
	/// </param>
	/// <param name='WeaponName'>
	/// Weapon name.
	/// </param>
	/// <param name='Damage'>
	/// Damage.
	/// </param>
	/// <param name='Path'>
	/// Path to animated components.
	/// </param>
	/// <param name='FireRate'>
	/// Fire rate in rounds per 30 updates.
	/// </param>
	/// <param name='Automatic'>
	/// Automatic?
	/// </param>
	public Weapon(GameObject MainObject, Vector3 Position, Vector3 ScopedPosition,
		bool CanScope, int MaxAmmo, string WeaponName, int Damage, string Path, float FireRate, bool Automatic) {
		
		this.InstantiableObject = MainObject;
		this.Position           = Position;
		this.ScopedPosition     = ScopedPosition;
		this.CanScope           = CanScope;
		this.MaxAmmo            = MaxAmmo;
		CurAmmo                 = MaxAmmo;
		this.WeaponName         = WeaponName;
		this.IsValid            = true;
		this.Damage             = Damage;
		this.Path               = Path;
		this.fireRate           = FireRate;
		this.Automatic          = Automatic;
	}
	
	/// <summary>
	/// Initializes a blank instance of the <see cref="Weapon"/> class.
	/// </summary>
	public Weapon() {
		this.IsValid = false;
	}
	
	
	
	
	/// <summary>
	/// Instantiates the gun in front of the player (camera), so that is can be usefully animated.
	/// </summary>
	/// <param name='player'>
	/// The camera gameobject to put the gun in front of.
	/// </param>
	public virtual void create(GameObject player){
		camera = player;
		GameObject Gun = (GameObject)MonoBehaviour.Instantiate(InstantiableObject, new Vector3 (0,0,0), player.transform.rotation);
		Gun.transform.parent = player.transform;
		Gun.transform.localPosition = Position;
		mainObject = Gun;
		if (mainObject.transform.FindChild(Path + "Flash") != null) {
			flash = mainObject.transform.FindChild(Path + "Flash").gameObject;
			flash.SetActive(false);
		}
		
		foreach (HardPoint hp in attachments) {
			hp.attachment.deploy(mainObject, hp.position);
			if(hp.attachment.type == AttachmentType.Scope) foldFrontSight();
		}

		//MonoBehaviour.print("Added " + WeaponName);
		AnimIdentify();
		isAimed = false;
		lastAim = Time.time;
		Exists = true;
	}
	
	/// <summary>
	/// Deactivate this instance.
	/// </summary>
	public virtual void destroy() {
		if (mainObject != null) {
			MonoBehaviour.Destroy(mainObject);
			//MonoBehaviour.print("Destroyed " + WeaponName);
			Exists = false;
		}
	}
	
	
	
	public virtual void withdraw() {
		curAnim = weaponAnimType.Withdrawing;
		isAimed = false;
		isOut = true;
	}
	
	public virtual void stow() {
		curAnim = weaponAnimType.Stowing;
		isAimed = false;
		isOut = false;
	}
	
	public virtual void toggleStowage() {
		isAimed = false;
		if (isOut) {
			stow ();
		} else {
			withdraw ();
		}
	}
	
	
	/// <summary>
	/// Aim this instance.
	/// </summary>
	public virtual void aim() {
		if (!IsValid || !Exists) {
			return;
		}
		lastAim = Time.time;
		if (isAimed) {
			//mainObject.transform.localPosition = Position;
			isAimed = false;
		} else {
			//mainObject.transform.localPosition = ScopedPosition;
			isAimed = true;
		}
	}
	
	public virtual bool ToggleFlashLight (){
		if (flashLight == null || !IsValid || !Exists) {
			return false;
		}
		flashLightOn = !flashLightOn;
		flashLight.SetActive(flashLightOn);
		return true;
	}
	
	/// <summary>
	/// Shoot this gun.
	/// </summary>
	/// <param name='camera'>
	/// Camera to aim from.
	/// </param>
	public virtual bool Shoot(Camera camera) {
		if (CurAmmo > 0/*TODO Modify:  && !vehicle.riding*/ && !isFiring && curAnim == weaponAnimType.None){
			CurAmmo -= 1;
			((AudioSource)mainObject.GetComponent("AudioSource")).Play();
			
			//Debug.Log("Setting anim clock to " + ((int)(30/(7.5*FireRateAsPercent / 100))) + 
			//	", or " + (30/(7.5*FireRateAsPercent / 100)));
			//Debug.Log("Acheived via (30/(7.5*(" + FireRateAsPercent + "/100)))" );
			//Debug.Log("Acheived via " + 30/(7.5*FireRateAsPercent/100));
			
			AnimClock = (int)(30/(7.5 * FireRateAsPercent / 100));
			ShotDelay = (int)(30/(7.5 * FireRateAsPercent / 100));
			isFiring = true;
			curAnim = weaponAnimType.Firing;
			
			if (SmokePuff) {
				GameObject Thing = (GameObject)MonoBehaviour.Instantiate(SmokePuff, new Vector3 (0,0,0), mainObject.transform.rotation);
				Thing.transform.parent = mainObject.transform;
				Thing.transform.localPosition = SmokePuffPosition;
				//Thing.transform.Rotate(rot, Space.Self);
			}
			
			for (int i = 0; i<numOfShots; i++) {
				Vector2 position = Random.insideUnitCircle;
  				int x = (int)(position.x * xSpread);
  				int y = (int)(position.y * yspread);
				Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2+x,Screen.height/2+y,0));
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Range)){
					Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);	
					
					if (hit.transform.gameObject.GetComponent<Rigidbody>() != null) {
						hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(hit.normal * -HitStrength);
					}					 
					if (hit.transform.tag == "Explosive") {
						if ((Detonator)hit.transform.gameObject.GetComponent("Detonator") != null) {
							Detonator target = (Detonator)hit.transform.gameObject.GetComponent("Detonator");
							target.Explode();
						}
						if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
							AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
							targetSound.Play();
						}
						if (hit.transform.gameObject.GetComponent<ExplosiveDamage>() != null) {
							hit.transform.gameObject.GetComponent<ExplosiveDamage>().explode();	
						}
					} else if (hit.transform.tag == "Enemy") {
						if (hit.transform.gameObject.GetComponent("AudioSource") != null) {
							AudioSource targetSound = (AudioSource)hit.transform.gameObject.GetComponent("AudioSource");
							targetSound.Play();
						}
						if (hit.transform.gameObject.GetComponent("EnemyHealth") != null) {
							EnemyHealth enemyHealth = (EnemyHealth)hit.transform.gameObject.GetComponent("EnemyHealth");
							enemyHealth.damage(Damage, DamageCause.Shot);
							MonoBehaviour.print("Dealt " + Damage.ToString() + " Damage to " + hit.transform.gameObject.name);
						}
						GameObject newBlood = (GameObject)MonoBehaviour.Instantiate(BloodSpray, hit.point, hitRotation);
						newBlood.transform.parent = hit.transform;
						newBlood.transform.Translate(0,(float)0.05,0);
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
			}
			return true;
		}
		return false;
	}
	
	public virtual int[] Reload(int[] ammo) {
		if (CurAmmo < MaxAmmo && !isFiring && curAnim == weaponAnimType.None){
			AnimClock = 15;
			curAnim = weaponAnimType.Reloading;
			
			if (ammo[(int)ammoType] == 0) {
				Debug.Log ("Out of bullets!");
			}
			ammo[(int)ammoType] += CurAmmo;
			CurAmmo = 0;
			if (ammo[(int)ammoType] >= MaxAmmo) {
				CurAmmo = MaxAmmo;
				ammo[(int)ammoType] -= MaxAmmo;
			} else {
				CurAmmo = ammo[(int)ammoType];
				ammo[(int)ammoType] = 0;
			}
		}
		return ammo; //~!~!~!~!~!~!~!
	}
	
	/// <summary>
	/// Instantiate the weapon pickup for this gun, and let it go.
	/// </summary>
	public virtual void Drop() {
		if (!IsValid) {
			return;
		}
		GameObject pickup = (GameObject)MonoBehaviour.Instantiate(InstantiablePickup, mainObject.transform.position, mainObject.transform.rotation);
		pickup.SetActive(true);
		//((WeaponPickup)pickup.GetComponent("WeaponPickup")).thisGun = this;
		destroy();
		IsValid = false;
	}
	
	/// <summary>
	/// Identify the animatable parts.
	/// </summary>
	public virtual void AnimIdentify() {
		if (animate) {
			Hammer = GameObject.Find(mainObject.name + "/" + Path + "Hammer").transform;
			Slide = GameObject.Find(mainObject.name + "/" + Path + "Slide").transform;
			Trigger = GameObject.Find(mainObject.name + "/" + Path + "Trigger").transform;
		}
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
	
	/// <summary>
	/// Finds the flash.
	/// </summary>
	void findFlash() {
		flash = GameObject.Find(mainObject.transform.name + "/" + Path + "Flash");
	}
	
	/// <summary>
	/// Update animations.
	/// </summary>
	public virtual void AnimUpdate() {
		if (!Exists){
			return;
		}
		

		
		switch (curAnim) {
		case weaponAnimType.Stowing :
			mainObject.transform.parent = camera.transform.parent;
			mainObject.transform.localPosition = new Vector3(
				Mathf.Lerp(mainObject.transform.localPosition.x, StowedPosition.x, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localPosition.y, StowedPosition.y, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localPosition.z, StowedPosition.z, (Time.deltaTime)*switchSpeed));
			
			mainObject.transform.localEulerAngles = new Vector3(
				Mathf.Lerp(mainObject.transform.localEulerAngles.x, StowedRotationAsEulerAngles.x, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localEulerAngles.y, StowedRotationAsEulerAngles.y, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localEulerAngles.z, StowedRotationAsEulerAngles.z, (Time.deltaTime)*switchSpeed));
			
			if (mainObject.transform.localPosition.Equals(StowedPosition)) curAnim = weaponAnimType.None;
			break;
		case weaponAnimType.Withdrawing :
			mainObject.transform.parent = camera.transform;
			mainObject.transform.localPosition = new Vector3(
				Mathf.Lerp(mainObject.transform.localPosition.x, Position.x, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localPosition.y, Position.y, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localPosition.z, Position.z, (Time.deltaTime)*switchSpeed));
			
			mainObject.transform.localEulerAngles = new Vector3(
				Mathf.Lerp(mainObject.transform.localEulerAngles.x, 0, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localEulerAngles.y, 0, (Time.deltaTime)*switchSpeed),
				Mathf.Lerp(mainObject.transform.localEulerAngles.z, 0, (Time.deltaTime)*switchSpeed));
			
			
			if (mainObject.transform.localPosition.Equals(Position)) curAnim = weaponAnimType.None;
			break;
		case weaponAnimType.None :
			if (isOut) {
				if(isAimed == true){
					camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView,
						ScopeZoom,Time.deltaTime*zoomSmoothing);
					mainObject.transform.localPosition = new Vector3(
						Mathf.Lerp(mainObject.transform.localPosition.x, ScopedPosition.x, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.y, ScopedPosition.y, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.z, ScopedPosition.z, (Time.time-lastAim)*AimSpeed));
				} else {
					camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView,
						NormalZoom,Time.deltaTime*zoomSmoothing);
					mainObject.transform.localPosition = new Vector3(
						Mathf.Lerp(mainObject.transform.localPosition.x, Position.x, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.y, Position.y, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.z, Position.z, (Time.time-lastAim)*AimSpeed));
				}
				mainObject.transform.parent = camera.transform;
				mainObject.transform.localEulerAngles = new Vector3 (0,0,0);
			} else {
				mainObject.transform.parent = camera.transform.parent;
				mainObject.transform.localPosition = StowedPosition;	
			}
			break;
		case weaponAnimType.Firing :
			//Debug.Log("AnimClock Reads " + AnimClock.ToString());
			//Debug.Log("ShotDelay Reads " + ShotDelay.ToString());
			//Debug.Log("Animating " + mainObject.name);
			
			if (Slide == null ||  Hammer == null || Trigger == null) {
				AnimIdentify();
			}
			if (flash == null) {
				findFlash();
			}
			if (AnimClock == ShotDelay){
				Hammer.Rotate(-HammerRotation, 0, 0);
				
				mainObject.transform.Translate(0,0,-gunDistance/4);
				mainObject.transform.Rotate(-gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,-gunDistance/4);
				mainObject.transform.Rotate(-gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,-gunDistance/4);
				mainObject.transform.Rotate(-gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,-gunDistance/4);
				mainObject.transform.Rotate(-gunAngle*0.25f,0,0);
				
				Slide.Translate(0,0, SlideDistance);
				Trigger.Translate(0,0, TriggerDistance);
				//flash.transform.localScale = new Vector3 (10,10,10);
				if (flash) {
					flash.SetActive(true);
				}
				((MouseLookModded)mainObject.transform.parent.gameObject.GetComponent("MouseLookModded")).rotationY
					+= CameraClimb;
				float x = mainObject.transform.parent.parent.localEulerAngles.x;
				float y = mainObject.transform.parent.parent.localEulerAngles.y;
				float z = mainObject.transform.parent.parent.localEulerAngles.z;
				
				mainObject.transform.parent.parent.localEulerAngles = new Vector3(x,y+Random.Range(-maxCameraSway, maxCameraSway),z);
				//+= Random.Range(maxCameraSway, -maxCameraSway)
			}
			if (AnimClock == (ShotDelay - 1)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance/4);
				
				//flash.transform.localScale = new Vector3 (0,0,0);
				if (flash) {
					flash.SetActive(false);
				}
				((MouseLookModded)mainObject.transform.parent.gameObject.GetComponent("MouseLookModded")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 2)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance/4);
				
				((MouseLookModded)mainObject.transform.parent.gameObject.GetComponent("MouseLookModded")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 3)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance/4);
				
				((MouseLookModded)mainObject.transform.parent.gameObject.GetComponent("MouseLookModded")).rotationY 
					-= CameraClimb/4;
			}
			if (AnimClock == (ShotDelay - 4)){
				mainObject.transform.Rotate(gunAngle*0.25f,0,0);
				mainObject.transform.Translate(0,0,gunDistance/4);
				
				Trigger.Translate(0,0, -TriggerDistance);
			}		
			if (AnimClock == (ShotDelay-SlideDelay)){
				Hammer.Rotate(HammerRotation, 0, 0);
				Slide.Translate(0,0, -SlideDistance);
			}
			if (AnimClock == 0) {
				isFiring = false;
				curAnim = weaponAnimType.None;
			}
			if (AnimClock > 0){
				AnimClock--;
			}
			break;
		case weaponAnimType.Reloading :
			if (AnimClock > 0){
				AnimClock--;
			}
			if (AnimClock == 0) {
				isFiring = false;
				curAnim = weaponAnimType.None;
			}
			break;
		default :
			Debug.LogError("INVALID ANIMATIONTYPE? HOW THE F***?");
			break;
		}
	}
	
	public void foldFrontSight() {
		Transform sightPost = mainObject.transform.FindChild(Path + "FrontSight");
		if (sightPost) {
			sightPost.Rotate(90,0,0);
		}
	}
}

[System.Serializable]
public class HardPoint {
	public string name;
	public ConnectionType connectionType = ConnectionType.Picitanny;
	public WeaponAttachment attachment;
	public Vector3 position;
}

public enum ConnectionType {
	Picitanny,
	Weaver,
	BarrelTip
}

/// <summary>
/// Weapon animation type.
/// </summary>
public enum weaponAnimType {
	None,
	Firing,
	Reloading,
	Withdrawing,
	Stowing
}

/// <summary>
/// Preset places to put the gun when stowed.
/// </summary>
public enum stowPointPresets {
	Custom,
	Rifle,
	Pistol
}