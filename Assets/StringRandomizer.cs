using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Collections;
using Random = UnityEngine.Random;

// Have you seen Disney+ Loki
public class StringRandomizer : MonoBehaviour {
    // How long this effect lasts
    [SerializeField] private float _minTotalTime = 0;
    [SerializeField] private float _maxTotalTime = 1f;
    // How long for a char to change
    [SerializeField] private float _minChangeTime = 0f;
    [SerializeField] private float _maxChangeTime = 0.1f;
    // Using unicode decimal format
    private const int _minUnicodeValue = 33;
    private const int _maxUnicodeValue = 126; 

    private TextMeshProUGUI _text;
    private string _originalText;
    private int _originalLength; // is this necessary? _originalText.Length works too
    private StringBuilder _stringBuilder;
    private Coroutine[] _coroutineArray;

    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        _originalText = _text.text;
        _originalLength = _text.text.Length;
        _stringBuilder = new StringBuilder(_originalText, _originalLength);
        _coroutineArray = new Coroutine[_originalLength];

        for (int i = 0; i < _originalLength; i++) {
            _coroutineArray[i] = StartCoroutine(StartRandCharCoroutine(i));
            StartCoroutine(EndRandCharCoroutine(i, _minTotalTime, _maxTotalTime));
        }

        StartCoroutine(FinalRandCharCoroutine(_maxTotalTime));
    }

    // Randomly change a specific character in a string
    IEnumerator StartRandCharCoroutine(int idx) {
        while (true) {
            float time = Random.Range(0f, 0.5f);
            int num = Random.Range(_minUnicodeValue, _maxUnicodeValue + 1);
            char c = Convert.ToChar(num);

            _stringBuilder[idx] = c;
            _text.text = _stringBuilder.ToString();
            yield return new WaitForSeconds(time);
        }
    }

    // Return character to what it was originally
    IEnumerator EndRandCharCoroutine(int idx, float min, float max) {
        float time = Random.Range(min, max);
        yield return new WaitForSeconds(time);
        StopCoroutine(_coroutineArray[idx]);
        _stringBuilder[idx] = _originalText[idx];
    }

    // There is currently a bug where not all of the characters
    // are returned back to normal (usually 1 or 2), this is used
    // as a temporary fix.
    IEnumerator FinalRandCharCoroutine(float time) {
        yield return new WaitForSeconds(time);
        _text.text = _originalText;
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
    }
}
