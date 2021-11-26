using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable] // Allows non-MonoBehaviour class to show up in Inspector
public class Audio : ISerializationCallbackReceiver {
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] [Range(0f, 1f)] private float _volume;
    [SerializeField] private bool _loop;

/*    [SerializeField] [Range(0f, 1f)] private float _spatialBlend;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private AudioRolloffMode _rolloff;*/

    private AudioSource _source;

    public AudioSource Source {
        get { return _source; }
        set { _source = value; }
    }

    public AudioMixerGroup AudioMixerGroup {
        get { return _audioMixerGroup; }
        set { _audioMixerGroup = value; }
    }

/*    public AudioSource Source {
        get { return _source; }
        set {
            _source = value;
            _source.name = _name;
            _source.clip = _clip;
            _source.volume = _volume;
            _source.spatialBlend = _spatialBlend;
            _source.minDistance = _minDistance;
            _source.maxDistance = _maxDistance;
            _source.rolloffMode = _rolloff;
        }
    }*/

    public AudioClip Clip {
        get { return _clip; }
    }

    public string Name {
        get { return _name; }
    }

    public float Volume {
        get { return _volume; }
        set { 
            if (value < 0 || value > 1) {
                throw new ArgumentOutOfRangeException("Volume needs to be between 0 and 1 (both inclusive).");
            }
        }
    }

    public bool Loop {
        get { return _loop; }
        set { _loop = value; }
    }

    public void Play() {
        _source.Play();
    }

    private void OnValidate() {
/*        if (_minDistance < 0) {
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
        }*/
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize() {
        OnValidate();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }
}
