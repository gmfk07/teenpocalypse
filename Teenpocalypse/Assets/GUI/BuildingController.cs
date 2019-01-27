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
        if (GUI.Button(new Rect(5, 10, width, 30), "50 Supplies: Shelter (" + shelterAmount + ")"))
        {
            if (GameController.Instance.Supplies >= 50)
            {
                GameController.Instance.Supplies -= 50;
                shelterAmount++;
            }
        }

        if (GUI.Button(new Rect(5, 60, width, 30), "75 Food: Farm (" + farmAmount + ")"))
        {
            if (GameController.Instance.Food >= 75)
            {
                GameController.Instance.Food -= 75;
                farmAmount++;
            }
        }

        if (GUI.Button(new Rect(5, 110, width, 30), "150 Supplies: Armory (" + armoryAmount + ")"))
        {
            if (GameController.Instance.Supplies >= 150)
            {
                GameController.Instance.Supplies -= 150;
                armoryAmount++;
            }
        }
    }
}
