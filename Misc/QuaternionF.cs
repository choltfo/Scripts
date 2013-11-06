using UnityEngine;
using System.Collections;

public static class QuaternionF {

	public static Quaternion rotateX (this Quaternion v, float x) {
		v.Set(v.x+x, v.y, v.z, v.w);
		return v;
	}
	
	public static Quaternion rotateY (this Quaternion v, float y) {
		v.Set(v.x, v.y+y, v.z, v.w);
		return v;
	}
	
	public static Quaternion rotateZ (this Quaternion v, float z) {
		v.Set(v.x, v.y, v.z+z, v.w);
		return v;
	}
	
	public static Quaternion rotateW (this Quaternion v, float w) {
		v.Set(v.x, v.y, v.z, v.w+w);
		return v;
	}
}
