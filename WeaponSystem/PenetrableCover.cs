using UnityEngine;
using System.Collections;

public class PenetrableCover : MonoBehaviour {
	
	public float impermeability = 50; // 100 is stainless steel, 10 is balsa wood.
	
	public float tolerance = 100; // 100 is unmarred, 0 is gone.
	
	public ParticleSystem PS;
	
	// Update is called once per frame
	void Update () {
		if (tolerance <= 0) {
			PS.Play();
			Destroy(gameObject);
		}
	}
	
	public void catchBullet () {
		
	}
}
