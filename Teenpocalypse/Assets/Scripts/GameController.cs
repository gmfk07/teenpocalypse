using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using TMPro; //For game over text
using Random = UnityEngine.Random; //For randomizing game over screen image.

public class GameController : MonoBehaviour
{
    public DayNightFilter dayNightFilter;

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

    public List<Character> AvailableCharacters;
    private List<Character> WillAddListCharacters = new List<Character>();

    public List<Character> OnDefense = new List<Character>();

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
    public GameObject playAgainButton;
    public GameObject gameOverBackground;
    public Sprite[] GameOverSprites;
    private bool gameOver;

    //Sound Effects
    public AudioClip clickSound;
    public AudioClip clockTickSound;
    public AudioClip gameOverSound;
    public AudioClip newActionAvailableSound;
    public AudioClip buildingSound;
    public AudioClip timePassingSound;

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
	public void NextWeek() { StartCoroutine(TheTimesTheyAreAChangin()); }

    private bool dialogBoxGone = false;
    public void DialogBoxGone() { dialogBoxGone = true; }

    private IEnumerator TheTimesTheyAreAChangin() {
        SoundManager.instance.PlaySingle(clockTickSound);

        float randN = Random.Range(0f, 1f);

        int numEvents = randN < .5f ? 1 : (randN < .85f ? 2 : 3);
        int eventsLeft = numEvents;
        List<Event> weekEvents = new List<Event>(AvailableEvents);

        const float timeScale = 1;
        float timeLeft = 7;
        bool eaten = false;
        bool worked = false;
        bool slept = false;
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime * timeScale;
            float hour = (timeLeft - Mathf.Floor(timeLeft)) * 24;
            dayNightFilter.nightness = Mathf.Sin(hour / 12 * Mathf.PI);
            

            if(hour < 1) { //reset for new day
                eaten = false;
                worked = false;
                slept = false;
            }

            if(hour > 12 && !eaten) {
                eaten = true;
                EatFood();
            }
            if(hour > 14 && !worked) {
                worked = true;
                DoActions();
            }
            if(hour > 20 && !slept) {
                slept = true;
                Sleep();
            }

            if(timeLeft < 7f / numEvents * 0.8f && eventsLeft == 1 ||
               timeLeft < 7f / numEvents * 1.5f  && eventsLeft == 2 ||
               timeLeft < 7f / numEvents * 2.8f  && eventsLeft == 3) {
                PickEvent(weekEvents);
                eventsLeft--;
                yield return new WaitUntil(() => dialogBoxGone);
                dialogBoxGone = false;
            }


            yield return null;
            currentWeek.text = $"Week {Week} Day {1 + Mathf.FloorToInt(7 - timeLeft)}";
        }
        currentWeek.text = "Week " + Week;

