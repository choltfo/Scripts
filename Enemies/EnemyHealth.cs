using UnityEngine;
using System.Collections;

/// <summary>
/// The health level of any enemy.
/// </summary>

public class EnemyHealth : MonoBehaviour {
	/// <summary>
	/// The health level of the enemy.
	/// </summary>
	public int Health = 100;
	
	void Update() {
		if (Health <= 0) {
			Destroy(gameObject);
		}
	}
}
