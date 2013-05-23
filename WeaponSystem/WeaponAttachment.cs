using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponAttachment {
	public GameObject instantiableThing;
	public ConnectionType railType = ConnectionType.Picitanny;
	public bool isValid = false;
	public AttachmentType type = AttachmentType.Flashlight;
	public string toggleableObjectPath;
	public Vector3 rot;
	
	GameObject Thing;
	bool on = true;
	GameObject toggleableObject;
	
	public static bool isToggleable(AttachmentType type) {
		switch (type) {
		case AttachmentType.Flashlight: return true;
		case AttachmentType.Laser: 		return true;
		case AttachmentType.Foregrip: 	return false;
		case AttachmentType.Scope: 		return false;
		case AttachmentType.Silencer: 	return false;
		}
		return false;
	}
	
	public bool deploy (GameObject parent, Vector3 relPos) {
		Debug.Log("Activating an Attachment");
		if (!isValid) return false;
		Debug.Log("Activating an Attachment successfully");
		Thing = (GameObject)MonoBehaviour.Instantiate(instantiableThing, new Vector3 (0,0,0), parent.transform.rotation);
		Thing.transform.parent = parent.transform;
		Thing.transform.localPosition = relPos;
		Thing.transform.Rotate(rot, Space.Self);
		//Thing.transform.Translate(relPos);
		if (isToggleable(type)) {
			toggleableObject = Thing.transform.FindChild(toggleableObjectPath).gameObject;
			toggle();
		}
		return true;
	}
	
	public bool toggle() {
		if (!isValid) return false;
		if (!isToggleable(type)) return false;
		toggleableObject.SetActive(on);
		on = !on;
		return true;
	}
}

public enum AttachmentType {
	Flashlight,
	Laser,
	Foregrip,
	Scope,
	Silencer
}