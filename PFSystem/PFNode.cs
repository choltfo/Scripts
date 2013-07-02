using UnityEngine;
using System.Collections;

public class PFNode : MonoBehaviour {
	public PFNodeEntry[] Nodes;
	public PFNodeType type = PFNodeType.Transit; 
}

public class PFNodeEntry {
	public PFNode node;
	public bool accessible;
	public float riskfactor = 0;
	
	public float riskAssessment (GameObject[] enemies, PFNode otherNode) {
		foreach (GameObject enemy in enemies) {
			riskFactor += Vector3.Distance(enemy.transform.postition, node.transform.position);
			riskFactor += Vector3.Distance(enemy.transform.postition, otherNode.transform.position);
		}
		riskFactor += Vector3.Distance(node.transform.postition, otherNode.transform.position);
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