using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceList : MonoBehaviour
{
    public TextMeshProUGUI textField;

    private void Start()
    {
        //textField = GetComponent<TextMeshProUGUI>();
    }

    private void OnGUI()
    {
        textField.text = "Food: " + GameController.Instance.Food + "\nSupplies: " + GameController.Instance.Supplies
            + "\nMorale: " +
            GameController.Instance.TeamMorale;
    }
}
