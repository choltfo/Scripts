private var existenceTimer : int = 0;


function Update() {
	if (existenceTimer == 0) {
		existenceTimer ++;
		return;
	}
	if (existenceTimer == 1) {
		transform.Rotate(90,0,0);
		existenceTimer ++;
		return;
	}
}
