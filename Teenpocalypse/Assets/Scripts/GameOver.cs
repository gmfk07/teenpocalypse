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
        gameOverBackground.SetActive(false);
        weeksSurvived.text = "";

        Debug.Log("Game over!");
        CallGameOver(weeks);
    }

    public void CallGameOver(int weeks)
    {
        weeksSurvived.text = "You survived " + weeks + " weeks.";

        SpriteRenderer gameOverSpriteRenderer = gameOverBackground.GetComponent<SpriteRenderer>();
        gameOverSpriteRenderer.sprite = GetRandomGameOverMessage();

        Debug.Log("Activate game over background");
        gameOverBackground.SetActive(true);
        StartCoroutine(FadeIn(gameOverBackground));
    }

public void HideGameOver()
    {
        gameOverBackground.SetActive(false);
    }

    private Sprite GetRandomGameOverMessage()
    {
        Debug.Log("Get a random sprite");
        return GameOverSprites[Random.Range(0, GameOverSprites.Length)];
    }

    private static IEnumerator FadeIn(GameObject newObj)
    {
        const float time = 1;
        float timeLeft = time;
        var sprite = newObj?.GetComponent<SpriteRenderer>();

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (sprite != null) sprite.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            yield return null;
        }
    }

}
