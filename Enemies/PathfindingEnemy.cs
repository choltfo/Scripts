using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PFNodeClient))]
[RequireComponent(typeof(CharacterController))]

public class PathfindingEnemy : MonoBehaviour {
	
	public float updateInterval = 1f;
	//[HideInInspector]
	//public PFNode targetNode;
	
	float lastUpdate = -1f;
	
	PFNodeClient PFNC;
	CharacterController CC;
	
	
	// Use this for initialization
	void Start () {
		PFNC = GetComponent<PFNodeClient>();
		CC	 = GetComponent<CharacterController>();
		
		//targetNode = PFNC.currentNode;
	}
	
	// Update is called once per frame
	void Update () {
		
		CC.Move(getRelativePosition(transform, PFNC.currentNode.transform.position));
		
		if (Time.time > lastUpdate + updateInterval && Vector3.Distance(transform.position, PFNC.currentNode.transform.position) < 1) {
			lastUpdate = Time.time;
			PFNC.currentNode = PFNC.getNodeNearest();
		}
	}
	
	public static Vector3 getRelativePosition(Transform origin, Vector3 position) {
		Vector3 distance = position - origin.position;
		Vector3 relativePosition = Vector3.zero;
		relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
		relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
		relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
		return relativePosition;
	}
}
