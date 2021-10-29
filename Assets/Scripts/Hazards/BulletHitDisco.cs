using System.Collections;
using UnityEngine;

// Randomly rotates and scales a gameobject
// Used on cubes to simulate a blob getting hit
public class BulletHitDisco : BulletHit {
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;
    [SerializeField] private float _scaleRange;

    private Vector3 _originalScale;
    private readonly Vector3 _pointFive = new Vector3(0.5f, 0.5f, 0.5f);

    void Start() {
        StartCoroutine("DestroyGameObjectCoroutine");
        _originalScale = transform.localScale;
    }

    void Update() {
        transform.Rotate(GetRandomVector3(_pointFive, 0.5f) * _speed);
        transform.localScale = GetRandomVector3(_originalScale, _scaleRange);
    }

    private Vector3 GetRandomVector3(Vector3 v, float scale) {
        float x = Random.Range(v.x - scale, v.x + scale);
        float y = Random.Range(v.y - scale, v.y + scale);
        float z = Random.Range(v.z - scale, v.z + scale);

        return new Vector3(x, y, z);
    }

    IEnumerator DestroyGameObjectCoroutine() {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }

    private void OnValidate() {
        if (_duration < 0) {
            _duration = 0;
        } else if (_duration > MaxDuration) {
            _duration = MaxDuration;
            Debug.LogWarning("Duration cannot be more than const MaxDuration, " + 
                "please check script variables in BulletHit and BulletHitDisco");
        }

        if (_speed < 0) {
            _speed = 0;
        }

        if (_scaleRange < 0) {
            _scaleRange = 0;
        }
    }
}
