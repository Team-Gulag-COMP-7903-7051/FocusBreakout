using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Vector3 _direction;

    public int Damage {
        get { return _damage; }
    }

    public float Speed {
        get { return _speed; }
    }

    public Vector3 Direction {
        get { return _direction;  }
        set { _direction = value; }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.CompareTag("Blob")) {
            hit.collider.GetComponent<Blob>().TakeDamage(Damage);
        }

        Destroy(gameObject);
    }

    void OnValidate() {
        if (_damage < 1) {
            _damage = 1;
        }

        if (_speed < 1) {
            _speed = 1;
        }
    }
}
