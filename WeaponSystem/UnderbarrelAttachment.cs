using UnityEngine;
using System.Collections;

[System.Serializable]
public class UnderbarrelAttachment {	
	/// <summary>
	/// The prefab that should be created when this gun is pulled out.
	/// </summary>
	public GameObject InstantiableObject;
	/// <summary>
	/// The prefab that should be instantiated when this gun is dropped.
	/// </summary>
	public GameObject InstantiablePickup;
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
	/// The position, relative to the Gun,
	/// that the gun is held at when not scoping.
	/// </summary>
	public Vector3 Position;
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
	/// The display name of the weapon (Because SIG P220 doesn;t roll of the tounge).
	/// </summary>
	public string DisplayName;
	/// <summary>
	/// The path to the animated objects, e.g muzzle flare and slide.
	/// </summary>
	public string Path = ".../";
	[HideInInspector]
	public bool IsValid = true;
	/// <summary>
	/// Whether or not the gun is automatic (pewpewpew!) or semi automatic (pew! pew!).
	/// </summary>
	public bool Automatic;
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
	public GameObject Gun;
	[HideInInspector]
	public bool isAimed = false;
	public bool Exists = false;
	[HideInInspector]
	public bool isFiring = false;
	[HideInInspector]
	public GameObject mainObject = null;
	[HideInInspector]
	public GameObject flash = null;
	[HideInInspector]
	public int AnimClock = 0;
	
	[HideInInspector]
	public Transform Hammer;
	[HideInInspector]
	public Transform Slide;
	[HideInInspector]
	public Transform Trigger;
	/// <summary>
	/// The angle, in degrees, that the Gun moves up after each shot.
	/// </summary>
	public float GunClimb = 2f;
	/// <summary>
	/// The angle, in degrees, that the Gun moves left
	/// and right after each shot.
	/// </summary>
	public float maxGunSway = 2f;
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
	public int ShotDelay;

	public bool Shoot(Camera Gun) {
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
			for (int i = 0; i<numOfShots; i++) {
				Vector2 position = Random.insideUnitCircle;
  				int x = (int)(position.x * xSpread);
  				int y = (int)(position.y * yspread);
				Ray ray = Gun.ScreenPointToRay(new Vector3(Screen.width/2+x,Screen.height/2+y,0));
				RaycastHit hit;
				if( Physics.Raycast( ray, out hit, 100 ) ){
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

	public void activate(GameObject l_Gun){
		Gun = l_Gun;
		GameObject Attachment = (GameObject)MonoBehaviour.Instantiate(InstantiableObject, new Vector3 (0,0,0), Gun.transform.rotation);
		Attachment.transform.parent = l_Gun.transform;
		Attachment.transform.localPosition = Position;
		mainObject = Attachment;
		/*flash = GameObject.Find(mainObject.transform.name + "/" + Path + "Flash");
		if (flash != null) {
			//Debug.Log("Found flash for " + WeaponName + " at " + mainObject.transform.name +  "/" + Path + flash.name);
			flash.transform.localScale = new Vector3 (0,0,0);
		} else {
			Debug.Log("Didn't find flash for " + WeaponName + " at " + mainObject.transform.name + "/" + Path + "Flash");
		}
		flash.transform.localScale = new Vector3 (0,0,0);*/
		//MonoBehaviour.print("Added " + WeaponName);
		//AnimIdentify();
		isAimed = false;
		Exists = true;
	}
}
