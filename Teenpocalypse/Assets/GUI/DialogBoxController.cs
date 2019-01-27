using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour
{
    private int width = 260;
    private int height = 265;
	public bool IsShowing { get { return show; } }
    private bool show = false;
    private Rect windowRect;
    public Event currentEvent;
	bool showingConsequences;
	int choiceChosen = 0;

    void Start()
    {
       windowRect = new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);
    }

    void OnGUI()
    {
		if (show)
		{
			if (!showingConsequences)
				windowRect = GUI.Window(0, windowRect, DialogWindow, currentEvent.name);
			else
				windowRect = GUI.Window(0, windowRect, ConsequencesWindow, currentEvent.name);
		}
    }

    void DialogWindow(int windowID)
    {
        GUI.Label(new Rect(5, 25, width - 8, 250), currentEvent.GetDescription());
		currentEvent.Choices = currentEvent.GetChoices();

        for (int i=0; i<currentEvent.Choices.Count; i++)
        {
            if (GUI.Button(new Rect(5, 170 + 20*i, width - 10, 20), currentEvent.GetChoices()[i]))
            {
                currentEvent.Execute(i);
				choiceChosen = i;
				if (currentEvent.HasConsequencesText)
					showingConsequences = true;
				else
				{
					show = false;
				    GameController.Instance.DialogBoxGone();
					GameController.Instance.IncrementWeek();
				}
            }
        }
    }

	void ConsequencesWindow(int windowID)
	{
		GUI.Label(new Rect(5, 25, width - 8, 250), currentEvent.GetConsequencesText(choiceChosen));
		if (GUI.Button(new Rect(5, 150, width - 10, 20), "OK"))
		{
			showingConsequences = false;
			show = false;
		    GameController.Instance.DialogBoxGone();
			GameController.Instance.IncrementWeek();
		}
	}

    public void ShowBox(Event newEvent)
    {
        currentEvent = newEvent;
        show = true;
    }
}
