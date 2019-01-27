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

	Color originalColor;

	// Start is called before the first frame update
	void Start()
    {
		LoadCharacterData();
		originalColor = characterImageUI.color;
	}

    // Update is called once per frame
    void Update()
    {
        LoadCharacterData();
		if (character.IsResting)
		{
			Color c = originalColor;
			c.r -= 0.5f; c.g -= 0.5f; c.b -= 0.5f;
			characterImageUI.color = c;
			characterActionText.text = "Resting: " + character.RestingWeeks + " Weeks Left";
		    character.AssignedAction = null;
		}
		else
		{
			characterImageUI.color = originalColor;
			if (character.AssignedAction != null)
				characterActionText.text = character.AssignedAction.Name;
			else
				characterActionText.text = "";
		}
	}

	public void LoadCharacterData()
	{
		characterName.text = character.Name;
		characterPicture = character.Icon;
		characterImageUI.texture = characterPicture;
		characterHealthText.text = "Health: " + character.Health.ToString() + "/" + character.MaxHealth;
		characterRelationshipText.text = "Relation: " +character.Relationship.ToString() + "/100";
	}
}
