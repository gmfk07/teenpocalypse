using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPanel : MonoBehaviour
{
	public Action action;
	public TextMeshProUGUI actionName;
	public TextMeshProUGUI slotsText;
	public Image panelImage;
	Color originalColor;

	// Start is called before the first frame update
	void Start()
    {
		originalColor = panelImage.color;
	}

    // Update is called once per frame
    void Update()
    {
		panelImage.color = originalColor;
		slotsText.text = "x" + (action.Slots - action.AssignedCharacters.Count).ToString();
		if (action.SlotsFilled)
		{
			Color c = originalColor;
			c.r -= 0.6f; c.g -= 0.6f; c.b -= 0.6f;
			panelImage.color = c;
		}
    }
}
