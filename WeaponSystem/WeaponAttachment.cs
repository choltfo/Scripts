using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponAttachment {
	public GameObject instantiableThing;
	public ConnectionType railType = ConnectionType.PicitannySide;
	public bool isValid = false;
	public AttachmentType type = AttachmentType.Flashlight;
	public string toggleableObjectPath;
	public Vector3 rot;
	public AudioClip silencerNoise;
	public Material laserMat;
	
	GameObject Thing;
	bool on = true;
	GameObject toggleableObject;
	
	public static bool isToggleable(AttachmentType type) {
		switch (type) {
		case AttachmentType.Flashlight: return true;
		case AttachmentType.Laser: 		return true;
		case AttachmentType.Foregrip: 	return false;
		case AttachmentType.Scope: 		return false;
		case AttachmentType.IronSight: 	return false;
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
		if (type == AttachmentType.Laser) {
			Thing.AddComponent<LineRenderer>();
			Thing.GetComponent<LineRenderer>().material = laserMat;
			Thing.GetComponent<LineRenderer>().useWorldSpace = false;
			Thing.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0,0,0));
			Thing.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0,0,-1000));
			Thing.GetComponent<LineRenderer>().SetWidth(0.05f,0.05f);
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

[System.Serializable]
public class HardPoint {
	public string name;
	public ConnectionType connectionType = ConnectionType.PicitannySide;
	public WeaponAttachment attachment;
	public Vector3 position;
}

public enum ConnectionType {
	PicitannySide,
	WeaverSide,
	BarrelTip,
	PicitannyScope,
	WeaverScope
}

public enum AttachmentType {
	Flashlight,
	Laser,
	Foregrip,
	Scope,
	IronSight,
	Silencer
}