using UnityEngine;
using System.Collections.Generic;

/// <inheritdoc />
/// <summary>
/// Based on https://gist.github.com/sinbad/4a9ded6b00cf6063c36a4837b15df969
/// by Steve Streeting
/// </summary>
public class FireFlicker : MonoBehaviour {
    private Light _light;
    public float minIntensity;
    public float maxIntensity = 1f;

    [Range(1, 50)]
    public int smoothing = 5;

    private Queue<float> _smoothQueue;
    private float _lastSum = 0;


    private void Start() {
        _smoothQueue = new Queue<float>(smoothing);
        _light = GetComponent<Light>();
    }

    private void Update() {
        // pop off an item if too big
        while(_smoothQueue.Count >= smoothing) {
            _lastSum -= _smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        _smoothQueue.Enqueue(newVal);
        _lastSum += newVal;

        // Calculate new smoothed average
        _light.intensity = _lastSum / _smoothQueue.Count;
    }
}