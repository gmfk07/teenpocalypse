using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Actions/GatherMaterialsAction")]
public class GatherMaterials : Action
{

	public GatherMaterials()
	{
	}

	public override void Execute(Character actionTaker)
	{
		GameController.Instance.Wood += 10;
	}
}
