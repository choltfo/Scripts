using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {
	
	public static int lastUID = -1;
	
	public static int generateUID () {
		lastUID ++;
		return lastUID;
	}
	
	[HideInInspector]
	public int UID;
	public string storeName;
	public Inventory inventory;
	public Inventory playerInv;
	public Pause pauseController;
	
	public void Interact (ShootObjects player) {
		playerInv = player.inventory;
		Time.timeScale = 0;
		pauseController.pane = "/Store/"+UID.ToString();
	}
	
	void OnGUI () {
		if (Time.timeScale == 0) {
			if (pauseController.pane == "/Store/"+UID.ToString()) {
				
			}
		}
	}
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
