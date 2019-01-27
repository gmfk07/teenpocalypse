using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/GuardAction")]
public class GuardAction : Action
{
    public GuardAction()
    {
    }

    public override void Execute(Character actionTaker)
    {
        GameController.Instance.CharactersOnDefense ++;
    }
}
