using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	public bool isOpen;
	public Vector3 ClosedRotation;
	public Vector3 OpenRotation;
	public Vector3 ClosedPosition;
	public Vector3 OpenPosition;
	
	public bool autoClose = false;
	public float autoCloseTime = 10f; 
	float lastOpen = 0f;
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (isOpen) {
			transform.localPosition = Vector3.Lerp(transform.localPosition, OpenPosition, Time.deltaTime);
			transform.localEulerAngles = Vector3.Slerp(transform.localRotation.eulerAngles, OpenRotation, Time.deltaTime);
			if (Time.time > lastOpen + autoCloseTime) {
				isOpen = false;
			}
		} else {
			transform.localPosition = Vector3.Lerp(transform.localPosition, ClosedPosition, Time.deltaTime);
			transform.localEulerAngles = Vector3.Slerp(transform.localRotation.eulerAngles, ClosedRotation, Time.deltaTime);
		}
		
		
	}
	
	public void setState (bool state) {
		isOpen = state;
		lastOpen = Time.time;
	}
}
