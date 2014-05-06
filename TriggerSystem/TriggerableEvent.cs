using UnityEngine;
using System.Timers;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class TriggerableEvent {
	
	public string name;
	
	public Speech[] speechs;
	
	public TriggerableEvent[] events;
	public ExplosiveDamage[] explosions;
	public AudioSource soundSource;
	public AudioClip sound;
	public bool showText = false;
	public List<SubtitleLine> Text;
	public QuickTimeSpamEvent Spam;
	public QuickTimeSpamEventController QTSEController;
	public StatResult statMod;
	
	public GameObject instantiableObject;
	public Vector3 place;
	public Vector3 tolerance;
	public int number;
	
	public List<AudioClip> RadioSounds;
	public Radio radio;
	public bool radioState;
	public editSettings radioWriteStyle;
	
	public List<DoorControl> doors;
	
	public ObjectManipulation[] OMs;
	
	public List<UniqueEffect> UE;
	
	public void Trigger(SubtitleController TextDisplay) {
		foreach (ExplosiveDamage explosion in explosions) {
			if (explosion != null) explosion.explode(TextDisplay);
			try {
				if (explosion.GetComponent<Detonator>() != null) explosion.GetComponent<Detonator>().Explode();
			} catch (SystemException e) {
				Debug.LogError ("Explosion Failed. DAMNIT IT!");
				Debug.LogError (e.ToString());
			}
		}
		
		
		if (QTSEController != null) QTSEController.Queue(Spam);
		if (sound != null && soundSource != null) {
			soundSource.PlayOneShot(sound);
		}
		if (showText) {
			TextDisplay.setLines(Text);
		}
		if (instantiableObject!=null){
			for (int i = 0; i < number; i++) {
				MonoBehaviour.Instantiate(instantiableObject, (UnityEngine.Random.value * tolerance) + place, new Quaternion(0,0,0,1));
			}
		}
		
		if (radioWriteStyle == editSettings.append) {
			foreach (AudioClip clip in RadioSounds) radio.clips.Add(clip);
		}
		if (radioWriteStyle == editSettings.overwrite) {
			radio.clips = RadioSounds;
		}
		foreach (DoorControl d in doors) d.d.setState(d.open);
		foreach (ObjectManipulation OM in OMs) {
			OM.use();
		}
		foreach (UniqueEffect ue in UE) {
			ue.trigger();
		}
		foreach (TriggerableEvent TEvent in events) {
			TEvent.Trigger(TextDisplay);
		}
	}
}
[System.Serializable]
public enum editSettings {
	append,
	overwrite
}
[System.Serializable]
public class DoorControl {
	public Door d;
	public bool open;
}
[System.Serializable]
public class ObjectManipulation {
	public GameObject[] GOs;
	public ObjectManipulationType type;
	public Vector3 vec;
	
	public void use () {
		foreach (GameObject go in GOs) {
			switch (type) {
			case ObjectManipulationType.DeleteGO :
				MonoBehaviour.Destroy(go);
				break;
			case ObjectManipulationType.RotateGO :
				go.transform.Rotate(vec);
				break;
			case ObjectManipulationType.TranslateGO :
				go.transform.Translate(vec);
				break;
			case ObjectManipulationType.RemoveCOMP :
				MonoBehaviour.Destroy(go);
				break;
			case ObjectManipulationType.DisableCOMP :
				go.SetActive(false);
				break;
			case ObjectManipulationType.EnableCOMP :
				go.SetActive(true);
				break;
			}
		}
	}
}

[System.Serializable]
public enum ObjectManipulationType {
	DeleteGO,
	RotateGO,
	TranslateGO,
	RemoveCOMP,
	DisableCOMP,
	EnableCOMP
}
