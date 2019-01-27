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
}
