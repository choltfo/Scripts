using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PFNodeClient))]
[RequireComponent(typeof(CharacterController))]

public class PathfindingEnemy : Enemy {
	
	public float updateInterval = 1f;
	
	float lastUpdate = -1f;
	public float speed = 5;
	
	//[HideInInspector]
	public PFNodeClient PFNC;
	//[HideInInspector]
	public CharacterController CC;
	
	public bool ready = true;
	
	public Enemy target;
	
	
	public PFNode[] patrol;
	
	
	public bool alerted = false;
	
	public float fieldOfViewRadiusInDegrees = 30;
	
	public float visionRange = 10;
	
	public static bool debug = true;
	
	public static List<Enemy> targets;
	
	int patrolIndex = 0;
	
	
	// Use this for initialization
	void Start () {
		PFNC = GetComponent<PFNodeClient>();
		CC	 = GetComponent<CharacterController>();
		
		//targetNode = PFNC.currentNode;
		
		childStart();
	}
	
	public static void setTargets(List<Enemy> Es) {
		print ("setTargets called");
		targets = Es;
	}
	
	public void setTarget (Enemy e) {
		alerted = true;
		target = e;
		if (debug) print ("Shot by "+e.name+", retaliating.");
	}
	
	public void checkAnyVisible () {
		foreach (Enemy e in targets) {
			if (e.faction != faction) {
				if (debug) print ("Checking to see if alerted by " +  e.name);
				var rayDirection = e.transform.position - transform.position;
				if (Vector3.Angle(rayDirection, transform.forward) < fieldOfViewRadiusInDegrees) {
					if (debug) print ("Angle satisfied.");
					if (Vector3.Distance(transform.position, e.transform.position) < visionRange) {
						if (debug) print ("Distance satisfied, performing raycast.");
						Vector3 relAng =
							transform.InverseTransformDirection(e.transform.position - transform.position);
						relAng.Normalize();
						Ray ray = new Ray(transform.position, relAng);
						Debug.DrawRay(transform.position, relAng, new Color(255,0,0));
						RaycastHit hit;
						collider.Raycast(ray, out hit, visionRange);
						if (hit.collider != null) {
							if (debug) print ("Hit something, namely " + hit.collider.name);
							if (hit.transform == e.transform) {
								alerted = true;
								target = e;
								if (debug) print ("Found target. Starting to kill!");
							} else {
								if (debug) print ("Hit " + hit.collider.name);
							}
						} else {
							if (debug) print ("Did not hit anything, let alone the target.");
						}
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		ready = ((int)transform.position.z == (int)getZXPosition(PFNC.currentNode.transform.position).z &&
			(int)transform.position.x == (int)getZXPosition(PFNC.currentNode.transform.position).x);
			
		
		// DEBUG: print (transform.position.ToString() + " : " + getZXPosition(PFNC.currentNode.transform.position));
		
		if (alerted) {
			if (Time.time > lastUpdate + updateInterval && Vector3.Distance(transform.position, PFNC.currentNode.transform.position) < 1) {
				lastUpdate = Time.time;
				
				// Change this to whatever the best method is.
				// TODO: change to getSafest, once that works.
				//PFNC.currentNode = PFNC.getNodeNearest();											// This should be the same for all
																									// Enemies.
				PFNC.currentNode = PFNC.getNodeClosestToEnemies(GameObject.FindGameObjectsWithTag("Combatant"), faction);
				
				ready = false;
			}
		} else {
			// If not alerted....
			if (patrol.Length != 0) {
				if (ready) patrolIndex = (patrolIndex+1) % patrol.Length;
				PFNC.currentNode = patrol[patrolIndex];
			}
		}
		
		Vector3 target = getRelativePosition(transform, PFNC.currentNode.transform.position);
		CC.SimpleMove (target * speed);
		
		childFixedUpdate();
	}
	
	public virtual void childFixedUpdate() {
		return;
	}
	
	public virtual void childStart() {
		Debug.Log ("childStart - PFEnemy");
		return;
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
	
	/// <summary>
	/// Lists the enemies.
	/// </summary>
	public static List<Enemy> listEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Combatant");
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		List<Enemy> targets = new List<Enemy>();
		int i = 0;
		foreach (GameObject go in enemies) {
			targets.Add(go.GetComponent<Enemy>());
			i++;
		}
		
		i = 0;
		foreach (GameObject go in players) {
			targets.Add(go.GetComponent<Enemy>());
			i++;
		}
		
		if (debug) print(enemies.Length);
		return targets;
	}
}
