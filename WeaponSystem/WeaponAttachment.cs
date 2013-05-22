using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponAttachment {
	public GameObject instantiableThing;
	public RailType railType = RailType.Picitanny;
	public bool isValid = false;
	public AttachmentType type = AttachmentType.Flashlight;
	public string toggleableObjectPath;
	
	GameObject Thing;
	bool on = true;
	GameObject toggleableObject;
	
	public bool deploy (GameObject parent, Vector3 relPos) {
		Debug.Log("Activating an Attachment");
		if (!isValid) return false;
		Debug.Log("Activating an Attachment successfully");
		Thing = (GameObject)MonoBehaviour.Instantiate(instantiableThing, new Vector3 (0,0,0), parent.transform.rotation);
		Thing.transform.parent = parent.transform;
		Thing.transform.localPosition = relPos;
		//Thing.transform.Translate(relPos);
		toggleableObject = Thing.transform.FindChild(toggleableObjectPath).gameObject;
		return true;
	}
	
	public bool toggle() {
		toggleableObject.SetActive(on);
		return on = !on;
	}
}

public enum AttachmentType {
	Flashlight,
	Laser
}