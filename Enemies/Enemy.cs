using UnityEngine;
using System.Collections;

/// <summary>
/// A wrapper class for enemies.
/// </summary>
public class Enemy : MonoBehaviour {
	
	public Faction faction = Faction.Evil;
}

/// <summary>
/// The enemy's associated faction. Basically, who doesn't shoot at who.
/// </summary>
public enum Faction {
	Friendly,			// He is with the player.
	Evil				// He is ont with the player.
}
