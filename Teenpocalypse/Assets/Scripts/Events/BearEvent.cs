using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/BearEvent")]
public class BearEvent : Event
{
    private bool success;
    private Character target;
    private int loss;

    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                if (gc.TestDefense(1))
                    success = true;
                else
                    success = false;
                break;

            case 1:
                loss = gc.Food - (gc.Food / 2);
                gc.Food = gc.Food / 2;
                break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
                if (success)
				    return "The bear was driven away! Three cheers for our brave fighters!\nMorale: +10";
                else
                    return "We failed in our defense. The bear got into our food supply, and"
                break;

			case 1:
				return "The bear is given a wide berth, as they start to eat the food supply. Not much is left.\nFood: \nMorale: -5";
		}
		return "";
	}
}
