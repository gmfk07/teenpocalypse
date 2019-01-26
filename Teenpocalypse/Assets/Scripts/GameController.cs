using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	public static GameController Instance = null;
	public int Day = 1;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		//boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		//boardScript.SetupScene(level);

	}
}
