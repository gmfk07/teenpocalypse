using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/GovtRitualEvent")]
public class GovtRitualEvent : Event
{
    public override void Execute(int index)
    {
        GameController gc = GameController.Instance;
        switch (index)
        {
            case 0:
                foreach (Character character in gc.Roster)
                {
                    character.WorkMultiplier += .1f;
                    character.ChangeRelationshipWithDeletion(-8);
                }
                gc.TeamMorale = Mathf.Min(gc.TeamMorale + 5, Constants.MAX_VALUE);
                break;

            case 1:
                foreach (Character character in gc.Roster)
                {
                    character.ChangeRelationshipWithDeletion(5);
                }
                break;

            case 2:
                foreach (Character character in gc.Roster)
                {
                    character.WorkMultiplier -= .1f;
                    character.ChangeRelationshipWithDeletion(8);
                }
                gc.TeamMorale = Mathf.Min(gc.TeamMorale + 5, Constants.MAX_VALUE);
                break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
				return "You impose your will and take drastic measures in these desperate times to increase production. You are resented"
                    + " by your subjects, but your unwavering determination keeps morale high.\n\nAll Productivity: +10%\nAll Relation: -8\n" +
                    "Morale: +5";
			case 1:
				return "Listening to your subjects' worries improves their mood, and you still maintain control over your society's direction."
                    + "\n\nAll Relation: +5";
            case 2:
                return "Death to all who stand in the way of freedom for teenagers!\n\nAll Productivity: -10%\nAll Relation: +8\nMorale: +5";
        }
		return "";
	}
}
