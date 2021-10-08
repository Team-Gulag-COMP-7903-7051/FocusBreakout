using System;
using UnityEngine;

public class Bullet : Hazard
{
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        controller.Move(Direction * Time.deltaTime * Speed);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.CompareTag("Blob")) {
            hit.collider.GetComponent<Blob>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
