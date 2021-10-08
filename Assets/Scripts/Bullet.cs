using System;
using UnityEngine;

public class Bullet : Hazard
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.forward * Speed;
    }

    void OnCollisionEnter(Collision collision) {
        Blob blob = collision.gameObject.GetComponent<Blob>();
        if(blob != null) {
            collision.gameObject.GetComponent<Blob>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
