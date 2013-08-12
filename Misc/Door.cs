using UnityEngine;
using System.Collections;

public class Door : InteractObject {
	
	public bool isOpen;
	public Vector3 ClosedRotation;
	public Vector3 OpenRotation;
	public Vector3 ClosedPosition;
	public Vector3 OpenPosition;
	
	public bool autoClose = false;
	
	public override void Interact (GameObject player, ShootObjects SOPlayer) {
		isOpen = !isOpen;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpen) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, OpenPosition, Time.deltaTime);
			transform.localEulerAngles = Vector3.Slerp(transform.localRotation.eulerAngles, OpenRotation, Time.deltaTime);
		} else {
			transform.localPosition = Vector3.Lerp(transform.localPosition, ClosedPosition, Time.deltaTime);
			transform.localEulerAngles = Vector3.Slerp(transform.localRotation.eulerAngles, ClosedRotation, Time.deltaTime);
		}
	}
}
