using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanelController : MonoBehaviour
{
	List<CharacterPanel> characterPanels;

    // Start is called before the first frame update
    void Start()
    {
		GameController.Instance.Event_OnWeekStart += OnWeekStart;

		CreateCharacterPanels();
    }

	void CreateCharacterPanels()
	{
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnWeekStart()
	{

	}
}
