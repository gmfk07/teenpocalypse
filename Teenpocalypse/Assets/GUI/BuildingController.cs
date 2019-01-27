using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public int shelterAmount = 2;
    public int farmAmount = 0;
    public int armoryAmount = 0;

    public int width = 200;
    
    void OnGUI()
    {
        GameController gc = GameController.Instance;

        if (GUI.Button(new Rect(5, 10, width, 30), "50 Supplies: Shelter (" + shelterAmount + ")"))
        {
            if (gc.Supplies >= 50)
            {
                gc.Supplies -= 50;
                shelterAmount++;
            }
        }

        if (GUI.Button(new Rect(5, 60, width, 30), "75 Food: Farm (" + farmAmount + ")"))
        {
            if (gc.Food >= 75)
            {
                gc.Food -= 75;
                farmAmount++;
            }
        }

        if (GUI.Button(new Rect(5, 110, width, 30), "150 Supplies: Armory (" + armoryAmount + ")"))
        {
            if (gc.Supplies >= 150)
            {
                gc.Supplies -= 150;
                armoryAmount++;
            }
        }
    }
}
