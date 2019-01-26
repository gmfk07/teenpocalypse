using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanelController : MonoBehaviour
{
	List<CharacterPanel> characterPanels;
	public GameObject characterPanelPrefab;

    // Start is called before the first frame update
    void Start()
    {
		GameController.Instance.Event_OnWeekStart += OnWeekStart;
		GameController.Instance.Event_OnCharacterRemoved += OnCharacterChanged;
		GameController.Instance.Event_OnCharacterAdded += OnCharacterChanged;

		characterPanels = new List<CharacterPanel>();
		CreateCharacterPanels();
    }

	void CreateCharacterPanels()
	{
		int i = 0;
		int pad = 155;
		foreach (Character character in GameController.Instance.Roster)
		{
			Vector3 offset = new Vector3(pad * i, 63, 0);
			offset.x -= GameController.Instance.Roster.Count / 2 * pad;
			GameObject panel = Instantiate(characterPanelPrefab, transform.position + offset, Quaternion.identity, transform);
			CharacterPanel characterPanel = panel.GetComponent<CharacterPanel>();
			characterPanel.character = character;
			characterPanel.LoadCharacterData();
			characterPanels.Add(characterPanel);
			++i;
		}
	}

	void DeleteCharacterPanels()
	{
		if (characterPanels == null || characterPanels.Count == 0)
			return;
		for (int i = characterPanels.Count - 1; i >= 0; --i)
		{
			CharacterPanel panel = characterPanels[i];
			DestroyImmediate(panel.gameObject);
		}
		characterPanels.Clear();
	}

	void ResetCharacterPanels()
	{
		DeleteCharacterPanels();
		CreateCharacterPanels();
	}
	void OnCharacterChanged(Character c)
	{
		ResetCharacterPanels();
	}

	void OnWeekStart()
	{

	}
}
