using UnityEngine;
using System.Collections;

/// <summary>
/// Lists all the keyCodes and MouseButtons that are used by the player.
/// </summary>
public class Controls : MonoBehaviour {
	public int numOfControls		 = 16;
	
	public KeyCode jump              = KeyCode.Space;
	public KeyCode pause             = KeyCode.Escape;
	public KeyCode objectiveDetails  = KeyCode.Tab;
	public MouseButton aim           = MouseButton.RMB;
	public MouseButton fire          = MouseButton.LMB;
	public KeyCode minimap           = KeyCode.M;
	public KeyCode inventory         = KeyCode.I;
	public KeyCode reload            = KeyCode.R;
	public KeyCode interact          = KeyCode.E;
	public KeyCode drop              = KeyCode.Q;
	public MouseButton switchWeapons = MouseButton.CMB;
	public KeyCode weapon0           = KeyCode.Alpha1;
	public KeyCode weapon1           = KeyCode.Alpha2;
	public KeyCode melee             = KeyCode.F;
	public KeyCode flashlight        = KeyCode.C;
	public KeyCode laser       		 = KeyCode.V;
	public KeyCode grenade           = KeyCode.G;
}

/// <summary>
/// The mouse buttons in an easy to remember fashion.
/// </summary>
public enum MouseButton {
	LMB = 0,
	RMB = 1,
	CMB = 2
}