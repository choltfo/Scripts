using UnityEngine;
using System.Collections;

/// <summary>
/// Figures out the best node for the character to move to via the PFNode network.
/// Should not control the CharacterMotor directly, instead should be linked to an other class, e.g., enemy.
/// </summary>
public class PFNodeClient : MonoBehaviour {
	
	public static bool debugMode = false;
	
	public PFNode currentNode;
	
	public LayerMask LinecastMask;
	
	public float TeammateBias	 = 1.2f;
	public float EnemyBias		 = 1f;

	public bool decided = false;

	public PFNode choice = null;

	/// <summary>
	/// Gets the nearest node to the current node.
	/// </summary>
	/// <returns>
	/// The node nearest the current node.
	/// </returns>
	public PFNode getNodeNearest() {
		float distance = float.MaxValue;		// Should be safe.
		int index = -1;
		int i = 0;
		foreach (PFNodeEntry E in currentNode.Nodes) {
			if (E.distance < distance) {
				distance = E.distance;
				index = i;
			}
			i++;
		}
		choice=(index == -1) ? currentNode : currentNode.Nodes[index].node;
		return (index == -1) ? currentNode : currentNode.Nodes[index].node;
	}

	// This is heftily fucked up. Seriously needs fixing.
	public PFNode getNodeNearestCover () {
		//float nearestDistance = float.MaxValue;
		//PFNode nearestNode;
		
		if (currentNode.type == PFNodeType.Crouch || currentNode.type == PFNodeType.Stand) return currentNode;
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			if (node.node.type == PFNodeType.Stand || node.node.type == PFNodeType.Crouch) {
				// If node is cover
				Vector3 startPos = transform.position;
				Vector3.MoveTowards(startPos, node.node.transform.position, 1);
				if (!Physics.Linecast(startPos, node.node.transform.position)) {
					choice=node.node;
					return node.node;
				}
			}
		}
		
