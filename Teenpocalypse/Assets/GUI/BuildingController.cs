using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public int shelterAmount = 2;
    public int farmAmount = 0;
    public int armoryAmount = 0;
    public int recruitAmount = 0;
    public RecruitAction recruitAction;
    public FarmAction farmAction;

    public int width = 250;
    void OnGUI()
    {
        int buttonx = Screen.width - width - 5;
        GameController gc = GameController.Instance;

        if (GUI.Button(new Rect(buttonx, 10, width, 30), "50 Supplies: Shelter (" + shelterAmount + ")"))
        {
            if (gc.Supplies >= 50)
            {
                gc.Supplies -= 50;
                shelterAmount++;
            }
        }

        if (GUI.Button(new Rect(buttonx, 50, width, 30), "70 Food: Farm (" + farmAmount + ")"))
        {
            if (gc.Food >= 70)
            {
                gc.Food -= 70;
                farmAmount++;
                if (!gc.AvailableActions.Contains(farmAction))
                    gc.AddAction(farmAction);
                else
                    farmAction.Slots++;
            }
        }

        if (GUI.Button(new Rect(buttonx, 90, width, 30), "70 Supplies: Armory (" + armoryAmount + ")"))
        {
            if (gc.Supplies >= 70)
            {
                gc.Supplies -= 70;
                armoryAmount++;
            }
        }

        if (GUI.Button(new Rect(buttonx, 130, width, 30), "30 Food, 40 Supplies: Scout Hut (" + recruitAmount + ")"))
        {
            if (gc.Supplies >= 40 && gc.Food >= 30)
            {
                gc.Supplies -= 40;
                gc.Food -= 30;
                if (!gc.AvailableActions.Contains(recruitAction))
                    gc.AddAction(recruitAction);
                else
                    recruitAction.Slots++;
                recruitAmount++;
            }
        }
    }
}
