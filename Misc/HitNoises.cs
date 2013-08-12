using UnityEngine;
using System.Collections;

/// <summary>
/// Makes stuff make nois when it hits other stuff.
/// </summary>
public class HitNoises : MonoBehaviour {
	public AudioClip clip;
	
	void OnCollisionEnter (Collision collision) {
		audio.PlayOneShot(clip);
	}

}
