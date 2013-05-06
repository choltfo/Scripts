using UnityEngine;
using System.Collections;


/// <summary>
/// Represents a vehicle that one may drive.
/// Broken.
/// </summary>
public class Vehicle : MonoBehaviour {
	
	public GameObject player;
	public bool isOccupied;
	public bool active;
	public Vector3 ExitLocation;
	public VehicleControls VControls;
	public Controls controls;
	
	public void activate (GameObject Player) {
		player = Player.transform.parent.gameObject;
		VControls.active = true;
		controls = player.transform.FindChild("Camera").gameObject.GetComponent<Controls>();
		active = true;
		player.SetActive(false);
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = true;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = true;
	}
	
	public void deactivate () {
		VControls.active = false;
		active = false;
		player.SetActive(true);
		player.transform.position = transform.position + ExitLocation;
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	// Use this for initialization
	void Start () {
		VControls = gameObject.GetComponent<VehicleControls>();
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	void Update () {
		if (Input.GetKeyDown(controls.interact) && active) {
			deactivate();
		}
	}
}
