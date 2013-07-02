using UnityEngine;
using System.Collections;

public class PFNode : MonoBehaviour {
	public PFNodeEntry[] Nodes;
	public PFNodeType type = PFNodeType.Transit; 
}

public class PFNodeEntry {
	public PFNode node;
	public bool accessible;
	
	public float riskAssessment (GameObject[] enemies) {
		
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