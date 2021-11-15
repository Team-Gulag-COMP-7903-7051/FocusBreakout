using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GlitchStick))]
public class GlitchStick : MonoBehaviour {
    // Used if there are multiple GlitchSticks the GlitchBase needs to choose from.
    // Ex: if GlitchStick1 has Priority = 1 and GlitchStick2 has Priority = 5 then
    // GlitchStick1 will have a 1/6 and GlitchStick2 will have a 5/6 chance of being spawned.
    [SerializeField] private int _probability;
    [Space(Constants.InpectorSpaceAttribute)]

    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _minLength;
    [SerializeField] private float _maxLength;
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
    // Used in GetRandomFloat() to add some extra randomness with teleporting sticks.
    // BonusRarity is the probability calculated via (1/BonusRarity).
    [SerializeField] private int _bonusRarity;
    [SerializeField] private float _bonusMultiplier;

    private Material _material;
    #region GlitchColor Struct
    // Currently NOT being used, potential future implementation
    // Alpha in Material is overridden by Min and Max Opacity
    [System.Serializable]
    private struct GlitchMaterial {
        [SerializeField] private int _probability;
        [SerializeField] private Material _material;
        [SerializeField] [Range(0, 1)] private float _minOpacity;
        [SerializeField] [Range(0, 1)] private float _maxOpacity;

        public int Probability { get; }
        public Material Material { get; }
        public float MinOpacity { get; }
        public float MaxOpacity { get; }
    }
    #endregion

    void Start() {
        if (transform.parent == null) {
            throw new ArgumentException("GlitchStick needs to have a parent.");
        }

        _material = GetComponent<Renderer>().material;
        Color color = _material.color;
        float num = Random.Range(_minOpacity, _maxOpacity);

        color.a = num;
        _material.color = color;

        transform.localScale = GetRanomScale();
        StartCoroutine(TeleportCoroutine());
    }

    private Vector3 GetRanomScale() {
        float x = Random.Range(_minWidth, _maxWidth);
        float y = Random.Range(_minHeight, _maxHeight);
        float z = Random.Range(_minLength, _maxLength);

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

    public int Probability { 
        get { return _probability; }
    }

    private void OnValidate() {
        // Priority
        if (_probability < 1) {
            _probability = 1;
        }
        // Width and Length
        if (_minWidth < 0) {
            _minWidth = 0;
        }
        if (_maxWidth < _minWidth) {
            _maxWidth = _minWidth;
        }
        if (_minLength < 0) {
            _minLength = 0;
        }
        if (_maxLength < _minLength) {
            _maxLength = _minLength;
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
        // Bonus Values
        if (_bonusRarity < 0) {
            _bonusRarity = 0;
        }
        if (_bonusMultiplier < 1.1) {
            _bonusMultiplier = 1.1f;
        }
    }
}