using UnityEngine;
using System.Collections;

public class PlaneCrashLand : MonoBehaviour {
	
	public Vehicle v;
	public TriggerableEvent TE;
	public SubtitleController SBTL;
	
	bool done = false;
	
	void OnCollisionEnter () {
		print ("Plane crashed, ejecting passenger.");
		if (done) return; 
		rigidbody.velocity = Vector3.zero;
		Destroy(rigidbody);
		v.deactivate();
		TE.Trigger(SBTL);
		done = true;
	}
}
