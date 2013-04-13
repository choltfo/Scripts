using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int Health = 100;
	
	void Update() {
		if (Health <= 0) {
			Destroy(gameObject);
		}
	}
}
