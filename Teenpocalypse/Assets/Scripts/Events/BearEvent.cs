using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/BearEvent")]
public class BearEvent : Event
{
    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {

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
