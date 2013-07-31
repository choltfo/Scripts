using UnityEngine;
using System.Collections;

public class PFNode : MonoBehaviour {
	public PFNodeEntry[] Nodes;
	public PFNodeType type = PFNodeType.Transit;
	
	/// <summary>
	/// Checks all the connected nodes to allow for finding the best, and safest node.
	/// </summary>
	public void checkOptions(GameObject[] enemies) {
		foreach (PFNodeEntry e in Nodes) e.riskAssessment(enemies,this);
		
	}
}

[System.Serializable]
public class PFNodePathPoint {
	public PFNode node;
	public float time;
}

[System.Serializable]
public class PFNodeEntry {
	public PFNode node;
	public bool accessible;
	public float riskFactor = 0;
	
	public float riskAssessment (GameObject[] enemies, PFNode otherNode) {
		foreach (GameObject enemy in enemies) {
			riskFactor += Vector3.Distance(enemy.transform.position, node.transform.position);
			riskFactor += Vector3.Distance(enemy.transform.position, otherNode.transform.position);
		}
		riskFactor += Vector3.Distance(node.transform.position, otherNode.transform.position);
		return riskFactor;
	}
	
	public bool accessibilityAssessment (PFNode otherNode) {
		return accessible = !Physics.Linecast(node.transform.position, otherNode.transform.position);
	}
}

public enum PFNodeType {
	Transit,
	Stand,
	Crouch
}