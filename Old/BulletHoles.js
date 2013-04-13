#pragma strict

var newObject : Transform;


function Start () {

}

function Update () {
	if (Input.GetButtonDown("Fire1")) {
		Instantiate(newObject, transform.position, transform.rotation);
		Debug.Log("You just placed a "+newObject.name);
		//var p : Vector3 = camera.ScreenToWorldPoint (Vector3 (100,100,(Camera) this.nearClipPlane));
		//Debug.Log(p);
	}
}