using UnityEngine;
using System.Collections;

/// <summary>
/// Figures out the best node for the character to move to via the PFNode network.
/// Should not control the CharacterMotor directly, instead should be linked to an other class, e.g., enemy.
/// </summary>
public class PFNodeClient : MonoBehaviour {
	
	public static bool debugMode = false;
	
	public PFNode currentNode;
	
	
	public float TeammateBias	 = 1.2f;
	public float EnemyBias		 = 1f;
	
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
			if (Vector3.Distance (E.node.transform.position, currentNode.transform.position) < distance) {
				distance = Vector3.Distance (E.node.transform.position, currentNode.transform.position);
				index = i;
			}
			i++;
		}
		return (index == -1) ? currentNode : currentNode.Nodes[index].node;
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
				Mathf.Pow(Vector3.Distance (currentNode.transform.position, e.transform.position), 2);
		}
		if (debugMode) print ("Risk for " + currentNode.name + " is "+leastDangerous);
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			float riskFactor = 0;
			if (debugMode) foreach (GameObject g in enemies)print (g.name);
			foreach (GameObject e in enemies) {
				if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
					Mathf.Pow(Vector3.Distance (node.node.transform.position, e.transform.position), 2);
				//if (debugMode) print ("Calculated for " + e.name + " near " + node.node.gameObject.name);
			}
			if (debugMode) print ("Risk for " + node.node.name + " is "+riskFactor);
			if (riskFactor < leastDangerous) {
				index = i;
				leastDangerous = riskFactor;
			}
			i++;
		}
		
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
				Mathf.Pow(Vector3.Distance (currentNode.transform.position, e.transform.position), 2);
		}
		if (debugMode) print ("Risk for " + currentNode.name + " is "+leastDangerous);
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			float riskFactor = 0;
			if (debugMode) foreach (GameObject g in enemies)print (g.name);
			foreach (GameObject e in enemies) {
				
				
				// This is the fancy bit where you calculate where to run and hide.
				
				
				if (e.GetComponent<Enemy>() is ShootingEnemy) {
					float thisCombatantsRisk;
					if (e != gameObject.GetComponent<Enemy>()) {
						
						if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
							Mathf.Pow(Vector3.Distance (node.node.transform.position, e.transform.position), 2);
						
					}
					
				} else if (e.GetComponent<Enemy>() is PlayerCombatant) {
					
					if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
						Mathf.Pow(Vector3.Distance (node.node.transform.position, e.transform.position), 2);
					
				} else {
					
					if (e.GetComponent<Enemy>().faction != allegiance) riskFactor +=
						Mathf.Pow(Vector3.Distance (node.node.transform.position, e.transform.position), 2);
					
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
		
		return (index == -1) ? currentNode : currentNode.Nodes[index].node;
	}
}
