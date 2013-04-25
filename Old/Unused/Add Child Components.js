#pragma strict

public var Comp = "";

function Start () {
	for (var child : Transform in transform) {
		child.gameObject.AddComponent(Comp);
	}
}

function Update () {

}