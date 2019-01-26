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
                GameController.Instance.Food = Mathf.Max(0, GameController.Instance.Food - 30);
                GameController.Instance.TeamMorale = Mathf.Min(Constants.MAX_VALUE, GameController.Instance.TeamMorale + 10);
                break;

            case 1:
                GameController.Instance.TeamMorale = Mathf.Max(0, GameController.Instance.TeamMorale - 5);
                break;
        }
    }
}
