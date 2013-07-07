using UnityEngine;
using System.Collections;


/// <summary>
/// Figures out the best node for the character to move to via the PFNode network.
/// Should not control the CharacterMotor directly, instead should be linked to an other class, e.g., enemy.
/// </summary>
public class PFNodeClient : MonoBehaviour {
	
	public static bool debugMode = false;
	
	public PFNode currentNode;
	
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
			if (e.GetComponent<Enemy>().faction != allegiance) leastDangerous += Mathf.Pow(Vector3.Distance (currentNode.transform.position, e.transform.position), 2);
		}
		if (debugMode) print ("Risk for " + currentNode.name + " is "+leastDangerous);
		
		foreach (PFNodeEntry node in currentNode.Nodes) {
			float riskFactor = 0;
			foreach (GameObject e in enemies) {
				if (e.GetComponent<Enemy>().faction != allegiance) riskFactor += Mathf.Pow(Vector3.Distance (node.node.transform.position, e.transform.position), 2);
				if (debugMode) print ("Calculated for " + e.name + " near " + node.node.name);
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
