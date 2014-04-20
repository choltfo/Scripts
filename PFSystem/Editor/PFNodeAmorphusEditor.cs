using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PFNodeAmorphousGroup))]
public class PFNodeAmorphusEditor : Editor {
	public override void OnInspectorGUI() {
		if (GUILayout.Button("Populate amorphus PFN group.")) {
			((PFNodeAmorphousGroup)target).SetupGroup();
			EditorUtility.SetDirty (target);
		}

	}
	public void OnSceneGUI () {
		PFNode[] PFNs = ((PFNodeAmorphousGroup)target).transform.gameObject.GetComponentsInChildren<PFNode>();
		for (int i = 0; i<PFNs.Length; i++) {
			PFNode p = PFNs[i];
			p.transform.position = Handles.PositionHandle (p.transform.position, p.transform.rotation);
			//foreach (PFNodeEntry e in p.Nodes) {
			//	Handles.DrawLine(e.node.transform.position, p.transform.position);
			//}
			Handles.Label(p.transform.position,"Node "+ i.ToString());
		}
		if (GUI.changed)
			EditorUtility.SetDirty (target);
	}
}
