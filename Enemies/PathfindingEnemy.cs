using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PFNodeClient))]
[RequireComponent(typeof(CharacterController))]

public class PathfindingEnemy : Enemy {
	/// <summary>
	/// Whether this AI should bother using NodePF.
	/// Disable for stationary enemies.
	/// </summary>
	public bool USEPF = true;
	
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



	public static bool debug = false;
	public static bool timing = false;

	public static List<Enemy> targets;



	int patrolIndex = 0;
	
	
	float lastTargetCheck = 0f;
	
	public float targetCheckDelay = 0.5f; // The amount of time you have to remain within view for the enemy to notice you.
	
	
	public AlertingMethod AlertMethod = AlertingMethod.NONE;	// Determines the AI action immediately thereafter.
	
	
	
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
	
	public void setTarget (Enemy e, AlertingMethod M) {
		alerted = true;
		target = e;
		if (debug) print ("Shot by "+e.name+", retaliating.");

		if (AlertMethod == AlertingMethod.Shot ||
			AlertMethod == AlertingMethod.Hear && USEPF) {
			PFNC.currentNode = PFNC.GNNCReturn();
		}
	}
	
	bool checkEnemyVisible (Enemy e) {
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
					Physics.Raycast(ray, out hit, visionRange);
					if (hit.collider != null) {
						if (debug) print ("Hit something, namely " + hit.collider.name);
						if (hit.transform == e.transform) {
							// Can see the target.
							
							return true;
							
							
							
							
						} else {
							if (debug) print ("Hit " + hit.collider.name);
						}
					} else {
						if (debug) print ("Did not hit anything, let alone the target.");
					}
				}
			}
		}
		return false;
	}
	
	public void checkAnyVisible () {
		foreach (Enemy e in targets) {
			if (e is VehicleEnemyPlaceholder) {
				if (checkEnemyVisible(e)) {
					setTarget((e as VehicleEnemyPlaceholder).enemy, AlertingMethod.See);
					lastTargetCheck = Time.time;
				}
			} else {
				if (checkEnemyVisible(e)) {
					setTarget(e,AlertingMethod.See);
					lastTargetCheck = Time.time;
				}
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!PFNC.GNNCFinished() && USEPF) PFNC.GNNCProgress();

		PFNC.currentNode = PFNC.GNNCReturn(); 	// NOTE: This may screw things up further down the line.

		if (Time.time - lastTargetCheck > targetCheckDelay) {
			checkTarget();

			if (!alerted) {
				checkAnyVisible();
			} else {
				if (USEPF && PFNC.GNNCFinished()) PFNC.currentNode = PFNC.getNodeNearestCover();
			}

			lastTargetCheck = Time.time;
		}
		
		// IF in position, we are ready.
		
		if (USEPF && PFNC.GNNCFinished()) {
			ready = ((int)transform.position.z == (int)getZXPosition(PFNC.currentNode.transform.position).z &&
				(int)transform.position.x == (int)getZXPosition(PFNC.currentNode.transform.position).x);
		} else {
			ready = true;
		}
			
		
		// DEBUG: print (transform.position.ToString() + " : " + getZXPosition(PFNC.currentNode.transform.position));
		
		if (alerted) {
			// If alerted, and time has passed, and we are near the target node
			if (USEPF && PFNC.GNNCFinished()) {
				if (Time.time > lastUpdate + updateInterval && Vector3.Distance(transform.position, PFNC.currentNode.transform.position) < 1) {
					
					lastUpdate = Time.time;
					
					// Change this to whatever the best method is.
					// TODO: change to getSafest, once that works.
					//PFNC.currentNode = PFNC.getNodeNearest();											// This should be the same for all
																										// Enemies.
					//if (USEPF) PFNC.currentNode = PFNC.getNodeClosestToEnemies(GameObject.FindGameObjectsWithTag("Combatant"), faction);
					
					ready = false;
				}
			}
		} else {
			// If not alerted and on a patrol
			if (patrol.Length != 0) {
				if (ready) patrolIndex = (patrolIndex+1) % patrol.Length;
				PFNC.currentNode = patrol[patrolIndex];
				float heading  = (CC.velocity.sqrMagnitude > 0 ? Quaternion.LookRotation(CC.velocity, Vector3.up).eulerAngles.y : 0);
				transform.eulerAngles.Set(transform.eulerAngles.x,heading,transform.eulerAngles.z);
			}
		}
		
		if (USEPF && PFNC.GNNCFinished()) {

			Vector3 target = getRelativePosition(transform, PFNC.currentNode.transform.position);
			target.Normalize();
			CC.SimpleMove (transform.InverseTransformDirection(target) * speed);

		}
		
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
	
	public void checkTarget() {
		if (target == null) {
			return;
		}
			
		if (!target.gameObject.activeInHierarchy) {
			alerted = false;
			return;
		}
	}
	
	public Vector3 getZXPosition (Vector3 pos) {
		pos.Set (pos.x, transform.position.y, pos.z);
		return pos;
	}
	
	/// <summary>
	/// Lists all the enemies in the scene.
	/// </summary>
	public static List<Enemy> listEnemies() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Combatant");
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		Object[] vehicles = Object.FindObjectsOfType(typeof(VehicleEnemyPlaceholder));
		List<Enemy> targets = new List<Enemy>();
		int i = 0;
		foreach (GameObject go in enemies) {
			targets.Add(go.GetComponent<Enemy>());
			i++;
		}
		
		foreach (Object v in vehicles) {
			if (((VehicleEnemyPlaceholder)v).enemy != null) {
				targets.Add((v as VehicleEnemyPlaceholder).gameObject.GetComponent<VehicleEnemyPlaceholder>());
			}
		}
		
		i = 0;
		foreach (GameObject go in players) {
			targets.Add(go.GetComponent<Enemy>());
			i++;
		}
		
		//if (debug) print(enemies.Length);
		return targets;
	}

	public static void hearNoise (Enemy e, float detectionRange) {
		
		foreach (Enemy r in listEnemies()) {
			
			if (r == e) break;
			
			if (!(r is PathfindingEnemy)) break;
			
			if (((PathfindingEnemy)r).alerted) break;
			
			if (r.faction != e.faction) {
				if (Vector3.Distance(r.transform.position, e.transform.position) < detectionRange) {
					((PathfindingEnemy)r).setTarget(e, AlertingMethod.Hear);
				}
			}
		}
	}
}


// Used to determine initial action upon being alerted. 
public enum AlertingMethod {
	NONE,
	See,
	Hear,
	Shot
}
