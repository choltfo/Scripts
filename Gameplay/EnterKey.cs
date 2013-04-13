using UnityEngine;
using System.Collections;

public class EnterKey : MonoBehaviour {
	
	public Controls controls;
	public ShootController WeaponController;
	public float InteractDistance = 5;
	public bool riding = false;
	public GameObject currentVehicle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(controls.interact)) {
			print("Attempting interaction)");
			if (riding) {
				print("Exiting Vehicle " + currentVehicle.transform.gameObject.name);
				((Camera)gameObject.transform.GetComponent("Camera")).enabled = true;
				((Camera)currentVehicle.transform.root.Find("Camera").GetComponent("Camera")).enabled = false;
				((AudioListener)currentVehicle.transform.root.Find("Camera").GetComponent("AudioListener")).enabled = false;
				((AudioListener)gameObject.transform.GetComponent("AudioListener")).enabled = true;
				riding = false;
				((Vehicle)currentVehicle.transform.root.gameObject.GetComponent("Vehicle")).isOccupied = false;	
				transform.root.position = currentVehicle.transform.position + ((Vehicle)currentVehicle.GetComponent("Vehicle")).ExitLocation;
				currentVehicle = null;
			} else {
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
				RaycastHit hit;
				if( Physics.Raycast( ray, out hit, 100 ) && hit.distance < InteractDistance) {
					if (hit.transform.root.gameObject.GetComponent("Vehicle") != null){
						print("Entering Vehicle " + hit.transform.gameObject.name);
						((Camera)hit.transform.root.Find("Camera").GetComponent("Camera")).enabled = true;
						((Camera)gameObject.transform.GetComponent("Camera")).enabled = false;
						((AudioListener)hit.transform.root.Find("Camera").GetComponent("AudioListener")).enabled = true;
						((AudioListener)gameObject.transform.GetComponent("AudioListener")).enabled = false;
						riding = true;
						((Vehicle)hit.transform.root.gameObject.GetComponent("Vehicle")).isOccupied = true;
						currentVehicle = hit.transform.gameObject;
					}
					
					if (hit.transform.root.gameObject.GetComponent("Pickup") != null){
						Pickup Item = (Pickup)hit.transform.root.gameObject.GetComponent("Pickup");
						Item.Interact();
					}
				}
			}
		}
		if (Input.GetKeyDown(controls.weapon0)) {
			WeaponController.SelectWeapon(0);
		}
		if (Input.GetKeyDown(controls.weapon1)) {
			WeaponController.SelectWeapon(1);
		}
		if (Input.GetKeyDown(controls.weapon2)) {
			WeaponController.SelectWeapon(2);
		}
	}
}