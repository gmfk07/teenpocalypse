using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/FeastEvent")]
public class FeastEvent : Event
{
    public override void Execute(int index)
    {
        switch (index)
        {
            case 0:
                GameController.Instance.Food = Mathf.Max(0, GameController.Instance.Food - GameController.Instance.Roster.Count*GameController.Instance.FoodPerPerson);
                GameController.Instance.TeamMorale = Mathf.Min(Constants.MAX_VALUE, GameController.Instance.TeamMorale + 10);
                break;

            case 1:
                GameController.Instance.TeamMorale = Mathf.Max(0, GameController.Instance.TeamMorale - 5);
                break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return "The villagers feast! Yay!\nFood: -10\nMorale: +10";
			case 1:
				return "The villagers become angry. But ultimately trust in your leadership.\nFood: -0\nMorale: -5";
		}
		return "";
	}
}
