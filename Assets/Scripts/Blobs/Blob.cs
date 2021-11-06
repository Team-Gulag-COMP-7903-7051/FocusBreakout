using UnityEngine;

public class Blob : MonoBehaviour {
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private Audio[] _audioArray;

    void Awake() {
        foreach (Audio s in _audioArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void TakeDamage(int dmg) {
        AudioSource.PlayClipAtPoint(_audioArray[1].Clip, transform.position, _audioArray[1].Volume);
        _health -= dmg;
        if (_health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        if (Random.Range(0, 420) == 0) {
            AudioSource.PlayClipAtPoint(_audioArray[0].Clip, transform.position, _audioArray[0].Volume);
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

    protected virtual void OnValidate() {
        if (_health < 1) {
            _health = 1;
        }

        if (_speed < 1) {
            _speed = 1;
        }
    }
}
