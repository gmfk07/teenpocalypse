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
		GameController.Instance.Event_OnActionAdded += OnActionChanged;
		GameController.Instance.Event_OnActionRemoved += OnActionChanged;

		actionPanels = new List<ActionPanel>();
		CreateActionPanels();
    }

	void CreateActionPanels()
	{
		int i = 0;
		int pad = 325;
		int panelsPerRow = 10;
		foreach (Action action in GameController.Instance.AvailableActions)
		{
			Vector3 offset = new Vector3(pad * (i % panelsPerRow), pad * 0.75f * (i / panelsPerRow), 0);
			offset.x -= GameController.Instance.AvailableActions.Count / 2 * pad - 60;
			GameObject panel = Instantiate(ActionPanel, transform.position + offset, Quaternion.identity, transform);
			ActionPanel actionPanel = panel.GetComponent<ActionPanel>();
			actionPanel.action = action;
			actionPanel.actionName.text = action.Name;
			actionPanels.Add(actionPanel);
			++i;
		}

		Debug.Log("Creating Action Panels");
	}

	void DeleteActionPanels()
	{
		if (actionPanels == null || actionPanels.Count == 0)
			return;
		foreach (ActionPanel panel in actionPanels)
		{
			Destroy(panel.gameObject);
		}
		actionPanels.Clear();
		Debug.Log("Deleting Action Panels");
	}

	void ResetCharacterPanels()
	{
		DeleteActionPanels();
		CreateActionPanels();
	}

	void OnActionChanged(Action action)
	{
		ResetCharacterPanels();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnWeekStart()
	{
		ResetCharacterPanels();
	}
}
