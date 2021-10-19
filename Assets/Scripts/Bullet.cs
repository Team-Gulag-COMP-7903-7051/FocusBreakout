using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] // needed for playing audio if object is to be destroyed
public class Bullet : Hazard
{
    [SerializeField] private Sound[] _soundArray;

    private CharacterController _controller;

    void Awake() {
        _controller = GetComponent<CharacterController>();

        foreach(Sound s in _soundArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }
        
    }

    private void Update() {
        _controller.Move(Direction * Time.deltaTime * Speed);
    }

    protected override void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.CompareTag("Blob")) {
            AudioSource.PlayClipAtPoint(_soundArray[0].Clip, hit.collider.transform.position);
        } else {
            AudioSource.PlayClipAtPoint(_soundArray[1].Clip, hit.collider.transform.position);
        }
        
        base.OnControllerColliderHit(hit);
    }
}
