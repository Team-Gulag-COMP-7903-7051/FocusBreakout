using UnityEngine;

// Still in super early development.
public class AudioManager : MonoBehaviour {
    [SerializeField] private Sound[] _soundArray;

    private AudioSource _audioSource;

    void Awake() {
        _audioSource = GetComponent<AudioSource>();
        foreach (Sound s in _soundArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayDeathScream(Vector3 pos) {
        AudioSource.PlayClipAtPoint(_soundArray[0].Clip, pos);
    }
}
