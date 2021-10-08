using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {
    [SerializeField]
    private int _health;
    [SerializeField]
    private float _speed;

    public bool IsPlayer { get; }

    public void TakeDamage(int dmg) {
        _health -= dmg;
        if (_health <= 0) {
            Die();
        }
    }
    private void Die() {
        Debug.Log("Died");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Hazard")) {
            _health -= collision.gameObject.GetComponent<Hazard>().Damage;
        }
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
