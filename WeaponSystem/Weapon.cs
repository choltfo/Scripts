using UnityEngine;
using System.Collections;

[System.Serializable]

/// <summary>
/// Represents a weapon that can be fired by <see cref="Shootobjects"/>.
/// </summary>
public class Weapon {
	
	public int UID;
	
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
	/// The scope zoom.
	/// Overwritten by 
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
	/// The fire rate as a percentage. The maximum (100%) should be one shot per physics frame.
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
	public string path = ".../";
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
	
	public weaponAnimType curAnim = weaponAnimType.None;
	
	[HideInInspector]
	/// <summary>
	/// The Slide transform. Not visible in Inspector.
	/// </summary>
	public Transform Slide;
	/// <summary>
	/// The slide's delay before moving backwards.
	/// </summary>
	public int SlideDelay;
	[HideInInspector]
	/// <summary>
	/// The delay, in updates, between shots. Not visible in Inspector.
	/// </summary>
	public int ShotDelay;
	
	/// <summary>
	/// How far away an enemy can hear this gunshot.
	/// </summary>
	public float detectionDistance;
	
	float actualDetectionDistance;
	
	public HardPoint[] attachments;
	float lastAim;
	float lastHoldToggle;

	public bool animate = true;
	public bool isOut = false;
	
	
	public GameObject SmokePuff;
	public Vector3 SmokePuffPosition;
	
	public bool player = true;
	
	public static int playerWeaponLayer = 9;
	
	public AnimationCurve AnimSlideTX;
	public AnimationCurve AnimSlideTY;
	public AnimationCurve AnimSlideTZ;
	public AnimationCurve AnimWeaponTX;
	public AnimationCurve AnimWeaponTY;
	public AnimationCurve AnimWeaponTZ;
	
	public AnimationCurve AnimSlideRX;
	public AnimationCurve AnimSlideRY;
	public AnimationCurve AnimSlideRZ;
	public AnimationCurve AnimWeaponRX;
	public AnimationCurve AnimWeaponRY;
	public AnimationCurve AnimWeaponRZ;
	
	public AnimationCurve AnimVerticalRecoil;
	
	float lastShot = 0f;
	public float shotDelay;
	
	public float reloadTime = 5f;
	
	public bool animateSlide;

	public float maxRecoilSway = 2;

	float lastReloadStart;
	
	//public UnderbarrelAttachment underbarrel;
	
	//TODO Add reloading animations.
	
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
	public virtual void create(GameObject Player, bool isPlayerWeapon){
		camera = Player;
		this.player = isPlayerWeapon;
		GameObject Gun = (GameObject)MonoBehaviour.Instantiate(InstantiableObject, new Vector3 (0,0,0), Player.transform.rotation);
		Gun.transform.parent = Player.transform;
		Gun.transform.localPosition = Position;
		mainObject = Gun;
		if (mainObject.transform.FindChild(path + "Flash") != null) {
			flash = mainObject.transform.FindChild(path + "Flash").gameObject;
			flash.SetActive(false);
		}
		if (this.player) {
			foreach (HardPoint hp in attachments) {
				hp.attachment.deploy(mainObject, hp.position);
				if (hp.attachment.type == AttachmentType.Silencer) {
				mainObject.audio.clip = hp.attachment.silencerNoise;
				}
				if(hp.attachment.type == AttachmentType.Scope) foldFrontSight();
			}
		}

		//MonoBehaviour.print("Added " + WeaponName);
		AnimIdentify();
		isAimed = false;
		lastAim = Time.time;
		Exists = true;
		
		// Attachment specific methods.
		bool ZoomChanged = false;
		actualDetectionDistance = detectionDistance;
		if (this.player) {
			for (int i = 0; i < attachments.Length; i++) {
				if (attachments[i].attachment.type == AttachmentType.Silencer && attachments[i].attachment.isValid) {
					mainObject.GetComponent<AudioSource>().clip = attachments[i].attachment.silencerNoise;
					actualDetectionDistance -= Mathf.Abs (attachments[i].attachment.detectionReduction);
				}
				if (attachments[i].attachment.type == AttachmentType.Scope && attachments[i].attachment.isValid) {
					ScopeZoom = attachments[i].attachment.overrideZoom;
					ZoomChanged = true;
				}
			}
		}
		if (!ZoomChanged) {
			ScopeZoom = 60;
		}
		NormalZoom = 60;

		
		if (this.player) {
			setLayerOfChildren(mainObject.transform);
		}
	}
	
