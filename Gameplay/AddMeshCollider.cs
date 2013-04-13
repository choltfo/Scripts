using UnityEngine;
using System.Collections;

public class AddMeshCollider : MonoBehaviour {
	
	public PhysicMaterial material;
	
	void  Start (){
		foreach(Transform child in transform) {
			if (child.gameObject.GetComponent("MeshFilter") != null) {
				child.gameObject.AddComponent("MeshCollider");
				MeshCollider collider = (MeshCollider)child.gameObject.GetComponent("MeshCollider");
				collider.sharedMesh = ((MeshFilter)(child.gameObject.GetComponent("MeshFilter"))).sharedMesh;
				collider.isTrigger = false;
				collider.material = material;
				collider.enabled = true;
			} else {
				foreach(Transform SubChild in child) {
					if (SubChild.gameObject.GetComponent("MeshFilter") != null) {
						SubChild.gameObject.AddComponent("MeshCollider");
						MeshCollider collider = (MeshCollider)SubChild.gameObject.GetComponent("MeshCollider");
						collider.sharedMesh = ((MeshFilter)(SubChild.gameObject.GetComponent("MeshFilter"))).sharedMesh;
						collider.isTrigger = false;
						collider.material = material;
						collider.enabled = true;
					}
				}
			}
		}
	}
}