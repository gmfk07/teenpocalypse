using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class NameGenerator : MonoBehaviour
{
    public string charName;

    [Serializable]
    public class CharacterDetails
    {
        public string name;
        public string surname;
    }

    private string GetPlayerName()
    {
        string newName = "";
        CharacterDetails newCharacter = new CharacterDetails();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://uinames.com/api/?region=United%20States"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log(jsonResponse);
        newCharacter = JsonUtility.FromJson<CharacterDetails>(jsonResponse);
        newName = newCharacter.name + " " + newCharacter.surname;
        Debug.Log("Name is " + newName);
        return newName;
    }

    // Start is called before the first frame update
    void Start()
    {
        charName = GetPlayerName();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
