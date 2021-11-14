using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blob : MonoBehaviour {
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private Audio[] _audioArray;

    private int _currentHealth;
    void Awake() {
        foreach (Audio s in _audioArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }

        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int dmg) {
        if (dmg <= 0) {
            throw new ArgumentException("Can't deal " + dmg + " damage, needs to be at least 0");
        }
        AudioSource.PlayClipAtPoint(_audioArray[1].Clip, transform.position, _audioArray[1].Volume);
        _currentHealth -= dmg;
        if (_currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Heal(int num) {
        if (num <= 0) {
            throw new ArgumentException("Can't heal " + num + ", needs to be at least 0");
        }
        _currentHealth += num;
        if (_currentHealth > _maxHealth) {
            _currentHealth = _maxHealth;
        }
        
    }

    protected virtual void Die() {
        if (Random.Range(0, 420) == 0) {
            AudioSource.PlayClipAtPoint(_audioArray[0].Clip, transform.position, _audioArray[0].Volume);
        }
        BlobManager.RemoveBlob(this);
        Destroy(gameObject);
    }

    public int CurrentHealth {
        get { return _currentHealth; }
    }

    public int MaxHealth {
        get { return _maxHealth; }
    }

    public float Speed {
        get { return _speed; }
    }

    protected virtual void OnValidate() {
        if (_currentHealth < 1) {
            _currentHealth = 1;
        }

        if (_speed < 1) {
            _speed = 1;
        }
    }
}
