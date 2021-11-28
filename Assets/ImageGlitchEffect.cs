using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGlitchEffect : MonoBehaviour {
    [SerializeField] private float _minTeleportX;
    [SerializeField] private float _maxTeleportX;
    [SerializeField] private float _minTeleportY;
    [SerializeField] private float _maxTeleportY;
    private RectTransform _image;
    private Vector3 v;
    void Start() {
        _image = GetComponent<RectTransform>();
        float x = Random.Range(_minTeleportX, _maxTeleportX);
        float y = Random.Range(_minTeleportY, _maxTeleportY);
        _image.anchoredPosition = new Vector3(x, y, 0);
    }

    void Update() {
        float x = Random.Range(_minTeleportX, _maxTeleportX);
        float y = Random.Range(_minTeleportY, _maxTeleportY);
        _image.anchoredPosition = new Vector3(x, y, 0);
    }


    IEnumerator StartRandImgCoroutine() {
        while (true) {

        }
    }

    // Return character to what it was originally
    IEnumerator EndRandImgCoroutine(int idx, float min, float max, string text) {
        float time = Random.Range(min, max);
        yield return new WaitForSeconds(time);
    }

    // There is currently a bug where not all of the characters
    // are returned back to normal (usually 1 or 2), this is used
    // as a temporary fix.
    IEnumerator FinalRandImgCoroutine(float time, string text) {
        yield return new WaitForSeconds(time);
    }
}
