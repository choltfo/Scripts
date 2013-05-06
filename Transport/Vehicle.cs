using UnityEngine;
using System.Collections;


/// <summary>
/// Represents a vehicle that one may drive.
/// Broken.
/// </summary>
public class Vehicle : MonoBehaviour {
	
	public bool isOccupied;
	public bool active;
	public Vector3 ExitLocation;
	public VehicleControls controls;
	
	public void activate () {
		controls.active = true;
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = true;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = true;
	}
	
	public void deactivate () {
		controls.active = false;
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	// Use this for initialization
	void Start () {
		controls = gameObject.GetComponent<VehicleControls>();
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
