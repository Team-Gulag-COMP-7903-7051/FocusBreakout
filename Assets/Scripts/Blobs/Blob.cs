using UnityEngine;

[RequireComponent(typeof(AudioSource))] // needed for playing audio if object is to be destroyed
public class Blob : MonoBehaviour {
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private Sound[] _soundArray;

    private AudioSource _audioSource;

    void Awake() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        foreach (Sound s in _soundArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void TakeDamage(int dmg) {
        AudioSource.PlayClipAtPoint(_soundArray[1].Clip, transform.position, 0.5f);
        _health -= dmg;
        if (_health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        if (Random.Range(0, 420) == 0) {
            AudioSource.PlayClipAtPoint(_soundArray[0].Clip, transform.position, 0.1f);
        }
        BlobManager.RemoveBlob(this);
        Destroy(gameObject);
    }

    public int Health {
        get { return _health; }
    }

    public float Speed {
        get { return _speed; }
    }

    private void OnValidate() {
        if (_health < 1) {
            _health = 1;
        }

        if (_speed < 1) {
            _speed = 1;
        }
    }
}
