using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/FeastEvent")]
public class FeastEvent : Event
{
    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                gc.ChangeFood(gc.Food - gc.Roster.Count*gc.FoodPerPerson);
                gc.TeamMorale = Mathf.Min(Constants.MAX_VALUE, gc.TeamMorale + 10);
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
				return "The villagers feast! Yay!\nFood: -10\nMorale: +10";
			case 1:
				return "The villagers become angry. But ultimately trust in your leadership.\nFood: -0\nMorale: -5";
		}
		return "";
	}
}
