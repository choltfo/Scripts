using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PFNodeClient))]
[RequireComponent(typeof(CharacterController))]

public class PathfindingEnemy : Enemy {
	
	public float updateInterval = 1f;
	
	float lastUpdate = -1f;
	public float speed = 5;
	
	PFNodeClient PFNC;
	CharacterController CC;
	
	public bool ready = true;

	
	// Use this for initialization
	void Start () {
		PFNC = GetComponent<PFNodeClient>();
		CC	 = GetComponent<CharacterController>();
		
		//targetNode = PFNC.currentNode;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if ((int)transform.position.z == (int)getZXPosition(PFNC.currentNode.transform.position).z &&
			(int)transform.position.x == (int)getZXPosition(PFNC.currentNode.transform.position).x) {
			ready = true;
			//print ("In position, readying up.");
		}
		
		// DEBUG: print (transform.position.ToString() + " : " + getZXPosition(PFNC.currentNode.transform.position));
		
		Vector3 target = getRelativePosition(transform, PFNC.currentNode.transform.position);
		CC.SimpleMove (target * speed);
		
		if (Time.time > lastUpdate + updateInterval && Vector3.Distance(transform.position, PFNC.currentNode.transform.position) < 1) {
			lastUpdate = Time.time;
			
			// Change this to whatever the best method is.
			// TODO: change to getSafest, once that works.
			//PFNC.currentNode = PFNC.getNodeNearest();								// This should be the same for all
																					// Enemies.
			PFNC.currentNode = PFNC.getNodeSafest(GameObject.FindGameObjectsWithTag("Combatant"), faction);
			
			ready = false;
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
	
	public Vector3 getZXPosition (Vector3 pos) {
		pos.Set (pos.x, transform.position.y, pos.z);
		return pos;
	}
}
