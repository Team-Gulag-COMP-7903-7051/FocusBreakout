using System.Collections;
using UnityEngine;


public class ImageGlitchEffect : MonoBehaviour {
    [SerializeField] private float _minTotalTime = 0.5f;
    [SerializeField] private float _maxTotalTime = 1.5f;

    [SerializeField] private float _minTeleportX;
    [SerializeField] private float _maxTeleportX;
    [SerializeField] private float _minTeleportY;
    [SerializeField] private float _maxTeleportY;

    private RectTransform _imagePosition;
    private Coroutine _coroutine;

    void Start() {
        _imagePosition = GetComponent<RectTransform>();

        _coroutine = StartCoroutine(StartImgGlitchCoroutine());
        StartCoroutine(EndImgGlitchCoroutine());
    }

    IEnumerator StartImgGlitchCoroutine() {
        while (true) {
            float time = Random.Range(0f, 0.05f);
            float x = Random.Range(_minTeleportX, _maxTeleportX);
            float y = Random.Range(_minTeleportY, _maxTeleportY);

            _imagePosition.anchoredPosition = new Vector3(x, y, 0);
            yield return new WaitForSeconds(time);
        }
    }

    // Return character to what it was originally
    IEnumerator EndImgGlitchCoroutine() {
        float time = Random.Range(0f, 1f);
        yield return new WaitForSeconds(time);
        StopCoroutine(_coroutine);
    }

    // There is currently a bug where not all of the characters
    // are returned back to normal (usually 1 or 2), this is used
    // as a temporary fix.
    IEnumerator FinalImgGlitchCoroutine(float time, string text) {
        yield return new WaitForSeconds(time);
    }
}
