using UnityEngine;
using System.Collections;

public class HitNoises : MonoBehaviour {
	public AudioSource source;
	public AudioClip clip;
	
	void OnCollisionEnter (Collision collision) {
		source.PlayOneShot(clip);
	}

}
