[System.Serializable]

/// <summary>
/// Weapon ammo types.
/// Add all of them that are not here yet as needed
/// </summary>
public enum AmmoType {
	
	/// <summary>
	/// Barretta M9, P220
	/// </summary>
	Parabellum9x19mm,
	/// <summary>
	/// SCAR-H
	/// </summary>
	NATO762x51mm,
	/// <summary>
	/// M4A1
	/// </summary>
	NATO556,
	/// <summary>
	/// AK-47
	/// </summary>
	FMJ762x39mm,
	/// <summary>
	/// Crossbow bolt
	/// </summary>
	FourtyCMBolt,
	/// <summary>
	/// 40MM grenade launcher
	/// </summary>
	Grenade40mm,
	/// <summary>
	/// Heavy machine gun, M107
	/// </summary>
	BMG50,
	/// <summary>
	/// Phaser
	/// </summary>
	ChargeCapsule,
	/// <summary>
	/// 15 mm Manu Tormento
	/// </summary>
	Parabellum15x25
}

/// <summary>
/// Get Ammo prices.
/// </summary>
public static class AmmoPrice {
	
	/// <summary>
	/// Get the price of a specified bullet type.
	/// </summary>
	/// <param name='ammo'>
	/// The type of ammo to get the price of.
	/// </param>
	static public float Get (AmmoType ammo) {
		switch (ammo) {
		case AmmoType.Parabellum9x19mm:	return 1;
		case AmmoType.NATO762x51mm:		return 2;
		case AmmoType.FMJ762x39mm: 		return 3;
		case AmmoType.FourtyCMBolt:		return 4;
		case AmmoType.Grenade40mm:		return 20;
		case AmmoType.BMG50:			return 5;
		case AmmoType.ChargeCapsule:	return 20;
		default:						return 1;
		}
	}
}

/// <summary>
/// Get the penetration ability of the selected round as a percent.
/// </summary>
/// <param name='ammo'>
/// The type of ammo to get the price of.
/// </param>
public static class AmmoPenetration {
	static public float Get (AmmoType ammo) {
		switch (ammo) {
		case AmmoType.Parabellum9x19mm:	return 10;
		case AmmoType.NATO762x51mm:		return 25;
		case AmmoType.FMJ762x39mm: 		return 20;
		case AmmoType.FourtyCMBolt:		return 5;
		case AmmoType.Grenade40mm:		return 4;
		case AmmoType.BMG50:			return 70;
		case AmmoType.ChargeCapsule:	return 10;
		default:						return 1;
		}
	}
}
