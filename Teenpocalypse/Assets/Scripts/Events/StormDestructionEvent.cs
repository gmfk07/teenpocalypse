using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/StormDestructionEvent")]
public class StormDestructionEvent : Event
{
	public Character ChosenCharacter;
	public List<Character> OtherCharacters;

	public override void Chosen()
	{
		ChosenCharacter = GameController.Instance.Roster[Random.Range(0, GameController.Instance.Roster.Count)];
		OtherCharacters = new List<Character>();
	}
	public override string GetDescription()
	{
		return "A storm hit!\n\nOnly " + ChosenCharacter.Name + "'s portion of the storm has been hit."
			+ ChosenCharacter.Name + " is really upset and is afraid of sleeping outside.";
	}

	public override List<string> GetChoices()
	{
		List<string> result = new List<string>();
		result.Add("Tell them to deal with it.");

		for (int i = 0; i < GameController.Instance.Roster.Count; ++i) {
			Character affected = GameController.Instance.Roster[i];
			if (affected == ChosenCharacter)
				continue;
			string cName = affected.Name;
			string choice = "Force " + cName + " to sleep outside instead.";
			result.Add(choice);
			OtherCharacters.Add(affected);
		}
		return result;
	}

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
				ChosenCharacter.ChangeRelationshipWithDeletion(-10);
				ChosenCharacter.ChangeHealthWithDeletion(-5);
				break;
			default:
				index -= 1;
				OtherCharacters[index].ChangeRelationshipWithDeletion(-15);
				OtherCharacters[index].ChangeHealthWithDeletion(-5);
				ChosenCharacter.ChangeRelationshipWithDeletion(20);
				break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return ChosenCharacter.Name + " is sad and blames it on you for telling them to 'Deal With It'. They get hurt from outdoor weather."
					+ "\n" + ChosenCharacter.Name +"'s Relation: -10\n" + ChosenCharacter.Name + "'s Health: -5";
			default:
				string cName = OtherCharacters[choice - 1].Name;
				return ChosenCharacter.Name + " becomes ecstatic! They jump into " + cName + "'s shelter and dance.\n" + cName + " hates you.\n\n"
					+ "\n" + ChosenCharacter.Name + "'s Relation: +20\n" + cName + "'s Relation: -20\n" + cName + "'s Health: -5";
		}
	}
}
