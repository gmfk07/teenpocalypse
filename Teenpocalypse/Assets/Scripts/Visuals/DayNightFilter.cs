using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class DayNightFilter : MonoBehaviour {
    [Range(0, 1)] [Tooltip("How night is it")]
    public float nightness;

    public List<GameObject> shadowsToFadeOut;
    private PostProcessVolume _volume;
    private Light _light;
    private float _oldNightness;

    private void Start() {
        _volume = GetComponent<PostProcessVolume>();
        _light = GetComponent<Light>();
        _oldNightness = nightness;
    }

    private void Update() {
        _volume.weight = nightness;
        _light.intensity = Mathf.Lerp(1, 0.25f, nightness);
        if(Math.Abs(_oldNightness - nightness) > 0.01f) {
            foreach(var shadow in shadowsToFadeOut) {
                shadow.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, nightness);
            }
        }
        _oldNightness = nightness;
    }
}