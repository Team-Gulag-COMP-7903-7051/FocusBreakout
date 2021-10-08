using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _speed = 1;

    public int Damage {
        get { return _damage; }
    }

    public float Speed {
        get { return _speed; }
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
