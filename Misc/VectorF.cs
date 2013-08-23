using UnityEngine;
using System.Collections;

// Written mostly by WPennyPacker on the unity forums.
// Thanks a lot!
public static class VectorF {
	  public static Vector3 RotateX( this Vector3 v, float angle )

    {

        float sin = Mathf.Sin( angle );

        float cos = Mathf.Cos( angle );

        

        float ty = v.y;

        float tz = v.z;

        v.y = (cos * ty) - (sin * tz);

        v.z = (cos * tz) + (sin * ty);
		return v;
    }

    

    public static Vector3 RotateY( this Vector3 v, float angle ) {

        float sin = Mathf.Sin( angle );

        float cos = Mathf.Cos( angle );

        

        float tx = v.x;

        float tz = v.z;

        v.x = (cos * tx) + (sin * tz);

        v.z = (cos * tz) - (sin * tx);
		return v;
    }

 

    public static Vector3 RotateZ( this Vector3 v, float angle )

    {

        float sin = Mathf.Sin( angle );

        float cos = Mathf.Cos( angle );

        

        float tx = v.x;

        float ty = v.y;

        v.x = (cos * tx) - (sin * ty);

        v.y = (cos * ty) + (sin * tx);
		return v;
    }
	
	public static float GetPitch( this Vector3 v )

    {

        float len = Mathf.Sqrt( (v.x * v.x) + (v.z * v.z) );    // Length on xz plane.

        return( -Mathf.Atan2( v.y, len ) );
    }

        

    public static float GetYaw( this Vector3 v )

    {

        return( Mathf.Atan2( v.x, v.z ) );

    }
	
	
	public static Vector3 moveAlongX (this Vector3 v, float x) {
		v.Set(v.x+x, v.y, v.z);
		return v;
	}
	
	public static Vector3 moveAlongY (this Vector3 v, float y) {
		v.Set(v.x, v.y+y, v.z);
		return v;
	}
	
	public static Vector3 moveAlongZ (this Vector3 v, float z) {
		v.Set(v.x, v.y, v.z+z);
		return v;
	}
}
