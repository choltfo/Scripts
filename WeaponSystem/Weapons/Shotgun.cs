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
		if (CurAmmo > 0/*TODO Modify:  && !vehicle.riding*/ && !isFiring){
			CurAmmo -= 1;
			((AudioSource)mainObject.GetComponent("AudioSource")).Play();
			
			AnimClock = (int)(30/(7.5 * FireRateAsPercent / 100));
			ShotDelay = (int)(30/(7.5 * FireRateAsPercent / 100));
			
			isFiring = true;
			
			for (int i = 0; i<numOfShots; i++) {
				Vector2 position = Random.insideUnitCircle;
  				int x = (int)(position.x * xSpread);
  				int y = (int)(position.y * yspread);
				Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2+x,Screen.height/2+y,0));
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
				return true;
			}
		}
		return false;
	}

	public void AnimIdentify() {
		return;	
	}
	
	public void AnimUpdate() {
		Debug.Log("Called from Shotgun.cs");
		return;
	}
}