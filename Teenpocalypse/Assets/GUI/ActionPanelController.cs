using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPanelController : MonoBehaviour
{
	public GameObject ActionPanel;

	List<ActionPanel> actionPanels;

    // Start is called before the first frame update
    void Start()
    {
		GameController.Instance.Event_OnWeekStart += OnWeekStart;

		CreateActionPanels();
    }

	void CreateActionPanels()
	{
		int i = 0;
		int pad = 205;
		int panelsPerRow = 3;
		foreach (Action action in GameController.Instance.AvailableActions)
		{
			Vector3 offset = new Vector3(pad * (i % panelsPerRow), pad * 0.75f * (i / panelsPerRow), 0);
			GameObject panel = Instantiate(ActionPanel, transform.position + offset, Quaternion.identity, transform);
			ActionPanel actionPanel = panel.GetComponent<ActionPanel>();
			actionPanel.action = action;
			actionPanel.actionName.text = action.Name;
			++i;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnWeekStart()
	{

	}
}
