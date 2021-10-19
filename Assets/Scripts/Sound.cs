using UnityEngine;

[System.Serializable] // Allows non-MonoBehaviour class to show up in Inspector
public class Sound : ISerializationCallbackReceiver {
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] [Range(0f, 1f)] private float _volume;
    [SerializeField] [Range(0f, 1f)] private float _spatialBlend;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    private AudioSource _audioSource;

    public AudioSource AudioSource {
        get { return _audioSource; }
        set {
            _audioSource = value;
            _audioSource.name = _name;
            _audioSource.clip = _clip;
            _audioSource.volume = _volume;
            _audioSource.spatialBlend = _spatialBlend;
            _audioSource.rolloffMode = AudioRolloffMode.Linear;
            _audioSource.minDistance = _minDistance;
            _audioSource.maxDistance = _maxDistance;
        }
    }

    public AudioClip Clip {
        get { return _clip; }
    }

    public string Name {
        get { return _name; }
    }

    private void OnValidate() {
        if (_minDistance < 0) {
            _minDistance = 0;
        }
        if (_minDistance > _maxDistance) {
            _minDistance = _maxDistance;
            Debug.LogWarning("MinDistance must be smaller than MaxDistance");
        }

        if (_maxDistance < 0) {
            _maxDistance = 0;
        }
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize() {
        OnValidate();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }
}