	void setLayerOfChildren (Transform go) {
		go.gameObject.layer = playerWeaponLayer;
		foreach (Transform t in go) {
			setLayerOfChildren (t);
		}
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
	
	/// <summary>
	/// generates a new BulletHit that can be used for calculating reflections and shots.
	/// </summary>
	/// <returns>
	/// The bullet hit generated.
	/// </returns>
	/// <param name='Hit'>
	/// The raycastHit to use.
	/// </param>
	/// <param name='shooter'>
	/// The Enemy that shot the gun.
	/// </param>
	public BulletHit generateHit(RaycastHit Hit, Enemy shooter) {
		BulletHit bh = new BulletHit();
		bh.BloodSpray = BloodSpray;
		bh.BulletHole = BulletHole;
		bh.caliber = ammoType;
		bh.Damage = Damage;
		bh.DirtSpray = DirtSpray;
		bh.hit = Hit;
		bh.HitStrength = HitStrength;
		bh.maxRange = Range;
		bh.origin = camera.transform.position;
		bh.shooter = shooter;
		return bh;
	}
	
	
	public virtual void withdraw() {
		curAnim = weaponAnimType.Withdrawing;
		isAimed = false;
		isOut = true;
		lastHoldToggle = Time.time;
	}
	
	public virtual void stow() {
		curAnim = weaponAnimType.Stowing;
		isAimed = false;
		isOut = false;
		lastHoldToggle = Time.time;
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
		isAimed = !isAimed;
	}
	
	/// <summary>
	/// Shoot this gun.
	/// </summary>
	/// <param name='camera'>
	/// Camera to aim from.
	/// </param>
	public virtual bool Shoot(Camera cCamera, Enemy shooter) {
		
		if (CurAmmo > 0/*TODO Modify:  && !vehicle.riding*/ && actionHasReset() && curAnim == weaponAnimType.None){
			CurAmmo -= 1;
			mainObject.GetComponent<AudioSource>().Play();
			PathfindingEnemy.hearNoise(shooter, actualDetectionDistance);
			//Debug.Log("Setting anim clock to " + ((int)(30/(7.5*FireRateAsPercent / 100))) + 
			//	", or " + (30/(7.5*FireRateAsPercent / 100)));
			//Debug.Log("Acheived via (30/(7.5*(" + FireRateAsPercent + "/100)))" );
			//Debug.Log("Acheived via " + 30/(7.5*FireRateAsPercent/100));
			
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
  				float x = position.x * xSpread;
  				float y = position.y * yspread;
								// This defines a point half a meter in front of the shooter, bypassing all sorts of detection issues.
				Ray ray = new Ray(camera.transform.TransformPoint(Vector3.forward * 0.26f),
					camera.transform.forward.RotateX(x*Mathf.Deg2Rad).RotateY(y*Mathf.Deg2Rad));
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Range)){
					generateHit(hit, shooter).calculateDamage();
				}
			}

			camera.transform.Rotate(0,Random.Range(-maxRecoilSway, maxRecoilSway),0);

