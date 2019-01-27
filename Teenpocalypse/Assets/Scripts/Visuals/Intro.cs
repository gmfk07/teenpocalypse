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

    private int _state;
    private bool _gotToNameScreen;
    private bool _hasNameRun;
    private bool _hasName;
    private TextMeshProUGUI _nameText;
    private string _nameTextStart;
    private string _nameTextEnd;
    [HideInInspector] public string playerName;

    public void OnEdit() {
        _nameText.text = _nameTextStart + textField.text + _nameTextEnd;
    }

    public void EndEdit() {
        _hasName = true;
        playerName = textField.text;
    }

    private void Update() {
        if(Input.anyKeyDown || _hasNameRun) {
            _hasNameRun = false;
            switch(_state) {
                case 0:
                    black.SetActive(true);
                    NextText();
                    _state++;
                    break;
                case 1:
                    johnny.SetActive(true);
                    johnnyBG.SetActive(true);
                    StartCoroutine(PanSlightly(johnny));
                    NextText();
                    _state++;
                    break;
                case 2:
                    johnnyHappy.SetActive(true);
                    johnnyHappyBG.SetActive(true);
                    StartCoroutine(PanSlightly(johnnyHappy));
                    NextText();
                    _state++;
                    break;
                case 3:
                    teens.SetActive(true);
                    NextText();
                    _state++;
                    break;
                case 4:
                    if(!_gotToNameScreen) {
                        teensProtag.SetActive(true);
                        StartCoroutine(FadeInHiglight(teensProtag, null));
                        NextText();
                        _nameText = texts[_state].GetComponent<TextMeshProUGUI>();
                        const string insertNameHere = "[Insert Name Here]";
                        int i = _nameText.text.IndexOf(insertNameHere, StringComparison.Ordinal);
                        _nameTextStart = _nameText.text.Substring(0, i);
                        _nameTextEnd = _nameText.text.Substring(i + insertNameHere.Length,
                                                                _nameText.text.Length - i - insertNameHere.Length);
                        _gotToNameScreen = true;
                    }
                    if(_hasName) {
                        _hasNameRun = true;
                        _state++;
                    }

                    break;
                case 5:
                    teensMagic.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensMagic, teensProtag));
                    NextText();
                    _state++;
                    break;
                case 6:
                    teensValentino.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensValentino, teensMagic));
                    NextText();
                    _state++;
                    break;
                case 7:
                    teensCrystal.SetActive(true);
                    StartCoroutine(FadeInHiglight(teensCrystal, teensValentino));
                    NextText();
                    _state++;
                    break;
                case 8:
                    StartCoroutine(FadeInHiglight(null, teensCrystal));
                    NextText();
                    _state++;
                    break;
                case 9:
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
        if(text == null)
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
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