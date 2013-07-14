using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	public float speed;
	public float generalResistance;
	public float recoilFactor;
	
	public float xp;
	
	public CharacterControls CControls;
	public Health health;

    void Start() {
        CControls = this.gameObject.GetComponent<CharacterControls>();
        health = this.gameObject.GetComponent<Health>();
    }

    void Update() {
        
    }
}