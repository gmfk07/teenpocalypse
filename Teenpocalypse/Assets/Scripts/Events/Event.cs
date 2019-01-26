using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : ScriptableObject
{
    public string Name;
    [TextArea(4, 20)] public string Description;
    [Range(0, 10)] public int minWeek;
    public List<string> choices;

    public abstract void Execute(int choice);
}
