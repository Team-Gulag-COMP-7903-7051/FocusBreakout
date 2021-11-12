using UnityEngine;

[System.Serializable] // Allows non-MonoBehaviour class to show up in Inspector
public class Audio : ISerializationCallbackReceiver {
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] [Range(0f, 1f)] private float _volume;
    [SerializeField] [Range(0f, 1f)] private float _spatialBlend;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private AudioRolloffMode _rolloff;

    private AudioSource _audioSource;

    public AudioSource AudioSource {
        get { return _audioSource; }
        set {
            _audioSource = value;
            _audioSource.name = _name;
            _audioSource.clip = _clip;
            _audioSource.volume = _volume;
            _audioSource.spatialBlend = _spatialBlend;
            _audioSource.minDistance = _minDistance;
            _audioSource.maxDistance = _maxDistance;
            _audioSource.rolloffMode = _rolloff;
        }
    }

    public AudioClip Clip {
        get { return _clip; }
    }

    public string Name {
        get { return _name; }
    }

    public float Volume {
        get { return _volume; }
    }

    public void Play() {
        _audioSource.Play();
    }

    private void OnValidate() {
        if (_minDistance < 0) {
            _minDistance = 0;
        }

        if (_maxDistance < 0) {
            _maxDistance = 0;
        }

        if (_maxDistance < _minDistance) {
            _maxDistance = _minDistance;
            Debug.LogWarning("Sound MaxDistance must be larger than MinDistance");
        }

        if (_rolloff == AudioRolloffMode.Custom) {
            Debug.LogError("Audio Rolloff 'Custom' is not available, sorry I " +
                "don't know how to get rid of it :c \nRolloff changed to Linear");
            _rolloff = AudioRolloffMode.Linear;
        }
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize() {
        OnValidate();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }
}
