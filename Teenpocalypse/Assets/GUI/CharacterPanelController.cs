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

	private void Update()
	{
		int i = 0;
		int pad = 155;
		foreach (CharacterPanel panel in characterPanels)
		{
			Vector3 offset = new Vector3(pad * i, 63, 0);
			offset.x -= GameController.Instance.Roster.Count / 2 * pad;
			Vector3 newPosition = transform.position + offset;
			panel.transform.position = newPosition;
			++i;
		}
	}

	void CreateCharacterPanels()
	{
		foreach (Character character in GameController.Instance.Roster)
		{
			GameObject panel = Instantiate(characterPanelPrefab, transform.position, Quaternion.identity, transform);
			CharacterPanel characterPanel = panel.GetComponent<CharacterPanel>();
			characterPanel.character = character;
			characterPanel.LoadCharacterData();
			characterPanels.Add(characterPanel);
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
