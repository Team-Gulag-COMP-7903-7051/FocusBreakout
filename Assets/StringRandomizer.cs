using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;

// Have you seen DisneyPlus' Loki
public class StringRandomizer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // How long this effect lasts
    [SerializeField] private float _minTotalTime = 0;
    [SerializeField] private float _maxTotalTime = 1f;
    // How long for a char to change
    [SerializeField] private float _minChangeTime = 0f;
    [SerializeField] private float _maxChangeTime = 0.1f;
    // hmmmm
    [SerializeField] private int _idkWhatToNameThisVariable = 5;
    // Using unicode decimal format
    private const int _minUnicodeValue = 33;
    private const int _maxUnicodeValue = 126; // use 126 for no log warnings

    private string _originalText;
    private int _originalLength; // is this necessary? _originalText.Length works too
    private TextMeshProUGUI _text;
    private StringBuilder _stringBuilder;
    private Coroutine[] _initialRandCoroutineArray;
    private bool _isHover;
    private Coroutine[] _hoverRandCoroutineArray;

    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        _originalText = _text.text;
        _originalLength = _text.text.Length;
        _stringBuilder = new StringBuilder(_originalText, _originalLength);
        _initialRandCoroutineArray = new Coroutine[_originalLength];
        _hoverRandCoroutineArray = new Coroutine[_idkWhatToNameThisVariable];
        _isHover = false;

        for (int i = 0; i < _originalLength; i++) {
            _initialRandCoroutineArray[i] = StartCoroutine(StartRandCharCoroutine(i));
            StartCoroutine(EndRandCharCoroutine(i, _minTotalTime, _maxTotalTime));
        }

        StartCoroutine(FinalRandCharCoroutine(_maxTotalTime));

        for (int i = 0; i < _idkWhatToNameThisVariable; i++) {
            _hoverRandCoroutineArray[i] = StartCoroutine(MinimalRandCharCoroutine());
        } 
    }

    // Randomly change a specific character in a string
    IEnumerator StartRandCharCoroutine(int idx) {
        while (true) {
            float time = Random.Range(0f, 0.5f);

            _stringBuilder[idx] = GetRandomChar();
            _text.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(time);
        }
    }

    // Return character to what it was originally
    IEnumerator EndRandCharCoroutine(int idx, float min, float max) {
        float time = Random.Range(min, max);
        yield return new WaitForSeconds(time);
        StopCoroutine(_initialRandCoroutineArray[idx]);
        _stringBuilder[idx] = _originalText[idx];
    }

    // There is currently a bug where not all of the characters
    // are returned back to normal (usually 1 or 2), this is used
    // as a temporary fix.
    IEnumerator FinalRandCharCoroutine(float time) {
        yield return new WaitForSeconds(time);
        _text.text = _originalText;
    }

    // Occassionally switch a char to another random char and back.
    IEnumerator MinimalRandCharCoroutine() {
        while (true) {
            int idx = Random.Range(0, _originalLength);
            float glitchTime = Random.Range(0f, 0.25f);
            float restTime = Random.Range(0f, 3f);

            _stringBuilder[idx] = GetRandomChar();
            _text.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(glitchTime);

            _stringBuilder[idx] = _originalText[idx]; // reset StringBuilder for next iteration
            _text.text = _originalText;
            if (!_isHover) {
                yield return new WaitForSeconds(restTime);
            }            
        }
    }

    private char GetRandomChar() {
        int num = Random.Range(_minUnicodeValue, _maxUnicodeValue + 1);
        return Convert.ToChar(num);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _isHover = true;
        // restart coroutines
        for (int i = 0; i < _hoverRandCoroutineArray.Length; i++) {
            StopCoroutine(_hoverRandCoroutineArray[i]);
            _hoverRandCoroutineArray[i] = StartCoroutine(MinimalRandCharCoroutine());
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        _isHover = false;
    }

    private void OnValidate() {
        // Total Time
        if (_minTotalTime < 0) {
            _minTotalTime = 0;
        }
        if (_maxTotalTime < _minTotalTime) {
            _maxTotalTime = _minTotalTime;
        }
        // Change Time
        if (_minChangeTime < 0) {
            _minChangeTime = 0;
        }
        if (_maxChangeTime < _minChangeTime) {
            _maxChangeTime = _minChangeTime;
        }
        // some var
        if (_idkWhatToNameThisVariable < 0) {
            _idkWhatToNameThisVariable = 0;
        }
    }
}
