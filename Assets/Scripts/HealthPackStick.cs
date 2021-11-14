using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// A stick that requires a parent class to work.
public class HealthPackStick : MonoBehaviour {
    // for width and length
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [Space(Constants.InpectorSpaceAttribute)]

    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;
    [Space(Constants.InpectorSpaceAttribute)]

    [SerializeField] [Range(0, 1)] private float _minOpacity;
    [SerializeField] [Range(0, 1)] private float _maxOpacity;
    [Space(Constants.InpectorSpaceAttribute)]

    [SerializeField] private float _minTeleportRadius;
    [SerializeField] private float _maxTeleportRadius;

    [SerializeField] private float _minTeleportTime;
    [SerializeField] private float _maxTeleportTime;

    private Material _material;
    // Used in GetRandomFloat()
    private const int _bonusRarity = 75;
    private const float _bonusMultiplier = 3;
    void Start() {
        if (transform.parent == null) {
            throw new ArgumentException("HealthPackStick needs to have a parent.");
        }

        _material = GetComponent<Renderer>().material;
        float num = Random.Range(_minOpacity, _maxOpacity);
        Color c = _material.color;
        
        if (Random.Range(0, 4) == 0) {
            c = Color.black;
            num += 0.25f;
        }
        c.a = num;
        _material.color = c;

        transform.localScale = GetRanomScale();
        StartCoroutine(TeleportCoroutine());
    }

    private Vector3 GetRanomScale() {
        float x = Random.Range(_minSize, _maxSize);
        float y = Random.Range(_minHeight, _maxHeight);
        float z = Random.Range(_minSize, _maxSize);

        return new Vector3(x, y, z);
    }

    IEnumerator TeleportCoroutine() {
        while (true) {
            float time = Random.Range(_minTeleportTime, _maxTeleportTime);
            float x = GetRandomFloat(_minTeleportRadius, _maxTeleportRadius);
            float y = GetRandomFloat(_minTeleportRadius, _maxTeleportRadius);
            float z = GetRandomFloat(_minTeleportRadius, _maxTeleportRadius);

            transform.localPosition = new Vector3(x, y, z);

            yield return new WaitForSeconds(time);
        }
    }

    // Returns a random float between min and max (both inclusive)
    // with a 50% chance its sign will be flipped + Bonus Values.
    private float GetRandomFloat(float min, float max) {
        float num = Random.Range(min, max);
        if (Random.Range(0, 2) == 0) {
            num *= -1;
        }

        // Bonus Values
        if (Random.Range(0, _bonusRarity) == 0) {
            num *= _bonusMultiplier;
        }

        return num;
    }

    private void OnValidate() {
        // Size
        if (_minSize < 0) {
            _minSize = 0;
        }
        if (_maxSize < _minSize) {
            _maxSize = _minSize;
        }
        // Height
        if (_minHeight < 0) {
            _minHeight = 0;
        }
        if (_maxHeight < _minHeight) {
            _maxHeight = _minHeight;
        }
        // Opacity
        if (_maxOpacity < _minOpacity) {
            _maxOpacity = _minOpacity;
        }
        // TP Radius
        if (_minTeleportRadius < 0) {
            _minTeleportRadius = 0;
        }
        if (_maxTeleportRadius < _minTeleportRadius) {
            _maxTeleportRadius = _minTeleportRadius;
        }
        // TP Time
        if (_minTeleportTime < 0) {
            _minTeleportTime = 0;
        }
        if (_maxTeleportTime < _minTeleportTime) {
            _maxTeleportTime = _minTeleportTime;
        }
    }
}
