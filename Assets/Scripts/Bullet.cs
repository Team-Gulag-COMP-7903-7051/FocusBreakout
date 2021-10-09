using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Hazard
{
    [SerializeField]
    private float _speed = 50;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.forward * _speed;
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            Debug.Log("Lol get fucked " + collision.gameObject.transform.position);
        }
        Destroy(gameObject);
    }

    void OnValidate() {
        if(_speed < 0) {
            _speed = 0;
        }
    }

}
