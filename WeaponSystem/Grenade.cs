using UnityEngine;
using System.Collections;

[System.Serializable]
public class Grenade {
	public string name = "M67";
	public GameObject instantiableGrenade;
	public GameObject holdableGrenade;
	public float range;
	public float maxDamage;
	public float detonateDelay;
	public Vector3 holdPosition;
	
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
		GameObject thrownGrenade = (GameObject)MonoBehaviour.Instantiate(instantiableGrenade, thrower.position, thrower.rotation);
		thrownGrenade.transform.Translate(holdPosition);
		thrownGrenade.transform.Rotate(-45,0,0);
		thrownGrenade.AddComponent("ThrownGrenade");
		thrownGrenade.AddComponent("Rigidbody");
		thrownGrenade.GetComponent<Rigidbody>().mass = 0.05f;//		\/- Change this.
		thrownGrenade.GetComponent<Rigidbody>().AddRelativeForce(0,0,throwPower);
		thrownGrenade.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		thrownGrenade.GetComponent<Detonator>().explodeOnStart = false;
		thrownGrenade.GetComponent<ThrownGrenade>().prime(detonateDelay);
		thrownGrenade.GetComponent<ThrownGrenade>().range = range;
		thrownGrenade.GetComponent<ThrownGrenade>().maxDamage = maxDamage;
		return thrownGrenade.GetComponent<ThrownGrenade>();
	}
}

public enum GrenadeType {
	Frag,
	Incendiary,
	Toxic
}