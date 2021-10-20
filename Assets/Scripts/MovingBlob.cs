using System.Collections;
using UnityEngine;

// They move n' groove
public class MovingBlob : Blob {
    [SerializeField] private float _directionChangeSpeed;
    private Vector3 _direction;
    private CharacterController _controller;
    private Coroutine _currentCoroutine;
    void Start() {
        _controller = GetComponent<CharacterController>();
        if (Random.Range(0, 2) == 0) {
            _direction = Vector3.right;
        } else {
            _direction = Vector3.left;
        }

        _currentCoroutine = StartCoroutine(ChangeDirectionCoroutine());
    }

    void Update() {
        _controller.Move(_direction * Speed * Time.deltaTime);
    }

    private void ChangeDirection() {
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ChangeDirectionCoroutine());
        _direction *= -1;
    }

    IEnumerator ChangeDirectionCoroutine() {
        yield return new WaitForSeconds(_directionChangeSpeed);
        ChangeDirection();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        ChangeDirection();
    }

    private void OnValidate() {
        if (_directionChangeSpeed < 0) {
            _directionChangeSpeed = 0;
        }
    }
}
