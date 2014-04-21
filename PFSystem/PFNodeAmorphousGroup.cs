using UnityEngine;
using System.Collections;


public class PFNodeAmorphousGroup : MonoBehaviour {
	public void SetupGroup() {
		PFNode[] PFNodes = GetComponentsInChildren<PFNode>();
		//print (PFNodes.Length);
		for (int i = 0; i < PFNodes.Length; i++) {
			PFNodes[i].Nodes = new PFNodeEntry[PFNodes.Length-1];
			for (int o = 0; o < PFNodes[i].Nodes.Length; o++) {
				PFNodes[i].Nodes[o] = new PFNodeEntry();
			}
			//print("Working at "+PFNodes[i].name+".");
			for (int o = 0; o < PFNodes.Length; o++) {
				//print("Attempting to connect "+i+" and "+o+".");
				PFNode n0 = PFNodes[o];
				if (o > i) {
					PFNodes[i].Nodes[o-1].node = PFNodes[o];
					PFNodes[i].Nodes[o-1].distance = Vector3.Distance(PFNodes[i].transform.position, PFNodes[o].transform.position);
				} else if (o < i) {
					PFNodes[i].Nodes[o].node = PFNodes[o];
					PFNodes[i].Nodes[o].distance = Vector3.Distance(PFNodes[i].transform.position, PFNodes[o].transform.position);
				}
			}
		}
	}
}
