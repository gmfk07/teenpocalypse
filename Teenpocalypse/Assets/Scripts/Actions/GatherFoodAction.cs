using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Actions/GatherFoodAction")]
public class GatherFoodAction : Action
{

	public GatherFoodAction()
	{
	}

	public override void Execute(Character actionTaker)
	{
		GameController.Instance.Food += (int)Mathf.Round(8 * actionTaker.WorkMultiplier);
        if (Random.Range(0, 100) < 50)
            actionTaker.Health -= 3;
    }
}
