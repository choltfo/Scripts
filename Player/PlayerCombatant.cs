using UnityEngine;
using System.Collections;

public class PlayerCombatant : Enemy {
	
	public Weapon currentWeapon;
	
	ShootObjects so;
	
	// Use this for initialization
	void Start () {
		so = GetComponent<ShootObjects>();
	}
	
	// Update is called once per frame
	void Update () {
		currentWeapon = so.inventory.weapons[so.currentWeapon];
	}
}
