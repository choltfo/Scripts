using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

/// <summary>
/// A wrapper class for enemies.
/// </summary>
public class Enemy : MonoBehaviour {
	/// <summary>
	/// The usefulness scale.
	/// Really Ian?
	/// </summary>
	public int usefulnessScale = 2;
	/// <summary>
	/// The player to attack.
	/// </summary>
	public GameObject player;
	CharacterController motor;
	EnemyHealth health;
	/// <summary>
	/// Whether the game is paused.
	/// </summary>
	public bool isPaused = false;
}
