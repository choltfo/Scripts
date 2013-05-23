using UnityEngine;
using System.Collections;

[System.Serializable]
public class Grenade {
	public string name = "M67";
	public float price; 
	public GameObject instantiableGrenade;
	public GameObject holdableGrenade;
	public float range;
	public float maxDamage;
	public float detonateDelay;
	public Vector3 holdPosition;
	public int throwAngle = 45;
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
		thrownGrenade.GetComponent<Rigidbody>().mass = 0.05f;
		Rigidbody player = thrower.parent.gameObject.rigidbody;
		Debug.Log(player);
		Debug.Log(player.angularVelocity);
		thrownGrenade.GetComponent<Rigidbody>().AddRelativeForce(player.angularVelocity.x, player.angularVelocity.y, player.angularVelocity.z + throwPower);
		thrownGrenade.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		thrownGrenade.GetComponent<Detonator>().explodeOnStart = false;
		thrownGrenade.GetComponent<ThrownGrenade>().prime(detonateDelay);
		thrownGrenade.GetComponent<ThrownGrenade>().range = range;
		thrownGrenade.GetComponent<ThrownGrenade>().maxDamage = maxDamage;
		thrownGrenade.GetComponent<ThrownGrenade>().detonateOnCollision = detonateOnCollision;
		return thrownGrenade.GetComponent<ThrownGrenade>();
	}
}

public enum GrenadeType {
	Frag,
	Incendiary,
	Toxic
}