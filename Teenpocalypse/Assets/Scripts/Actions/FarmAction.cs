using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Actions/FarmAction")]
public class FarmAction : Action
{

	public FarmAction()
	{
	}

	public override void Execute(Character actionTaker)
	{
		GameController.Instance.Food += (int)Mathf.Round(10 * actionTaker.WorkMultiplier);
    }
}
