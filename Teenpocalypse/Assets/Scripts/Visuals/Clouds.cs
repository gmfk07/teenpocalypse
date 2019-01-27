using UnityEngine;

public class Clouds : MonoBehaviour {
    public GameObject clouds1;
    public GameObject clouds2;
    public float startX;
    public float midX;
    public float endX;
    public float speed;
    private bool _one = true;
    private bool _two = false;

    void Update() {
        if(_one)
            clouds1.transform.Translate(Vector3.right * Time.deltaTime * speed);
        if(_two)
            clouds2.transform.Translate(Vector3.right * Time.deltaTime * speed);
        if(_one) {
            if(clouds1.transform.localPosition.x > midX)
                _two = true;
            if(clouds1.transform.localPosition.x > endX) {
                _one = false;
                clouds1.transform.localPosition = new Vector3(startX, 0, 0);
            }
        }
        if(_two) {
            if(clouds2.transform.localPosition.x > midX)
                _one = true;
            if(clouds2.transform.localPosition.x > endX) {
                _two = false;
                clouds2.transform.localPosition = new Vector3(startX, 0, 0);
            }
        }
    }
}