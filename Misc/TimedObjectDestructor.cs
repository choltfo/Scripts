using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour {
	
	public float secondsToDestroy;
	public void Start () {
   		Destroy(gameObject, secondsToDestroy);
	}
	void Update () {
	
	}
}
