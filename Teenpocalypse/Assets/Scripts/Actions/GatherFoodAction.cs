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
		GameController.Instance.Food += (int)Mathf.Round(10 * actionTaker.WorkMultiplier);
        if (Random.Range(0, 1) < .50f)
            actionTaker.Health -= 5;
    }
}
