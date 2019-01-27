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
			+ ChosenCharacter.Name + " is really upset and is afraid he won't have anywhere to sleep tonight.";
	}

	public override List<string> GetChoices()
	{
		List<string> result = new List<string>();
		result.Add("Tell them to deal with it.\nForce " + ChosenCharacter.Name + " to sleep outside.");

		for (int i = 0; i < GameController.Instance.Roster.Count; ++i) {
			Character affected = GameController.Instance.Roster[i];
			if (affected == ChosenCharacter)
				continue;
			string cName = affected.Name;
			string choice = "Force " + cName + " to give up their bed and sleep outside instead.";
			result.Add(choice);
			OtherCharacters.Add(affected);
		}
	}

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                gc.ChangeFood(gc.Food - gc.Roster.Count*gc.FoodPerPerson);
                gc.TeamMorale = Mathf.Min(Constants.MAX_VALUE, gc.TeamMorale - 5);
                break;

            case 1:
                gc.TeamMorale = Mathf.Max(0, gc.TeamMorale - 5);
                break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return "Each villager becomes ";
			case 1:
				return "The villagers become angry. But ultimately trust in your leadership.\nFood: -0\nMorale: -5";
		}
		return "";
	}
}
