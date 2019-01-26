using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxController : MonoBehaviour
{
    private int width = 200;
    private int height = 225;
    private bool show = true;
    private Rect windowRect;
    public Event currentEvent;

    void Start()
    {
       windowRect = new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);
    }

    void OnGUI()
    {
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, currentEvent.name);
    }

    void DialogWindow(int windowID)
    {
        GUI.Label(new Rect(5, 25, width - 8, 250), currentEvent.Description);

        for (int i=0; i<currentEvent.Choices.Count; i++)
        {
            if (GUI.Button(new Rect(5, 150 + 25*i, width - 10, 20), currentEvent.Choices[i]))
            {
                currentEvent.Execute(i);
                show = false;
                GameController.Instance.IncrementWeek();
            }
        }
    }

    public void ShowBox(Event newEvent)
    {
        currentEvent = newEvent;
        show = true;
    }
}
