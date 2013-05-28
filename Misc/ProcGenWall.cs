using UnityEngine;
using System.Collections;

public class ProcGenWall : MonoBehaviour {
	void Start() {
		for (int y = 0; y < 10; y++) {
			for (int x = 0; x < 10; x++) {
				//for (int z = 0; x < 10; z++) {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.AddComponent<Rigidbody>();
					cube.transform.parent = transform;
					cube.transform.localPosition = new Vector3((float)((int)y%2==1 ? (float)x : (float)x+0.5), (float)y, 0);
				//}
			}
		}
	}
}
