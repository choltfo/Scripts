using UnityEngine;
using System.Collections;
/// <summary>
/// Timed object destructor.
/// Unused.
/// </summary>
public class TimedObjectDestructor : MonoBehaviour {
	
	public float secondsToDestroy;
	public void Start () {
   		Destroy(gameObject, secondsToDestroy);
	}
	void Update () {
	
	}
}
