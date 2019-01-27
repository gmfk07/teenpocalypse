using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/RecruitAction")]
public class RecruitAction : Action
{
    public RecruitAction()
    {
    }

    public override void Execute(Character actionTaker)
    {
        if (Random.Range(0, 100) < 25 * actionTaker.WorkMultiplier && GameController.Instance.AvailableCharacters.Count > 0)
            GameController.Instance.RecruitCharacter();
    }
}
