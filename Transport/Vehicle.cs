using UnityEngine;
using System.Collections;


/// <summary>
/// Represents a vehicle that one may drive.
/// Broken.
/// </summary>
public class Vehicle : MonoBehaviour {
	
	public GameObject player;
	public bool isOccupied;
	public bool isActive;
	public Vector3 ExitLocation;
	public VehicleControls VControls;
	public Controls controls;
	
	public void activate (GameObject Player) {
		player = Player.transform.parent.gameObject;
		VControls.isCarActive = true;
		controls = player.transform.FindChild("Camera").gameObject.GetComponent<Controls>();
		isActive = true;
		player.SetActive(false);
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = true;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = true;
	}
	
	public void deactivate () {
		VControls.isCarActive = false;
		isActive = false;
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
		if (Input.GetKeyDown(controls.interact) && isActive) {
			deactivate();
		}
	}
}