			lastShot = Time.time;
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// Shoots the gun from an AI, instead of a player.
	/// </summary>
	/// <returns>
	/// Wether the gun shot.
	/// </returns>
	/// <param name='camera'>
	/// The GameObject that is situated in place of a camera.
	/// </param>
	public virtual bool AIShoot(GameObject camera, Enemy shooter) {
		//Debug.Log("Attempting firing");
		if (CurAmmo > 0 && actionHasReset () && curAnim == weaponAnimType.None){
			//Debug.Log("Firing by AI");
			CurAmmo -= 1;
			((AudioSource)mainObject.GetComponent<AudioSource>()).Play();
			
			//Debug.Log("Setting anim clock to " + ((int)(30/(7.5*FireRateAsPercent / 100))) + 
			//	", or " + (30/(7.5*FireRateAsPercent / 100)));
			//Debug.Log("Acheived via (30/(7.5*(" + FireRateAsPercent + "/100)))" );
			//Debug.Log("Acheived via " + 30/(7.5*FireRateAsPercent/100));
			isFiring = true;
			curAnim = weaponAnimType.Firing;
			
			
			if (SmokePuff) {
				GameObject Thing = (GameObject)MonoBehaviour.Instantiate(SmokePuff, new Vector3 (0,0,0), mainObject.transform.rotation);
				Thing.transform.parent = mainObject.transform;
				Thing.transform.localPosition = SmokePuffPosition;
				//Thing.transform.Rotate(rot, Space.Self);
			}
			PathfindingEnemy.hearNoise(shooter, actualDetectionDistance);
			for (int i = 0; i<numOfShots; i++) {
				/*
				Vector3 aim = camera.transform.forward;
				//aim.Set (aim.x+Random.Range(-xSpread/10,xSpread/10), aim.y+Random.Range(-yspread/10,yspread/10), 0);
				*/
				Vector2 position = Random.insideUnitCircle;
				float x = position.x * xSpread;
				float y = position.y * yspread;
				// This defines a point half a meter in front of the shooter, bypassing all sorts of detection issues.
				Ray ray = new Ray(camera.transform.TransformPoint(Vector3.forward * 0.26f),
				                  camera.transform.forward.RotateX(x*Mathf.Deg2Rad).RotateY(y*Mathf.Deg2Rad));
				
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Range)){
					generateHit(hit, shooter).calculateDamage();
				}
			}
			lastShot = Time.time;
			return true;
		}
		return false;
	}
	
	
	
	public virtual int[] Reload(int[] ammo) {
		if (CurAmmo < MaxAmmo && curAnim == weaponAnimType.None){
			if (ammo[(int)ammoType] != 0) {
				lastReloadStart = Time.time;
				curAnim = weaponAnimType.Reloading;
			
				//Debug.Log ("Out of bullets!");
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
		}
		return ammo; //~!~!~!~!~!~!~!
	}
	
	/// <summary>
	/// Instantiate the weapon pickup for this gun, and let it go.
	/// </summary>
	public virtual void Drop() {
		if (!IsValid) {
			Debug.Log("Attempting drop of invalid weapon.");
			return;
		}
		Debug.Log("Attempting drop of weapon.");
		GameObject pickup = (GameObject)MonoBehaviour.Instantiate(InstantiablePickup, mainObject.transform.position, mainObject.transform.rotation);
		pickup.SetActive(true);
		pickup.GetComponent<WeaponPickup>().thisGun = this.duplicate();
		destroy();
		IsValid = false;
	}
	
	/// <summary>
	/// Identify the animatable parts.
	/// </summary>
	public virtual void AnimIdentify() {
		if (animateSlide) {
			Slide = GameObject.Find(mainObject.name + "/" + path + "Slide").transform;
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
		flash = GameObject.Find(mainObject.transform.name + "/" + path + "Flash");
	}
	
	
	float gunKickbackTime;
	float gunReturnTime;
	float slideKickbackTime;
	float slideReturnTime;
	
	
	
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
			//mainObject.transform.localPosition = new Vector3(
			//	Mathf.Lerp(mainObject.transform.localPosition.x, StowedPosition.x, (Time.time - lastHoldToggle)/switchSpeed),
			//	Mathf.Lerp(mainObject.transform.localPosition.y, StowedPosition.y, (Time.time - lastHoldToggle)/switchSpeed),
			//	Mathf.Lerp(mainObject.transform.localPosition.z, StowedPosition.z, (Time.time - lastHoldToggle)/switchSpeed));
			
			mainObject.transform.localPosition =
				//Vector3.Lerp(mainObject.transform.localPosition, StowedPosition, (Time.time - lastHoldToggle)/switchSpeed);
				Vector3.Lerp(mainObject.transform.localPosition, StowedPosition, (Time.time - lastHoldToggle)/switchSpeed);
				
			mainObject.transform.localEulerAngles = 
				Vector3.Slerp(new Vector3(0,0,0), mainObject.transform.localEulerAngles, (Time.time - lastHoldToggle)/switchSpeed);
			
			if (mainObject.transform.localPosition.Equals(StowedPosition)) {
				curAnim = weaponAnimType.None;
				Debug.Log ("Done moving to Stow position, setting curAnim to 'None'");
			}
			break;
			
		case weaponAnimType.Withdrawing :
			mainObject.transform.parent = camera.transform;
			//mainObject.transform.localPosition = new Vector3(
			//	Mathf.Lerp(mainObject.transform.localPosition.x, Position.x, (Time.time - lastHoldToggle)/switchSpeed),
			//	Mathf.Lerp(mainObject.transform.localPosition.y, Position.y, (Time.time - lastHoldToggle)/switchSpeed),
			//	Mathf.Lerp(mainObject.transform.localPosition.z, Position.z, (Time.time - lastHoldToggle)/switchSpeed));
			
			//mainObject.transform.localEulerAngles = new Vector3(
				//Mathf.Lerp(mainObject.transform.localEulerAngles.x, 0, (Time.time - lastHoldToggle)/switchSpeed),
				//Mathf.Lerp(mainObject.transform.localEulerAngles.y, 0, (Time.time - lastHoldToggle)/switchSpeed),
				//Mathf.Lerp(mainObject.transform.localEulerAngles.z, 0, (Time.time - lastHoldToggle)/switchSpeed));
			mainObject.transform.localEulerAngles = 
				Vector3.Slerp(mainObject.transform.localEulerAngles, new Vector3(0,0,0), (Time.time - lastHoldToggle)/switchSpeed);
			
			mainObject.transform.localPosition =
				Vector3.Lerp(mainObject.transform.localPosition, Position, (Time.time - lastHoldToggle)/switchSpeed);
			
			if (mainObject.transform.localPosition.Equals(Position)) {
				curAnim = weaponAnimType.None;
				Debug.Log ("Done moving to hold position, setting curAnim to 'None'");
			}
			break;
			
		case weaponAnimType.None :
			if (Slide != null) Slide.localPosition.Set(AnimSlideTX.Evaluate(0),
				AnimSlideTY.Evaluate(0), AnimSlideTZ.Evaluate(0));
			
			if (isOut) {
				if(isAimed == true){
					if (player) camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView,
						ScopeZoom,Time.deltaTime*zoomSmoothing);
					mainObject.transform.localPosition = new Vector3(
						Mathf.Lerp(mainObject.transform.localPosition.x, ScopedPosition.x, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.y, ScopedPosition.y, (Time.time-lastAim)*AimSpeed),
						Mathf.Lerp(mainObject.transform.localPosition.z, ScopedPosition.z, (Time.time-lastAim)*AimSpeed));
				} else {
					if (player) camera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camera.GetComponent<Camera>().fieldOfView,
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
			
			if (player) {
				
				if (Slide == null) {
					AnimIdentify();
					if (Slide == null) {
						animateSlide = false;
					} else animateSlide = true;
				} else animateSlide = true;
				
				if (animateSlide) Slide.localPosition = new Vector3 (AnimSlideTX.Evaluate(Time.time - lastShot),
					AnimSlideTY.Evaluate(Time.time - lastShot), AnimSlideTZ.Evaluate(Time.time - lastShot));
				
				mainObject.transform.localPosition = new Vector3 (AnimWeaponTX.Evaluate(Time.time - lastShot),
					AnimWeaponTY.Evaluate(Time.time - lastShot), AnimWeaponTZ.Evaluate(Time.time - lastShot)) +
					(isAimed ? ScopedPosition : Position);
				
				mainObject.transform.localEulerAngles = new Vector3 (AnimWeaponRX.Evaluate(Time.time - lastShot),
					AnimWeaponRY.Evaluate(Time.time - lastShot), AnimWeaponRZ.Evaluate(Time.time - lastShot));
				
				if (player) {
					mainObject.transform.parent.gameObject.GetComponent<MouseLookModded>().rotationY +=
						(AnimVerticalRecoil.Evaluate(Time.time - lastShot) -
					AnimVerticalRecoil.Evaluate(Time.time - lastShot - Time.deltaTime));
				} else {
					mainObject.transform.parent.Rotate(AnimVerticalRecoil.Evaluate(Time.time - lastShot) -
					AnimVerticalRecoil.Evaluate(Time.time - lastShot - Time.deltaTime),0,0);
				}
			}
			
			
			if (actionHasReset()) {
				curAnim = weaponAnimType.None;
			}
			break;
			
		case weaponAnimType.Reloading :
			if (lastReloadStart + reloadTime < Time.time){
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
		Transform sightPost = mainObject.transform.FindChild(path + "FrontSight");
		if (sightPost) {
			sightPost.Rotate(90,0,0);
		}
	}

	public Weapon duplicate() {
		return (Weapon)this.MemberwiseClone();
	}
	
	/// <summary>
	/// Has the weapon's action reset alredy?
	/// </summary>
	/// <returns>
	/// Whether the action has reset.
	/// </returns>
	public bool actionHasReset () {
		return ((lastShot + shotDelay) - Time.time) < 0;
	}
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