		return currentNode; // Untill we can have recursive search systems.
	}
	
	/// <summary>
	/// Gets the node closest from any enemy combatants, inclusive of current nodea.
	/// 
	/// FUTURE: Add calculations for safety based on effective range of the weapon they are holding.
	/// 	e.g., One is much safer ten meters away from a sniper than one half-kilometer away.
	/// </summary>
	/// <returns>
	/// The most dangerous node.
	/// </returns>
	public PFNode getNodeClosestToEnemies (GameObject[] enemies, Faction allegiance = Faction.Evil) {
		float leastDangerous = 0f;
		int index = -1;
		int i = 0;
		
		foreach (GameObject e in enemies) {
						// Change the != to whatever the faction relationship system is.
			if (e.GetComponent<Enemy>().faction != allegiance) leastDangerous +=
				(currentNode.transform.position- e.transform.position).sqrMagnitude;
		}
		if (debugMode) print ("Risk for " + currentNode.name + " is "+leastDangerous);
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			float riskFactor = 0;
			if (debugMode) foreach (GameObject g in enemies)print (g.name);
			foreach (GameObject e in enemies) {
				if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
					(node.node.transform.position - e.transform.position).sqrMagnitude;
				//if (debugMode) print ("Calculated for " + e.name + " near " + node.node.gameObject.name);
			}
			if (debugMode) print ("Risk for " + node.node.name + " is "+riskFactor);
			if (riskFactor < leastDangerous) {
				index = i;
				leastDangerous = riskFactor;
			}
			i++;
		}
		choice=(index == -1) ? currentNode : currentNode.Nodes[index].node;
		return (index == -1) ? currentNode : currentNode.Nodes[index].node;
	}
	
	/// <summary>
	/// Gets the node where one is most likely to be able to blow someone's brains out.
	/// Basically, the node with the most teammates, and the most enemeies nearby.
	/// </summary>
	/// <returns>
	/// The most dangerous node.
	/// </returns>
	public PFNode getNodeMostDangerous (GameObject[] enemies, Faction allegiance = Faction.Evil) {
		float leastDangerous = 0f;
		int index = -1;
		int i = 0;
		
		foreach (GameObject e in enemies) {
								  // Change the != to whatever the faction relationship system is.
			if (e.GetComponent<Enemy>().faction != allegiance) leastDangerous +=
				(currentNode.transform.position - e.transform.position).sqrMagnitude;
		}
		if (debugMode) print ("Risk for " + currentNode.name + " is "+leastDangerous);
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			float riskFactor = 0;
			if (debugMode) foreach (GameObject g in enemies)print (g.name);
			foreach (GameObject e in enemies) {
				
				
				// This is the fancy bit where you calculate where to run and hide.
				
				
				if (e.GetComponent<Enemy>() is ShootingEnemy) {
					//float thisCombatantsRisk;
					if (e != gameObject.GetComponent<Enemy>()) {
						
						if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
							(node.node.transform.position-e.transform.position).sqrMagnitude;
						
					}
					
				} else if (e.GetComponent<Enemy>() is PlayerCombatant) {
					
					if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
						(node.node.transform.position- e.transform.position).sqrMagnitude;
					
				} else {
					
					if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
						(node.node.transform.position- e.transform.position).sqrMagnitude;
					
				}
				//if (debugMode) print ("Calculated for " + e.name + " near " + node.node.gameObject.name);
				
			}
			if (debugMode) print ("Risk for " + node.node.name + " is "+riskFactor);
			if (riskFactor < leastDangerous) {
				index = i;
				leastDangerous = riskFactor;
			}
			i++;
		}

		choice=(index == -1) ? currentNode : currentNode.Nodes[index].node;
		return (index == -1) ? currentNode : currentNode.Nodes[index].node;
	}

	/*
	 * 
	 * Here's some insight as to how this is supposed to work.
	 * So, the GNNC system is updated every frame from the pathfinding enemy in question.
	 * It then contemplates one more node in the PFNC's current-node's connections list.
	 * When it has finished, the Pathfinding Enemy can then pull the new node data, and get moving.
	 * The reset function whould then be called to prevent things from going shit-sideways.
	 * 
	 * This system improves over the previous system in that we needn't worry about it causing lag in large nodes,
	 * 	only that our enemies will pause for an apprecciable fraction of a second while contemplating their next move.
	 * 
	 * Bear in mind, this will still return the current node if none are found, meaning that the enemy will just stand there,
	 * 	spamming through every option until hell freezes over, or it gets shot. This situation will need to be planned for.
	 * 
	 * 
	 * So far, as of 16/03/2014, the lag from idling enemies seems to be gone, but once their firing AI starts, things get hairy.
	 * 
	 */

	int GNNCi = 0;
	float GNNCSqDistNearest = float.MaxValue;
	int GNNCIndNearest = -1;
	int GNNCDec = -1;

	// Progress get node

	/// <summary>
	/// Progresses the Get Node Nearest Cover system.
	/// Should be called once a frame, maybe less is deltaTime gets high.
	/// </summary>
	public void GNNCProgress () {
		if (GNNCi < currentNode.Nodes.Length) {
			// Actual search stuff goes here.
			if (currentNode.Nodes[GNNCi].node.type != PFNodeType.Transit) {
				float sqDist = (currentNode.Nodes[GNNCi].node.transform.position- currentNode.transform.position).sqrMagnitude;
				if (sqDist < GNNCSqDistNearest) {
					GNNCSqDistNearest = sqDist;
					GNNCIndNearest = GNNCi;
				}
			}
		} else {
			GNNCDec = GNNCIndNearest;
		}
		GNNCi ++;
	}

	/// <summary>
	/// Resets the Get Node Nearest Cover system.
	/// Should be called upon moving of the character in question.
	/// </summary>
	public void GNNCReset () {
		GNNCi = 0;
		GNNCSqDistNearest = float.MaxValue;
		GNNCIndNearest = -1;
		GNNCDec = -1;
	}

	/// <summary>
	/// The result of the Node Nearest Cover system.
	/// </summary>
	/// <returns>The Node nearest cover, or the current node if the best option is yet unknown.</returns>
	public PFNode GNNCReturn () {
		return (GNNCDec == -1) ? currentNode : currentNode.Nodes[GNNCDec].node;
	}

	/// <summary>
	/// Returns wether a result has been reached through the Get Node Nearest Cover system.
	/// </summary>
	/// <returns><c>true</c>, if the Get Node Nearest Cover system has determined the best option, <c>false</c> otherwise.</returns>
	public bool GNNCFinished () {
		if (currentNode.Nodes.Length == null || GNNCi == null) return false;
		return GNNCi == currentNode.Nodes.Length;
	}
}





