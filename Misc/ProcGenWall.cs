using UnityEngine;
using System.Collections;

public class ProcGenWall : MonoBehaviour {
	public int X;
	public int Y;
	public float M;
	
	void Start() {
		for (int x = 0; x < X; x++) {
			for (int y = 0; y < Y; y++) {
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.GetComponent<Rigidbody>().mass = M;
				cube.transform.parent = transform;
				cube.transform.localPosition = new Vector3((float)((int)y%2==1 ? ((float)x *1.1) : ((float)(x+0.5)*1.1)), (float)(y*1.1), (float)0);
			}
		}
	}
}
