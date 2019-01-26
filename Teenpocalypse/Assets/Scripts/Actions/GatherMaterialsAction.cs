using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Actions/GatherMaterialsAction")]
public class GatherMaterialsAction : Action
{

	public GatherMaterialsAction()
	{
	}

	public override void Execute(Character actionTaker)
	{
		GameController.Instance.Supplies += (int) Mathf.Round(10 * actionTaker.WorkMultiplier);
	}
}
