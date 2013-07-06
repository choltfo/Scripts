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
	[HideInInspector]
	public CharacterController motor;
	[HideInInspector]
	public EnemyHealth health;
	
	public Faction faction;
}

/// <summary>
/// The enemy's associated faction. Basically, who doesn't shoot at who.
/// </summary>
public enum Faction {
	Friendly,			// He is with the player. 
	Evil				// He is ont with the player.
}
