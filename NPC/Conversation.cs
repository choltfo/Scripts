using UnityEngine;
using System.Collections;

[System.Serializable]
public class Conversation {
	public Option option;
	public bool done;
	
	[HideInInspector]
	public Option currentOption;
	
	public void Init() {
		option.Init();
		currentOption = option;
	}
	
	public void reset() {
		currentOption = option;
	}
}

[System.Serializable]
public class Option {
	
	public string name;
	
	public void Init() {
		foreach (Speech spc in speechs) {
			spc.Init();
		}
	}
	
	public OptionAction type;
	
	public Speech[] speechs;
	public bool possible;
	public bool returnToTop;
	public Option[] options;
	public TriggerableEvent TEvent;
	
}

/// <summary>
/// What the talk option does.
/// Used for restructuring the convo's flow.
/// </summary>
[System.Serializable]
public enum OptionAction {
	/// <summary>
	/// Opens up a new set of convo options.
	/// </summary>
	Normal,
	
	/// <summary>
	/// Returns to top of convo.
	/// </summary>
	ReturnToTop,
	
	/// <summary>
	/// Trigger the T Event, but leave the convo status unchanged.
	/// </summary>
	JustTrigger,
	
	/// <summary>
	/// Open the NPC's store. Will have to have an override built into the store.
	/// Does not work yet.
	/// </summary>
	OpenStore,

	/// <summary>
	/// Gives the interactee a weapon.
	/// Does not work yet.
	/// </summary>
	GiveWeapon,

	/// <summary>
	/// Trigger T Event, modify convo, and fsinish this conversation.
	/// </summary>
	Exit
}

