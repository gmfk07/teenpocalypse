using UnityEngine;

/// <summary>
/// Will move it from the pivot point of the sprite, which by default is the center,
/// so that probably needs to be changed.
/// </summary>
public class MoveToCorner : MonoBehaviour {
    public enum Corner {
        TopRight,
        TopLeft,
        BottomRight,
        BottomLeft
    }

    public Corner corner;

    private void Start() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 pos = Vector3.zero;
        switch(corner) {
            case Corner.TopRight:
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
                break;
            case Corner.TopLeft:
                pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight));
                break;
            case Corner.BottomRight:
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0));
                break;
            case Corner.BottomLeft:
                pos = Camera.main.ScreenToWorldPoint(Vector3.zero);
                break;
        }
        sr.transform.position = new Vector3(pos.x, pos.y);
    }
}