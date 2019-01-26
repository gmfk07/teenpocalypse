using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Action : ScriptableObject
{
	public string Name;
	[TextArea(4, 20)] public string Description;
	//[Range(0,10)] public int ToolsNeeded;
	[Range(0, 10)] public int MinWeek;
	[Range(1, 3)] public int InitialSlots;

	// Runtime
	[HideInInspector] public List<Character> AssignedCharacters;
	[HideInInspector] public int Slots;

	public Action()
	{
	}

	public void Init()
	{
		AssignedCharacters = new List<Character>();
		Slots = InitialSlots;
	}

	public abstract void Execute(Character actionTaker);
}
