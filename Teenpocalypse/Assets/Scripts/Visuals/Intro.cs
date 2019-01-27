using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject black;
    public GameObject johnny;
    public GameObject johnnyBG;
    public GameObject johnnyHappy;
    public GameObject johnnyHappyBG;
    public GameObject teens;
    public GameObject teensProtag;
    public GameObject teensMagic;
    public GameObject teensValentino;
    public GameObject teensCrystal;
    public List<GameObject> texts;
    public TMP_InputField textField;

    private int _state = 0;
    private bool _gotToNameScreen;
    private bool _hasNameRun;
    private bool _hasName;
    public TextMeshProUGUI nameText;
    private string _nameTextStart;
    private string _nameTextEnd;
    [HideInInspector] public string playerName;

    //Intro Music
    public AudioClip introMusicPart1;
    public AudioClip introMusicPart2;

    public void OnEdit() {
        nameText.text = _nameTextStart + textField.text + _nameTextEnd;
    }

    IEnumerator waitForIntoMusic(float seconds)
    {
        yield return new WaitForSeconds(seconds); // This statement will make the coroutine wait for the number of seconds you put there, 2 seconds in this case
        SoundManager.instance.musicSource.UnPause();
    }

    public void EndEdit() {
        if(textField.text.Length == 0) return;
        _hasName = true;
        playerName = textField.text;
        textField.gameObject.SetActive(false);
    }

    private void Update() {
        if(Input.anyKeyDown || _hasNameRun) {
            _hasNameRun = false;
            switch(_state) {
                case 0:
                    if(!_gotToNameScreen) {
                        black.SetActive(true);
                        NextText();
                        const string insertNameHere = "[Insert Name Here]";
                        int i = nameText.text.IndexOf(insertNameHere, StringComparison.Ordinal);
                        _nameTextStart = nameText.text.Substring(0, i);
                        _nameTextEnd = nameText.text.Substring(i + insertNameHere.Length,
                                                                nameText.text.Length - i - insertNameHere.Length);
                        _gotToNameScreen = true;
                    }
                    if(_hasName) {
                        _hasNameRun = true;
                        _state++;
                    }
                    break;
                case 1:
                    StartIntroMusic();
                    black.SetActive(true);
                    NextText();
                    _state++;
                    break;
                case 2:
                    johnny.SetActive(true);
                    johnnyBG.SetActive(true);
                    StartCoroutine(PanSlightly(johnny));
                    NextText();
                    _state++;
                    break;
                case 3:
                    johnnyHappy.SetActive(true);
                    johnnyHappyBG.SetActive(true);
                    StartCoroutine(PanSlightly(johnnyHappy));
                    NextText();
                    _state++;
                    break;
                case 4:
                    teens.SetActive(true);
                    NextText();
                    _state++;
                    break;
                case 5:
                    teensProtag.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensProtag, null));
                    NextText();
                    _state++;
                    break;
                case 6:
                    teensMagic.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensMagic, teensProtag));
                    NextText();
                    _state++;
                    break;
                case 7:
                    teensValentino.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensValentino, teensMagic));
                    NextText();
                    _state++;
                    break;
                case 8:
                    teensCrystal.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensCrystal, teensValentino));
                    NextText();
                    _state++;
                    break;
                case 9:
                    StartCoroutine(FadeInHiglight(null, teensCrystal));
                    NextText();
                    _state++;
                    break;
                case 10:
                    NextText();
                    break;
            }
        }
    }

    private void NextText() {
        if(_state < texts.Count) texts[_state].SetActive(true);
        StartCoroutine(FadeIn(_state < texts.Count ? texts[_state] : null, _state >= 1 ? texts[_state - 1] : null));
    }

    private static IEnumerator PanSlightly(GameObject obj) {
        const float time = 6;
        float timeLeft = time;
        var startPos = obj.transform.position;
        var endPos = obj.transform.position + Vector3.right * 0.6f;
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            obj.transform.position = Vector3.Slerp(endPos, startPos, (time - timeLeft) / time);
            yield return null;
        }
    }

    private static IEnumerator FadeIn(GameObject newObj, GameObject oldObj) {
        const float time = 1;
        float timeLeft = time;
        var text = newObj?.GetComponent<TextMeshProUGUI>();
        var textOld = oldObj?.GetComponent<TextMeshProUGUI>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(text != null) text.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(textOld != null) textOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
        if (text == null)
        {
            Destroy(GameObject.Find("SoundController"));
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }

    private void StartIntroMusic()
    {
        SoundManager.instance.musicSource.Pause();
        SoundManager.instance.soundEffectsSource.PlayOneShot(introMusicPart1);
        waitForIntoMusic(20);
        SoundManager.instance.musicSource.UnPause();
    }

    private static IEnumerator FadeInHiglight(GameObject newObj, GameObject oldObj) {
        const float time = 1;
        float timeLeft = time;
        var sprite = newObj?.GetComponent<SpriteRenderer>();
        var spriteOld = oldObj?.GetComponent<SpriteRenderer>();
        while(timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if(sprite != null) sprite.color = Color.Lerp(Color.clear, Color.white, (time - timeLeft) / time);
            if(spriteOld != null) spriteOld.color = Color.Lerp(Color.white, Color.clear, (time - timeLeft) / time);
            yield return null;
        }
        oldObj?.SetActive(false);
    }
}