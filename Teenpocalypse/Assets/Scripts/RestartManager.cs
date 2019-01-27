using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{ 
    public void RestartGameController()
    {
        Destroy(GameObject.Find("GameController"));
    }

    public void RestartSoundController()
    {
        Destroy(GameObject.Find("SoundController"));
    }
}
