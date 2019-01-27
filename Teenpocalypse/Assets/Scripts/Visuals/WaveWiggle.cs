using UnityEngine;

public class WaveWiggle : MonoBehaviour {
    private float _startY;
    private float _amplitudeStart = 0.15f;
    private float _amplitude;
    private float _frequencyStart = 0.5f;
    private float _frequency;
    private float _amplitudeFrequency = .5f;
    private float _frequencyFrequency = .3f;
    private float _offset;
    private float _offset2;
    private float _offset3;

    private void Start() {
        _startY = transform.position.y;
        _amplitudeStart *= Random.Range(0.8f, 1.2f);
        _frequencyStart *= Random.Range(1f, 1.4f);
        _amplitudeFrequency *= Random.Range(1f, 1.4f);
        _frequencyFrequency *= Random.Range(1f, 1.4f);
        _offset = Random.Range(-20 * Mathf.PI, 20 * Mathf.PI);
        _offset2 = Random.Range(-20 * Mathf.PI, 20 * Mathf.PI);
        _offset3 = Random.Range(-20 * Mathf.PI, 20 * Mathf.PI);
    }

    private void Update() {
        _amplitude = _amplitudeStart + _amplitudeStart / 4 * Mathf.Sin(Time.time * _amplitudeFrequency + _offset2);
        _frequency = _frequencyStart + _frequencyStart / 4 * Mathf.Sin(Time.time * _frequencyFrequency + _offset3);
        transform.position = new Vector3(transform.position.x,
                                         _startY + _amplitude * Mathf.Sin(Time.time * _frequency + _offset),
                                         transform.position.z);
    }
}