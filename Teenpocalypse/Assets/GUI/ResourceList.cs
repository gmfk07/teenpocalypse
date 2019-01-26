using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceList : MonoBehaviour
{
    private Text textField;

    private void Start()
    {
        textField = GetComponent<Text>();
    }

    private void OnGUI()
    {
        textField.text = "Food: " + GameController.Instance.Food + "\nSupplies: " + GameController.Instance.Supplies +
            "\nTools: " + GameController.Instance.Tools + "\nWeapons: " + GameController.Instance.Weapons + "\nMorale: " +
            GameController.Instance.TeamMorale;
    }
}
