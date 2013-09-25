using UnityEngine;
using System.Collections;


[System.Serializable]
public class WeaponAsset : ScriptableObject {

	public GameObject InstantiableObject;
	public GameObject InstantiablePickup;
	public float price;
	public GameObject BulletHole;
	public GameObject DirtSpray;
	public GameObject BloodSpray;
	public Vector3 Position;
	public Vector3 ScopedPosition;
	public Vector3 StowedPosition;
	public Vector3 StowedRotationAsEulerAngles;
	public float switchSpeed = 1f;
	public float AimSpeed = 10f;
	public float ScopeZoom;
	public float NormalZoom;
	public float zoomSmoothing;
	public int Damage;
	public float HitStrength = 50;
	public int FireRateAsPercent;
	[HideInInspector]
	public double fireRate;
	public AmmoType ammoType;
	public int MaxAmmo;
	public int CurAmmo;
	public string WeaponName;
	public string DisplayName;
	public string Path = ".../";
	public bool IsValid = false;
	public bool Automatic;
	public float Range = 300f;
	public int numOfShots = 200;
	public float xSpread = 15;
	public float yspread = 15;
	
	
	[HideInInspector]
	public GameObject camera;
	[HideInInspector]
	public bool isAimed = false;
	public bool Exists = false;
	[HideInInspector]
	public bool isFiring = false;
	[HideInInspector]
	public GameObject mainObject = null;
	[HideInInspector]
	public GameObject flash = null;
	public int AnimClock = 0;
	public weaponAnimType curAnim = weaponAnimType.None;
	
	[HideInInspector]
	public Transform Slide;
	public int SlideDelay;
	[HideInInspector]
	public int ShotDelay;
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
	
	public float maxRoundsPerSecond;
	float lastShot = 0f;
	public float shotDelay;
	
	public float reloadTime = 5f;
	
}
