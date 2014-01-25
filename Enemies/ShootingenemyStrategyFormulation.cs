using UnityEngine;
using System.Collections;

public class ShootingenemyStrategyFormulation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class ThoughtProcess {



}

public class StateActionTrain {
	public State S = State.Heard;
	public Action[] A = {Action.Burst, Action.Cover};
}

public enum State {
	Shot,
	Heard,
	Viewed
}

public enum Action {
	OpenFire,
	Burst,
	Cover
}

public enum PreCoverShot {
	ButstToCover
}

public enum NodeSelection {
	Closest,
	ClosestCover,
	ClosestStanding,
	ClosestCrouching,
	CrouchingNearestEnemy,
	StandingNeatestEnemy,
	CrouchingFurthestEnemy,
	StandingFurthestEnemy

}
