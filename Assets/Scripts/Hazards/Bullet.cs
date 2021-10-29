using UnityEngine;

[RequireComponent(typeof(AudioSource))] // needed for playing audio if object is to be destroyed
public class Bullet : Hazard {
    [SerializeField] private Sound[] _soundArray;

    private CharacterController _controller;
    private float _audioVolume;

    void Awake() {
        _controller = GetComponent<CharacterController>();
        _audioVolume = 0.4f;

        foreach(Sound s in _soundArray) {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void FixedUpdate() {
        _controller.Move(Direction * Time.fixedDeltaTime * Speed);
    }

    protected override void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.collider.CompareTag("Blob")) {
            AudioSource.PlayClipAtPoint(_soundArray[0].Clip, hit.collider.transform.position, _audioVolume);
        } else {
            AudioSource.PlayClipAtPoint(_soundArray[1].Clip, hit.collider.transform.position, _audioVolume);
        }
        
        base.OnControllerColliderHit(hit);
    }
}
