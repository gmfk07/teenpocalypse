using UnityEngine;

public class FillScreen : MonoBehaviour {
    private void Start() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float w = worldScreenWidth / sr.sprite.bounds.size.x;
        float h = worldScreenHeight / sr.sprite.bounds.size.y;

        float scalar = Mathf.Max(w, h);

        transform.localScale *= scalar;
    }
}