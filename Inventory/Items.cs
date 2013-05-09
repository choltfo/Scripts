/// <saummary>
/// Uh, yeah. Not really much going on here.
/// Seemed like a good idea at the time.
/// </summary>

public enum Items {
	Nut,
	Bolt,
	Bandage,
	Medkit
}

/// <summary>
/// Get Item prices.
/// </summary>
public static class ItemPrice {
	static public float Get (Items item) {
		switch (item) {
		case Items.Nut:		return 1;
		case Items.Bolt:	return 20;
		case Items.Bandage: return 30;
		case Items.Medkit:	return 100;
		}
		return 0;
	}
}