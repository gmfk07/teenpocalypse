using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayNightFilter : MonoBehaviour {
    private PostProcessVolume _volume;
    private Vignette _vignette;

    private void Start() {
        _vignette = ScriptableObject.CreateInstance<Vignette>();
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(1f);

        _volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _vignette);
    }

    private void Update() {
        _vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
    }

    private void OnDestroy() {
        RuntimeUtilities.DestroyVolume(_volume, true, true);
    }
}