using System.Collections;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {
    [SerializeField] private GameObject _player;
    [SerializeField] private float _spawnTime = 0.5f;

    private float _playerScale;
    private float _currentTime;

    void Start() {
        transform.localScale = Vector3.zero;
        transform.position = _player.transform.position;

        _playerScale = _player.transform.localScale.x;
        _currentTime = 0;
    }

    private void Update() {
        if (_currentTime < _spawnTime) {
            float scale = Mathf.Lerp(0, _playerScale, _currentTime / _spawnTime);
            transform.localScale = new Vector3(scale, scale, scale);
            _currentTime += Time.deltaTime;
        } else if (_currentTime < _spawnTime * 2) {
            float scale = Mathf.Lerp(_playerScale * 2, 0, _currentTime / (_spawnTime * 2));
            transform.localScale = new Vector3(scale, scale, scale);
            _currentTime += Time.deltaTime;
        } else {
            _currentTime = 0;
        }
    }

    private void OnValidate() {
        if (_spawnTime < 0) {
            _spawnTime = 0;
        }
    }
}
