using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMotor))]

/// <summary>
/// Figures out the best node for the character to move to via the PFNode network.
/// Should not control the CharacterMotor directly, instead should be linked to an other class, e.g., enemy.
/// </summary>
public class PFNodeClient : MonoBehaviour {
	
	public float updatePeriod;
	
	public PFNode currentNode;
	
	/// <summary>
	/// Gets the nearest node to the current node.
	/// </summary>
	/// <returns>
	/// The node nearest the current node.
	/// </returns>
	public PFNode getNodeNearest() {
		float distance = float.MaxValue;	// Should be safe.
		int index = -1;
		int i = 0;
		foreach (PFNodeEntry E in currentNode.Nodes) {
			if (Vector3.Distance (E.node.transform.position, currentNode.transform.position) < distance) {
				distance = Vector3.Distance (E.node.transform.position, currentNode.transform.position);
				index = i;
			}
			i++;
		}
		return (index == -1) ? currentNode : currentNode.Nodes[index];
	}
	
	/// <summary>
	/// Gets the node furthest from any enemy combatants, inclusive of self.
	/// 
	/// FUTURE: Add calculations for safety based on effective range of the weapon they are holding.
	/// 	e.g., One is much safer ten meters away from a sniper than one half-kilometer away.
	/// </summary>
	/// <returns>
	/// The safest node.
	/// </returns>
	public PFNode getNodeSafest (PathfindingEnemy[] enemies) {
		
		foreach (PFNode node in currentNode) {
			float riskFactor = 0;
			foreach (PathfindingEnemy e in enemies) {
				riskFactor += Mathf.Pow(Vector3.Distance (node.transform.position, e.transform.position), 2);
			}
		}
		
		return currentNode;//Failsafe. Or something.
	}
}