        if(TeamMorale <= 0) { GameOver(); }
    }

    private void PickEvent(List<Event> weekEvents) {
        if(AvailableEvents.Count > 0) {
            Event e = AvailableEvents[UnityEngine.Random.Range(0, AvailableEvents.Count)];
            e.Chosen();
            DialogBoxController.ShowBox(e);
            weekEvents.Remove(e);
            if(e.isRitual)
                AvailableEvents.Remove(e);
        } else {
            GameOver();
        }
    }

    private void DoActions() {
            // Execute All Assigned Actions
            foreach (Character character in Roster)
            {
                if (character.AssignedAction != null)
                {
					character.AssignedAction.AssignedCharacters.Remove(character);
					character.AssignedAction.Execute(character);
                    character.AssignedAction = null;
                }
                else
                {
                    character.ChangeHealth(RestHealthIncrease);
                    character.ChangeRelationship(RestRelationshipIncrease);
                }
            }
    }

    private void Sleep() {
        BuildingController bc = GetComponent<BuildingController>();

        if(bc.shelterAmount <= Roster.Count)
            TeamMorale -= Mathf.Max(Roster.Count - bc.shelterAmount, 0);
    }

    private void EatFood() {
            int foodNeeded = FoodPerPerson * Roster.Count;
            if (foodNeeded >= Food)
            {
                List<Character> toBeRemoved = new List<Character>();
                foreach (Character character in Roster)
                {
                    if (!character.ChangeHealth(-StarveHealthDecrease * ((foodNeeded - Food) / foodNeeded)) ||
                        !character.ChangeRelationship(-StarveRelationshipDecrease * ((foodNeeded - Food) / foodNeeded)))
                        toBeRemoved.Add(character);
                }
                foreach (Character character in toBeRemoved)
                {
                    RemoveCharacter(character);
                }
                Food = 0;
            }
            else
            {
                Food -= foodNeeded;
            }
    }

    #region Incrementing and Modifying
	// Called when a new week is started
	public void IncrementWeek()
	{
		++Week;
        OnDefense.Clear();
        LoadActions();
        LoadEvents();
        foreach (Character character in Roster)
        {
            if (character.RestingWeeks > 0)
                character.RestingWeeks--;
        }
        foreach (Character character in WillAddListCharacters)
        {
            AddCharacter(character);
        }
        WillAddListCharacters.Clear();
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
                if (!AvailableEvents.Contains(e) && (!e.isRitual || (Week == 1 && e.MinWeek <= 1)))
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
        weeksSurvived.enabled = false;
        playAgainButton.SetActive(false);

        //Set the game over text specifying how long your player survived.
        if (Week == 1)
        {
            weeksSurvived.text = "You survived " + Week + " week";
        }
        else
        {
            weeksSurvived.text = "You survived " + Week + " weeks";
        }

        //Hide the normal player GUI and controls
        HideControls();

        SpriteRenderer gameOverSpriteRenderer = gameOverBackground.GetComponent<SpriteRenderer>();

        //Set the Game Over Image with a Random fail message, and then show the image
        gameOverSpriteRenderer.sprite = GetRandomGameOverMessage();

        gameOverBackground.SetActive(true);

        StartCoroutine(FadeIn(gameOverBackground, 3.5f));
        StartCoroutine(LoadGameOverGUI(4));
        SoundManager.instance.PlaySingle(gameOverSound);
    }

    public void HideGameOver()
    {
        gameOver = false;
        gameOverBackground.SetActive(false);
        weeksSurvived.text = "";
        playAgainButton.SetActive(false);
    }

    public void HideControls()
    {
        GameObject gameplayScreen = GameObject.Find("GameplayScreen");
        DialogBoxController eventGUI = GameObject.Find("GameController").GetComponent<DialogBoxController>();
        BuildingController[] buildGUIs = GameObject.Find("GameController").GetComponents<BuildingController>();
        eventGUI.enabled = false;

        foreach (BuildingController bc in buildGUIs)
        {
            bc.enabled = false;
        }

        gameplayScreen.SetActive(false);
        currentWeek.text = "";
    }

    private Sprite GetRandomGameOverMessage()
    {
        return GameOverSprites[Random.Range(0, GameOverSprites.Length)];
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RestartManager rm = GameObject.Find("RestartManager").GetComponent<RestartManager>();
        rm.RestartGameController();

    }

    private static IEnumerator FadeIn(GameObject newObj, float time)
    {
        float timeLeft = time;
        var sprite = newObj?.GetComponent<SpriteRenderer>();
        var text = newObj?.GetComponent<TextMeshProUGUI>();

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (text != null) text.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if (sprite != null) sprite.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            yield return null;
        }
    }

    IEnumerator LoadGameOverGUI(int seconds)
    {
        yield return new WaitForSeconds(seconds); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        weeksSurvived.enabled = true;
         playAgainButton.SetActive(true);
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
        float score = 0;
        foreach (Character character in OnDefense)
        {
            score += character.WorkMultiplier;
        }
        if (score * DefenseMultiplier >= difficulty)
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
			if (!actionPanel.action.SlotsFilled)
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
				if (!characterPanel.character.IsResting)
				{
					Action cAction = characterPanel.character.AssignedAction;
					if (cAction != SelectedAction)
					{
						if (characterPanel.character.AssignedAction != null)
						{
							characterPanel.character.AssignedAction.AssignedCharacters.Remove(characterPanel.character);
						}
						characterPanel.character.AssignedAction = SelectedAction;
						characterPanel.character.AssignedAction.AssignedCharacters.Add(characterPanel.character);
					}
				}
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

    public void RecruitCharacter()
    {
        Character newCharacter = AvailableCharacters[Random.Range(0, AvailableCharacters.Count)];
        WillAddListCharacters.Add(newCharacter);
        AvailableCharacters.Remove(newCharacter);
    }
}