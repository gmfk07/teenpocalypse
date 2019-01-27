using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro; //For game over text
using Random = UnityEngine.Random; //For randomizing game over screen image.

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
    public int StarveHealthDecrease = 8;
    public int StarveRelationshipDecrease = 5;
    public int RestHealthIncrease = 1;
    public int RestRelationshipIncrease = 1;
	[Range(0, Constants.MAX_VALUE)]
	public int TeamMorale = 50;
    public float DefenseMultiplier = 1;
    public int CharactersOnDefense = 0;

	public List<Action> AllActions;
	[HideInInspector] public List<Action> AvailableActions;

    public List<Event> AllEvents;
    [HideInInspector] public List<Event> AvailableEvents;

	public List<Character> StartingRoster;
	[HideInInspector] public List<Character> Roster;

	public delegate void OnWeekStart();
	public OnWeekStart Event_OnWeekStart;
	public delegate void OnCharacterRemoved(Character character);
	public OnCharacterRemoved Event_OnCharacterRemoved;
	public delegate void OnCharacterAdded(Character character);
	public OnCharacterAdded Event_OnCharacterAdded;

	public delegate void OnActionAdded(Action action);
	public OnActionAdded Event_OnActionAdded;
	public delegate void OnActionRemoved(Action action);
	public OnActionRemoved Event_OnActionRemoved;

	public Character SelectedCharacter;
	public Action SelectedAction;

    public DialogBoxController DialogBoxController;

    public TextMeshProUGUI currentWeek;

    //Game Over objects
    public TextMeshProUGUI weeksSurvived;
    public GameObject gameOverBackground;
    public Sprite[] GameOverSprites;
    private bool gameOver;

    //Sound Effects
    public AudioClip clickSound;
    public AudioClip clockTickSound;

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
		foreach (Character character in StartingRoster) {
			AddCharacter(character);
		}
        HideGameOver();
		LoadActions();
		LoadEvents();
		m_HitObjects = new List<RaycastResult>();
        currentWeek.text = "Week " + Week;
    }

	// Activated on Button Press
	public void NextWeek()
	{
        SoundManager.instance.PlaySingle(clockTickSound);
        int foodNeeded = FoodPerPerson * Roster.Count;
        if (foodNeeded >= Food)
        {
            List<Character> toBeRemoved = new List<Character>();
            foreach (Character character in Roster)
            {
				if (!character.ChangeHealth(-StarveHealthDecrease * ((foodNeeded-Food)/foodNeeded)) ||
                    !character.ChangeRelationship(-StarveRelationshipDecrease * ((foodNeeded-Food)/foodNeeded)))
					toBeRemoved.Add(character);
			}
			foreach (Character character in toBeRemoved)
			{
				RemoveCharacter(character);
			}
            Food = 0;
        } else {
            Food -= foodNeeded;
        }

        BuildingController bc = GetComponent<BuildingController>();

        if (bc.shelterAmount <= Roster.Count)
            TeamMorale -= Mathf.Max(Roster.Count - bc.shelterAmount, 0);

		foreach (Character character in Roster)
		{
			if (character.AssignedAction != null)
			{
				character.AssignedAction.Execute(character);
				character.AssignedAction = null;
			} else {
                character.ChangeHealth(RestHealthIncrease);
                character.ChangeRelationship(RestRelationshipIncrease);
            }
		}
        if (AvailableEvents.Count > 0)
        {
			DialogBoxController.ShowBox(AvailableEvents[UnityEngine.Random.Range(0, AvailableEvents.Count)]);
        }
        else
        {
            GameOver();
        }

        if(TeamMorale <= 0)
        { GameOver(); }

	}

	#region Incrementing and Modifying
	// Called when a new week is started
	public void IncrementWeek()
	{
		++Week;
        currentWeek.text = "Week " + Week;
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
	public void AddCharacter(Character character)
	{
		character.Init();
		Roster.Add(character);
		Event_OnCharacterAdded?.Invoke(character);
	}
	public void RemoveCharacter(Character character)
	{
		Roster.Remove(character);
		Event_OnCharacterRemoved(character);
	}
	public void AddAction(Action action)
	{
		action.Init();
		AvailableActions.Add(action);
		Event_OnActionAdded?.Invoke(action);
	}
	public void RemoveAction(Action action)
	{
		AvailableActions.Remove(action);
		Event_OnActionRemoved(action);
	}
	#endregion

    //Ends the game!
    public void GameOver()
    {
        gameOver = true;

        //Set the game over text specifying how long your player survived.
        if (Week == 1)
        {
            weeksSurvived.text = "You survived " + Week + " week.";
        }
        else
        {
            weeksSurvived.text = "You survived " + Week + " weeks.";
        }

        //Hide the normal player GUI and controls
        HideControls();

        //Set the Game Over Image with a Random fail message, and then show the image
        SetRandomGameOverMessage();
        gameOverBackground.SetActive(true);
        
    }

    public void HideGameOver()
    {
        gameOver = false;
        gameOverBackground.SetActive(false);
        weeksSurvived.text = "";
    }

    public void HideControls()
    {
        GameObject charPan = GameObject.Find("CharactersPanelContainer");
        GameObject gameActions = GameObject.Find("Actions");
        GameObject guiText = GameObject.Find("Text");
        GameObject nextWeekButton = GameObject.Find("Next Week Button");
        DialogBoxController eventGUI = GameObject.Find("GameController").GetComponent<DialogBoxController>();
        charPan.SetActive(false);
        gameActions.SetActive(false);
        guiText.SetActive(false);
        nextWeekButton.SetActive(false);
        eventGUI.enabled = false;
        currentWeek.text = "";
    }

    private void SetRandomGameOverMessage()
    {
        Debug.Log("Called SetRandomGameOverMessage");
        Image gameOverImage = gameOverBackground.GetComponent<Image>();
        gameOverImage.sprite = GameOverSprites[Random.Range(0, GameOverSprites.Length)];
    }


    //Returns true if morale test succeeds, false otherwise
    public bool TestMorale(int successModifier)
    {
        if (UnityEngine.Random.Range(0, 100 - successModifier) <= TeamMorale)
            return true;
        return false;
    }

    public bool TestDefense(float difficulty)
    {
        if (CharactersOnDefense * DefenseMultiplier >= difficulty)
            return true;
        return false;
    }
	#region Mouse Handling
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
		if (DialogBoxController.IsShowing)
			return null;

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

	#endregion

    public void ChangeFood(int delta)
    {
        Food = Mathf.Max(Food + delta, 0);
    }

    public void ChangeSupplies(int delta)
    {
        Supplies = Mathf.Max(Supplies + delta, 0);
    }
}