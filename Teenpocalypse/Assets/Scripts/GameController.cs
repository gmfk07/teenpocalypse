using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GameController : MonoBehaviour
{

	public static GameController Instance = null;
	// Data currently exposed for debugging purposes
	public int Week = 1;
	public int Supplies = 10;
	public int Food = 10;
	public int Tools = 0;
  public int Weapons = 0;
  public int FoodPerPerson = 2;
	[Range(0, Constants.MAX_VALUE)]
	public int TeamMorale = 50;

	public List<Action> AllActions;
	[HideInInspector] public List<Action> AvailableActions;

    public List<Event> AllEvents;
    [HideInInspector] public List<Event> AvailableEvents;

	public List<Character> StartingRoster;
	[HideInInspector] public List<Character> Roster;

	public delegate void OnWeekStart();
	public OnWeekStart Event_OnWeekStart;

	public Character SelectedCharacter;
	public Action SelectedAction;

    public DialogBoxController dialogBoxController;

	List<RaycastResult> m_HitObjects;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		InitGame();
	}
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnMouseClicked();
		}
		if (Input.GetMouseButtonUp(0))
		{
			OnMouseReleased();
		}
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
		m_HitObjects = new List<RaycastResult>();
	}

	// Activated on Button Press
	public void NextWeek()
	{
        int foodNeeded = FoodPerPerson * Roster.Count;
        if (foodNeeded >= Food)
        {
            foreach (Character character in Roster)
            {
                character.Health -= foodNeeded - Food;
            }
            Food = 0;
        } else {
            Food -= foodNeeded;
        }

		foreach (Character character in Roster)
		{
			if (character.AssignedAction != null)
			{
				character.AssignedAction.Execute(character);
				character.AssignedAction = null;
			}
		}

        dialogBoxController.ShowBox(AvailableEvents[UnityEngine.Random.Range(0, AvailableEvents.Count)]);
	}

	#region Incrementing and Modifying
	// Called when a new week is started
	public void IncrementWeek()
	{
		++Week;
		LoadActions();
        LoadEvents();
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

    void LoadEvents()
    {
        // Adding new events
        foreach (Event e in AllEvents)
        {
            if (e.MinWeek <= Week)
            {
                if (!AvailableEvents.Contains(e))
                    AvailableEvents.Add(e);
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

	void OnMouseClicked()
	{
		List<GameObject> clickedOnObjects = ClickAndGetResults();

		CharacterPanel characterPanel = FindFirstOf<CharacterPanel>(clickedOnObjects);
		if (characterPanel)
		{
			SelectedCharacter = characterPanel.character;
		}
		ActionPanel actionPanel = FindFirstOf<ActionPanel>(clickedOnObjects);
		if (actionPanel)
		{
			SelectedAction = actionPanel.action;
		}
	}

	private void OnMouseReleased()
	{
		List<GameObject> clickedOnObjects = ClickAndGetResults();

		CharacterPanel characterPanel = FindFirstOf<CharacterPanel>(clickedOnObjects);
		if (characterPanel)
		{
			if (SelectedAction)
			{
				characterPanel.character.AssignedAction = SelectedAction;
			}
		}
		ActionPanel actionPanel = FindFirstOf<ActionPanel>(clickedOnObjects);
		if (actionPanel)
		{
			
		}

		SelectedAction = null;
		SelectedCharacter = null;
	}

	List<GameObject> ClickAndGetResults()
	{
		var pointer = new PointerEventData(EventSystem.current);
		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointer, m_HitObjects);

		if (m_HitObjects.Count <= 0) return null;
		List<GameObject> gameObjects = new List<GameObject>();
		foreach (var item in m_HitObjects)
		{
			gameObjects.Add(item.gameObject);
		}
		return gameObjects;
	}

	T FindFirstOf<T>(List<GameObject> gameObjects)
	{
		if (gameObjects == null) return default(T);
		foreach (GameObject go in gameObjects)
		{
			T component = go.GetComponent<T>();
			if (component != null)
				return component;
		}
		return default(T);
	}
}
