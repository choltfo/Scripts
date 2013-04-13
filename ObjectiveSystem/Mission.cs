using UnityEngine;
using System.Collections;

[System.Serializable]

public class Mission {
	
	public int currentObjecive = 0;
	public Objective[] objectives;
	public bool complete = false;
	//public Vector3 displayCoords;
	
	public bool updateObjectives() {
		/*bool notDone = false;
		foreach (Objective objective in objectives) {
			if (!objective.checkCompletion()) {
				notDone = true;
			}
		}
		if (!notDone) {
			complete = true;
			return true;
		} else {
			int lastCompleted = -1;
			for (int i = 0; i < objectives.Length; i ++) {
				if (objectives[i].complete) {
					lastCompleted = i;
				}
			}
			if (lastCompleted+1 == objectives.Length) {
				complete = true;
				return true;
			}
			//objectives[lastCompleted+1].Begin();
			
			if (objectives[lastCompleted+1].active == false) {
				objectives[lastCompleted+1].Activate();
			}
			if (objectives[lastCompleted] != null) {
				objectives[lastCompleted].Disactivate();
			}
			return false;
		}*/
		bool notDone = false;
		foreach (Objective objective in objectives) {
			if (!objective.checkCompletion()) {
				notDone = true;
			}
		}
		if (!notDone) {
			complete = true;
			return true;
		}
		
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i].Activate(i);
		}
		return false;
	}

	public bool checkCompletion() {
		foreach (Objective objective in objectives) {
			if (!objective.checkCompletion()) {
				return false;
			}
		}
		return true;
	}
	
	public void Begin () {
		//Debug.Log("STARTING MISSION");
		for (int i = 0; i < objectives.Length; i++) {
			objectives[i].Activate(i);
		}
	}
}
