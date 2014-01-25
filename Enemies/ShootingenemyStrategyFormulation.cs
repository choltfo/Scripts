using UnityEngine;
using System.Collections;

[System.Serializable]
public class ThoughtProcess {
	public StateActionTrain Shot;
	public StateActionTrain Heard;
	public StateActionTrain Viewed;
	public StateActionTrain WhileAlerted;
}

[System.Serializable]
public class StateActionTrain {
	public Action[] ActionsToTake;
}

[System.Serializable]
public class Action	{
	public int fireNShots = 0;
	public NodeSelection Goto = NodeSelection.None;
	public ActionOrder order = ActionOrder.FireFirst;
}

[System.Serializable]
public enum ActionOrder {
	FireFirst,
	MoveFirst,
	JustMove,
	JustFire
}

[System.Serializable]
public enum NodeSelection {
	None,
	Closest,
	ClosestCover,
	ClosestStanding,
	ClosestCrouching,
	CrouchingNearestEnemy,
	StandingNeatestEnemy,
	CrouchingFurthestEnemy,
	StandingFurthestEnemy
}
