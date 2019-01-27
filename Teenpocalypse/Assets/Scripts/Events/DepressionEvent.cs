using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/DepressionEvent")]
public class DepressionEvent : Event
{
	public Character ChosenCharacter;
	public List<Character> OtherCharacters;

	public override void Chosen()
	{
		ChosenCharacter = GameController.Instance.Roster[Random.Range(0, GameController.Instance.Roster.Count)];
	}
	public override string GetDescription()
	{
        return ChosenCharacter.Name + " shyly approaches you. The apocalypse has taken its toll on them, and they " +
            "say they're experiencing a harsh bout of depression.";
	}

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                ChosenCharacter.RestingWeeks += 4;
				ChosenCharacter.ChangeRelationshipWithDeletion(10);
				break;
			case 1:
                ChosenCharacter.ChangeRelationshipWithDeletion(-10);
                ChosenCharacter.WorkMultiplier = Mathf.Max(0.1f, ChosenCharacter.WorkMultiplier - 0.1f);
                gc.TeamMorale = Mathf.Max(0, gc.TeamMorale - 5);
				break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return ChosenCharacter.Name + " gives you a faint smile when you tell them to take some time off. You hope you haven't sealed"
					+ "the settlement's fate.\n" + ChosenCharacter.Name +"'s Relation: +10\n" + ChosenCharacter.Name + " rests for 3 weeks";
			default:
				return ChosenCharacter.Name + " struggles to get back to work, and brings down the camp's morale. Their illness has also" +
                    "taken a toll on their productivity.\n"
					+ "\n" + ChosenCharacter.Name + "'s Relation: -10\n" + ChosenCharacter.Name + "'s Productivity: -10%\nMorale: -5";
		}
	}
}
