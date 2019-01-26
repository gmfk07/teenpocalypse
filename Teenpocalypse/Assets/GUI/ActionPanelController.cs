using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPanelController : MonoBehaviour
{
	public GameObject ActionPanel;

    // Start is called before the first frame update
    void Start()
    {
		GameController.Instance.Event_OnWeekStart += OnWeekStart;

		int i = 0;
		int pad = 195;
		int panelsPerRow = 3;
		foreach (Action action in GameController.Instance.AvailableActions)
		{
			Vector3 offset = new Vector3(pad * (i % panelsPerRow), pad * 0.75f * (i / panelsPerRow), 0);
			GameObject panel = Instantiate(ActionPanel, transform.position + offset, Quaternion.identity, transform);
			//panel.GetComponent<RectTransform>().to = offset;
			panel.GetComponentInChildren<TextMeshProUGUI>().SetText(action.Name);
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
