using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/AdultEvent")]
public class AdultEvent : Event
{
    private bool success;
    private Character target;
    private int loss;

    GameController gc = GameController.Instance;

    public override List<string> GetChoices()
    {
        List<string> result = new List<string>();
        result.Add("Fight them!");

        if (gc.Supplies >= 20)
            result.Add("Give them what they want.");
        return result;
    }

    public override void Execute(int index)
    {
        switch (index)
        {
            case 0:
                if (gc.TestDefense(1.8f))
                {
                    success = true;
                    gc.TeamMorale = Mathf.Min(gc.TeamMorale + 10, 0);
                }
                else
                {
                    success = false;
                    if (gc.OnDefense.Count > 0)
                        target = gc.OnDefense[Random.Range(0, gc.OnDefense.Count)];
                    else
                        target = gc.Roster[Random.Range(0, gc.Roster.Count)];
					target.ChangeHealthWithDeletion(-45);
                    gc.TeamMorale = Mathf.Max(gc.TeamMorale + 15, Constants.MAX_VALUE);
                }
                break;

            case 1:
                gc.ChangeSupplies(-20);
                gc.TeamMorale = Mathf.Min(gc.TeamMorale - 10, 0);
                break;
        }
    }

	public override string GetConsequencesText(int choice)
	{
		switch (choice)
		{
			case 0:
                if (success)
                    return "Adult bodies litter the ground. The village cheers!\n\nMorale: +10";
                else
                    return "They tore us apart. " + target.Name + "got badly injured, though no supply was taken. The village "
                        + " rallies around their brave fighting. Never again!"
						+ target.Name + "'s Health: -45" + "\nMorale: +15";

			case 1:
				return "We give the adults what they want. They jeer as they trample all over our supply. The villagers consider you "
                    + "cowardly. Supply: -20, Morale: -10";
		}
		return "";
	}
}
