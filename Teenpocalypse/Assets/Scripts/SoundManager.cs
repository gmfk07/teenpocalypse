using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundEffectsSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;             

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Keep the music playing even if the scene changes 
        DontDestroyOnLoad(gameObject);
    }


    public void PlaySingle(AudioClip clip)
    {        
        soundEffectsSource.clip = clip;

        soundEffectsSource.Play();
    }


    //Randomize from a set of sound effects.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        soundEffectsSource.clip = clips[randomIndex];

        soundEffectsSource.Play();
    }
}