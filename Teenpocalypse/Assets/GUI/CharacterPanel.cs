using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{

	public Character character;
	public TextMeshProUGUI characterName;
	public TextMeshProUGUI characterActionText;
	public TextMeshProUGUI characterHealthText;
	public TextMeshProUGUI characterRelationshipText;
	public RawImage characterImageUI;
	public Texture characterPicture;

	// Start is called before the first frame update
	void Start()
    {
		LoadCharacterData();
	}

    // Update is called once per frame
    void Update()
    {
		if (character.AssignedAction != null)
			characterActionText.text = character.AssignedAction.Name;
		else
			characterActionText.text = "";
	}

	public void LoadCharacterData()
	{
		characterName.text = character.Name;
		characterPicture = character.Icon;
		characterImageUI.texture = characterPicture;
		characterHealthText.text = character.Health.ToString();
		characterRelationshipText.text = character.Relationship.ToString();
	}
}
