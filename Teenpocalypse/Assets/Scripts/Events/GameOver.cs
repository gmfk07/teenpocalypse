using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GameOver : MonoBehaviour
{
    public int weeks = 4;

    public TextMeshProUGUI weeksSurvived;
    public GameObject gameOverBackground;
    public Sprite[] GameOverSprites;

    // Start is called before the first frame update
    void Start()
    {
        //        gameOverBackground = GameObject.Find("GameOverScreen");
        //        weeksSurvived = GameObject.Find("WeeksSurvived").GetComponent<TextMeshProUGUI>();

        gameOverBackground.SetActive(false);
        weeksSurvived.text = "";

        CallGameOver(weeks);
    }

    public void CallGameOver(int weeks)
    {
        //        gameOverBackground = GameObject.Find("GameOverScreen");
        //        weeksSurvived = GameObject.Find("WeeksSurvived").GetComponent<TextMeshProUGUI>();
        weeksSurvived.text = "You survived " + weeks + " weeks.";
        gameOverBackground.SetActive(true);
        SetRandomGameOverMessage();
    }

    public void HideControls()
    {

 
    }

public void HideGameOver()
    {
//        gameOverBackground = GameObject.Find("GameOverScreen");
        gameOverBackground.SetActive(false);
    }

    private void SetRandomGameOverMessage()
    {
        Debug.Log("Called SetRandomGameOverMessage");
        Image gameOverImage = gameOverBackground.GetComponent<Image>();
        gameOverImage.sprite = GameOverSprites[Random.Range(0, GameOverSprites.Length)];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
