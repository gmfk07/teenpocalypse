using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/FoodRequestEvent")]
public class FoodRequestEvent : Event
{
	public Character ChosenCharacter;
	public List<Character> OtherCharacters;

	public override void Chosen()
	{
		ChosenCharacter = GameController.Instance.Roster[Random.Range(0, GameController.Instance.Roster.Count)];
		OtherCharacters = new List<Character>();
		for (int i = 0; i < GameController.Instance.Roster.Count; ++i)
		{
			Character affected = GameController.Instance.Roster[i];
			if (affected == ChosenCharacter)
				continue;
			OtherCharacters.Add(affected);
		}
	}
	public override string GetDescription()
	{
		return ChosenCharacter.Name + " is very hungry.\n\nThey ask for extra food.";
	}

	public override List<string> GetChoices()
	{
		List<string> result = new List<string>();
		result.Add("Refuse " +  ChosenCharacter.Name + "'s request");
		result.Add("Give " + ChosenCharacter.Name + " extra food");
		return result;
	}

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
				ChosenCharacter.ChangeRelationshipWithDeletion(-10);
				ChosenCharacter.ChangeHealthWithDeletion(-10);
				break;
			case 1:
				foreach (Character other in OtherCharacters)
				{
					other.ChangeRelationshipWithDeletion(-5);
				}
				ChosenCharacter.ChangeHealth(10);
				ChosenCharacter.ChangeRelationship(10);
				break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return ChosenCharacter.Name + " goes to bed hungry. He is mad you turned him down."
					+ "\n" + ChosenCharacter.Name +"'s Relation: -10\n" + ChosenCharacter.Name + "'s Health: -10";
			case 1:
				return ChosenCharacter.Name + " gobbles down the precious rations, feeling like a king. The rest find out and call you unfair.\n\n"
					+ ChosenCharacter.Name + "'s Relation: +10\n" + ChosenCharacter.Name + "'s Health: +10\n" + "Everyone Else's Relation: -5";
		}
		return "";
	}
}
