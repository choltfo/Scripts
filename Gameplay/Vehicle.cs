using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
	
	public bool isOccupied = false;
	public Vector3 ExitLocation;
	
	
	// Use this for initialization
	void Start () {
		((Camera)gameObject.transform.Find("Camera").gameObject.GetComponent("Camera")).enabled = false;
		((AudioListener)gameObject.transform.Find("Camera").gameObject.GetComponent("AudioListener")).enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
