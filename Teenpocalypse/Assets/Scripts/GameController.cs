using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

	public static GameController Instance = null;
	public int Week = 1;
	public int Wood = 10;
	public int Stone = 0;
	public int Food = 10;
	public int Tools = 0;

	public List<Action> AllActions;
	[HideInInspector] public List<Action> AvailableActions;

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

	}

	// Called when a new week is started
	void StartWeek()
	{
		++Week;
		foreach (Action action in AllActions)
		{
			if (action.MinWeek == Week)
			{
				AvailableActions.Add(action);
			}
		}
	}
}
