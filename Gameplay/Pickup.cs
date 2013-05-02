using UnityEngine;
using System.Collections;
/// <summary>
/// DEPRECATED, DO NOT USE!
/// A weapon pickup attached to a gameobject.
/// </summary>
public class Pickup : MonoBehaviour {
	
	public string Type = "Weapon";
	string GunName = "WeaponNameHere";
	public bool DestroyOnUse = false;
	public int Number;
	public ShootController weaponController;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Interact() {
		if (DestroyOnUse) {
			Destroy(gameObject);
			weaponController.SelectWeapon(GunName);
			weaponController.IsInPossession[Number - 1] = true;
		}
	}
}
