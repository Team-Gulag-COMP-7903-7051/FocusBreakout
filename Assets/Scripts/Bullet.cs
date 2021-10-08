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

        Vector3 move = new Vector3(0, 0, 1);
        controller.Move(move * Time.deltaTime * Speed);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.CompareTag("Blob")) {
            hit.collider.GetComponent<Blob>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
