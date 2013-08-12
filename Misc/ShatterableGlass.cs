using UnityEngine;
using System.Collections;

public class ShatterableGlass : MonoBehaviour {
	
	public float remainingStrength;
	public float maximumStrength;
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void shoot (RaycastHit hit, float power) {
		hit.normal.Normalize();
		particleSystem.Emit(new Vector3(0,0,0), (hit.normal.normalized * power), Random.Range(0.1f, 0.25f), 1f, renderer.material.color);
		particleSystem.Emit(200);
		renderer.enabled = false;
		collider.enabled = false;
	}
}
