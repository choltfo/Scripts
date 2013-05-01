using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {
	
	public ShootObjects weaponController;
	
	public GameObject fist;
	public AnimationCurve RFAnimX;
	public AnimationCurve RFAnimY;
	public AnimationCurve RFAnimZ;
	public AnimationCurve LFAnimX;
	public AnimationCurve LFAnimY;
	public AnimationCurve LFAnimZ;
	
	public float damage = 10;
	public float knockback = 5;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
