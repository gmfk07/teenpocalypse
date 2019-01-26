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
		GameController.Instance.Wood += 10;
	}
}
