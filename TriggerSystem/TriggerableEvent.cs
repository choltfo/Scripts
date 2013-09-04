using UnityEngine;
using System.Timers;
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
			if (explosion.GetComponent<Detonator>() != null) explosion.GetComponent<Detonator>().Explode();
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
				MonoBehaviour.Instantiate(instantiableObject, (Random.value * tolerance) + place, new Quaternion(0,0,0,1));
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
	public Component[] GOs;
	public ObjectManipulationType type;
	public Vector3 vec;
	
	public void use () {
		foreach (Component co in GOs) {
			switch (type) {
			case ObjectManipulationType.DeleteGO :
				MonoBehaviour.Destroy((Object)co.gameObject);
				break;
			case ObjectManipulationType.RotateGO :
				co.transform.Rotate(vec);
				break;
			case ObjectManipulationType.TranslateGO :
				co.transform.Translate(vec);
				break;
			case ObjectManipulationType.RemoveCOMP :
				MonoBehaviour.Destroy((Object)co);
				break;
			case ObjectManipulationType.DisableCOMP :
				if (co is Behaviour) {
					((Behaviour) co).enabled = false;
				} else if (co is Rigidbody) {
					((Rigidbody) co).Sleep();
				}
				break;
			case ObjectManipulationType.EnableCOMP :
				if (co is Behaviour) {
					((Behaviour) co).enabled = true;
				}
				if (co is Rigidbody) {
					((Rigidbody) co).WakeUp();
				}
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
