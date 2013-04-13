using UnityEngine;
using System.Collections;

public class HitObjects : MonoBehaviour {
	private GameObject[] Objects;
	void Update () {
		string Log = " ";
		//if (Input.GetButtonDown("Fire1")) {
			Objects = (GameObject[])(FindObjectsOfType (typeof(GameObject)));
			double Yang = Mathf.Tan(transform.rotation.y);
//			double Xang = Mathf.Tan(transform.rotation.x);
//			double Zang = Mathf.Tan(transform.rotation.z);
			for (int i = 0; i < Objects.Length; i++) {
				Transform curObject = (Transform)Objects[i].GetComponent("Transform");
					
				if (transform != curObject) {
					//if ((Xang * (curObject.position.z - transform.position.z) > ((curObject.position.y - transform.position.y) + 1.5)) && (Yang*(curObject.position.z - transform.position.z) < ((curObject.position.y - transform.position.y) - 1.5))){
					//	Log = Log + " " + "Hit " + Objects[i].name + " on ZY plane.";
					//}
					//if ((Yang * (transform.position.z - curObject.position.z) > ((transform.position.x - curObject.position.x) + 1.5)) && (Xang*(transform.position.z - curObject.position.z) < ((transform.position.x - curObject.position.x) - 1.5))){
					//	Log = Log + " " + "Hit " + Objects[i].name + " on XZ plane.";
					//}
					//if ((Mathf.Sqrt((transform.position.z - curObject.position.z) - (transform.position.x - curObject.position.x) - (transform.position.y - curObject.position.y))) > 0) {
					//	Log = Log + " Is positive!";
					//}
					
					if (Yang * (curObject.position.z - transform.position.z) > 
					((curObject.position.x - transform.position.x) + 1.5) &&
					(Yang*(curObject.position.z - transform.position.z) <
					((curObject.position.x - transform.position.x) - 1.5))) {
						Log = Log + " " + "Hit " + Objects[i].name + " on ZX plane.";
					print(Log);
					}
					Vector3 fwd = transform.TransformDirection(Vector3.forward);
					if (Physics.Raycast(transform.position, fwd, 10)) {
						print("There is something in front of the object!");
					} else {
						print ("");
					}
				}
			//}
		}
	}
}