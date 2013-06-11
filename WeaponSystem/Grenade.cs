using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// A grenade inside ones inventory.
/// </summary>
public class Grenade {
	/// <summary>
	/// The name of the grenade.
	/// </summary>
	public string name = "M67";
	/// <summary>
	/// The price of this grenade.
	/// </summary>
	public float price;
	/// <summary>
	/// The object to throw. Should be explosive.
	/// </summary>
	public GameObject instantiableGrenade;
	/// <summary>
	/// The object to hold before throwing.
	/// </summary>
	public GameObject holdableGrenade;
	/// <summary>
	/// The range to make the grenade explode with.
	/// </summary>
	public float range;
	/// <summary>
	/// The damage incurred at ground zero.
	/// </summary>
	public float maxDamage;
	/// <summary>
	/// The time before it blows to bits.
	/// </summary>
	public float detonateDelay;
	/// <summary>
	/// The position to hold the grenade at when throwing it.
	/// </summary>
	public Vector3 holdPosition;
	/// <summary>
	/// The angle, relative to the camera.
	/// </summary>
	public int throwAngle = 45;
	/// <summary>
	/// Should the grenade explode on contact with anything?
	/// </summary>
	public bool detonateOnCollision = false;
	
	/// <summary>
	/// Throws the grenade.
	/// </summary>
	/// <returns>
	/// The grenade thrown.
	/// </returns>
	/// <param name='throwPower'>
	/// If set to <c>true</c> throw power.
	/// </param>
	public ThrownGrenade throwGrenade (float throwPower, Transform thrower) {
		GameObject thrownGrenade = MonoBehaviour.Instantiate(instantiableGrenade, thrower.position, thrower.rotation) as GameObject;
		thrownGrenade.transform.Translate(holdPosition);
		thrownGrenade.transform.Rotate(-throwAngle,0,0);
		thrownGrenade.AddComponent("ThrownGrenade");
		thrownGrenade.AddComponent("ExplosiveDamage");
		
		thrownGrenade.GetComponent<ExplosiveDamage>().range = range;
		thrownGrenade.GetComponent<ExplosiveDamage>().maxDamage = maxDamage;
		
		thrownGrenade.GetComponent<Rigidbody>().mass = 0.05f;
		Rigidbody player = thrower.parent.gameObject.rigidbody;
		thrownGrenade.GetComponent<Rigidbody>().AddRelativeForce(player.velocity.x, player.velocity.y, player.velocity.z + throwPower);
		thrownGrenade.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		
		thrownGrenade.GetComponent<Detonator>().explodeOnStart = false;
		thrownGrenade.GetComponent<ThrownGrenade>().prime(detonateDelay);
		thrownGrenade.GetComponent<ThrownGrenade>().detonateOnCollision = detonateOnCollision;
		return thrownGrenade.GetComponent<ThrownGrenade>();
	}
	
	public GameObject convert(GameObject grenade) {
		grenade.AddComponent<ThrownGrenade>();
		grenade.GetComponent<Rigidbody>().mass = 0.05f;
		grenade.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		grenade.GetComponent<Detonator>().explodeOnStart = false;
		grenade.GetComponent<ThrownGrenade>().prime(detonateDelay);
		grenade.GetComponent<ThrownGrenade>().detonateOnCollision = detonateOnCollision;
		return grenade;
	}
}

/// <summary>
/// Lists types of grenades. Doesn't really do much. 
/// </summary>
public enum GrenadeType {
	/// <summary>
	/// A fragmentation
	/// Grenade. Default. Deals damage
	/// In all directions 
	/// </summary>
	Frag,
	/// <summary>
	/// An incendiary grenade. Starts a fire everywhere in range.
	/// </summary>
	Incendiary,
	/// <summary>
	/// A gas grenade. Creates a cloud of gas.
	/// </summary>
	Toxic
}