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
	public GameObject[] Headlights;
	
	public bool erectOnEnter = false;
	
	
	
	public void activate (GameObject Player) {
		if (isOccupied) {
			return;
		}
		
		if (erectOnEnter) {
			transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		}
		player = Player.transform.parent.gameObject;
		
		isOccupied = true;
		
		if (VControls != null) VControls.isCarActive = true;
		controls = player.transform.FindChild("Camera").gameObject.GetComponent<Controls>();
		isActive = true;
		player.SetActive(false);
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = true;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = true;
		
		foreach (GameObject go in Headlights) {
			go.SetActive(true);
		}
		
		if (erectOnEnter && isActive) {
			float z = transform.eulerAngles.z;
			float x = transform.eulerAngles.x;
			transform.Rotate(-x, 0, -z);
		}
	}
	
	public void deactivate () {
		if (VControls != null) VControls.isCarActive = false;
		isActive = false;
		player.SetActive(true);
		
		isOccupied = false;
		
		player.transform.position = transform.position + ExitLocation;
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
		foreach (GameObject go in Headlights) {
			go.SetActive(false);
		}
	}
	
	// Use this for initialization
	void Start () {
		VControls = gameObject.GetComponent<VehicleControls>();
		if (!isOccupied) ((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		if (!isOccupied) ((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	void Update () {
		if (Input.GetKeyDown(controls.interact) && isActive) {
			deactivate();
		}
	}
}
