[System.Serializable]

/// <summary>
/// Weapon ammo types.
/// Add all of them that are not here yet as needed
/// </summary>
public enum AmmoType {
	Parabellum9x19mm,		//Barretta M9, P220
	NATO762x51mm,			//SCAR-H
	NATO556,				//M4A1
	FMJ762x39mm,			//AK47
	FourtyCMBolt,			//Crossbow
	Grenade40mm				//Launching Grenade
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
		default: return 1;
		}
	}
}