using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

	public static GameController Instance = null;
	// Data currently exposed for debugging purposes
	public int Week = 1;
	public int Wood = 10;
	public int Stone = 0;
	public int Food = 10;
	public int Tools = 0;
	[Range(0, Constants.MAX_VALUE)]
	public int TeamMorale = 50;

	public List<Action> AllActions;
	[HideInInspector] public List<Action> AvailableActions;

	public List<Character> StartingRoster;
	[HideInInspector] public List<Character> Roster;

	public delegate void OnWeekStart();
	public OnWeekStart Event_OnWeekStart;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		foreach (Action action in AllActions) {
			action.Init();
		}
		foreach (Character character in StartingRoster)
		{
			AddCharacter(character);
		}
		LoadActions();
	}

	// Activated on Button Press
	public void NextWeek()
	{
		IncrementWeek();
	}

	#region Incrementing and Modifying
	// Called when a new week is started
	void IncrementWeek()
	{
		++Week;
		LoadActions();
	}
	void LoadActions()
	{
		// Adding new actions
		foreach (Action action in AllActions)
		{
			if (action.MinWeek <= Week)
			{
				if (!AvailableActions.Contains(action))
					AvailableActions.Add(action);
			}
		}
	}

	void IncrementTools()
	{
		++Tools;
		//// Adding new actions
		//foreach (Action action in AllActions)
		//{
		//	if (action.MinWeek >= Week && Tools == action.ToolsNeeded)
		//	{
		//		AvailableActions.Add(action);
		//	}
		//}
	}
	void AddCharacter(Character character)
	{
		character.Init();
		Roster.Add(character);
	}
	void RemoveCharacter(Character character)
	{
		Roster.Remove(character);
	}
	#endregion
}
