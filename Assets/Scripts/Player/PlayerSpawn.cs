using System.Collections;
using UnityEngine;

// Lerp, Slerp, and SmoothStep are not used becuase they are unable
// to start with a scale of 0 due to the short timeframe of the effect.
public class PlayerSpawn : MonoBehaviour {
    [SerializeField] private GameObject _player;
    [SerializeField] private float _rotationSpeed = 2500;
    [SerializeField] private float _scaleOvershoot = 0.5f;

    private MeshRenderer _bodyRenderer;
    private MeshRenderer _eyeRenderer;
    private float _playerScale;
    private float _scale;

    void Start() {
        transform.localScale = Vector3.zero;
        _playerScale = _player.transform.localScale.x;
        _bodyRenderer = _player.GetComponent<MeshRenderer>();
        _eyeRenderer = _player.transform.Find("Player Front").GetComponent<MeshRenderer>();

        _bodyRenderer.enabled = false;
        _eyeRenderer.enabled = false;
        AudioManager.Instance.Play("PlayerSpawn");
        StartCoroutine(GrowthCoroutine(0.01f, 0.1f));
    }

    private void Update() {
        transform.position = _player.transform.position;
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.Self);
    }

    IEnumerator GrowthCoroutine(float time, float step) {
        while (_scale < _playerScale + _scaleOvershoot) {
            yield return new WaitForSeconds(time);
            _scale += step;
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }

        _bodyRenderer.enabled = true;
        _eyeRenderer.enabled = true;

        while (_scale > _playerScale) {
            yield return new WaitForSeconds(time);
            _scale -= step;
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }

        AudioManager.Instance.Stop("PlayerSpawn");
        gameObject.SetActive(false);
    }

    private void OnValidate() {
        if (_rotationSpeed < 0) {
            _rotationSpeed = 0;
        }

        if (_scaleOvershoot < 0) {
            _scaleOvershoot = 0;
        }
    }
}
