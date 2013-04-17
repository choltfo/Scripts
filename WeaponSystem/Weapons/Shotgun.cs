using UnityEngine;
using System.Collections;

[System.Serializable]
public class Shotgun : Weapon {
	
	public int numOfShots = 200;
	public float xSpread = 15;
	public float yspread = 15;
	
	
	public Shotgun() {}
	
	
	public bool Shoot(Camera camera) {
		RaycastHit hit;
		Random random = new Random();
		if (CurAmmo > 0/*TODO Modify:  && !vehicle.riding*/ && !isFiring){
			CurAmmo -= 1;
			((AudioSource)mainObject.GetComponent("AudioSource")).Play();
			
			AnimClock = (int)(30/(7.5 * FireRateAsPercent / 100));
			ShotDelay = (int)(30/(7.5 * FireRateAsPercent / 100));
			
			isFiring = true;
			
			for (int i = 0; i<numOfShots; i++) {
				if( Physics.Raycast( ray, out hit, 100 ) ){
				int x = random.Next(-xSpread, xSpread);
				int y = random.Next(-ySpread, ySpread);
				Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2+x,Screen.height/2+y,0));
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
			return true;
		}
			}
		return false;
	}
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