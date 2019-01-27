using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/FeastEvent")]
public class FeastEvent : Event
{
    private int amtConsumed;

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                amtConsumed = gc.Roster.Count * gc.FoodPerPerson;
                gc.ChangeFood(-amtConsumed);
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
				return "The villagers feast! Yay!\nFood: -" + amtConsumed + "\nMorale: +10";
			case 1:
				return "The villagers become angry, but ultimately trust in your leadership.\nFood: -0\nMorale: -5";
		}
		return "";
	}
}
