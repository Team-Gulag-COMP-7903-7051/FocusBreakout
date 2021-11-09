using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// They move n' groove
public class MovingBlob : Blob {
    [SerializeField] private float _directionChangeSpeed;
    [SerializeField] private WorldDirection _worldDirection;
/*    [SerializeField] private LinearDirection _x;
    [SerializeField] private LinearDirection _y;
    [SerializeField] private LinearDirection _z;*/

    private Vector3 _direction;
    private CharacterController _controller;
    private Coroutine _currentCoroutine;
    void Start() {
        _controller = GetComponent<CharacterController>();
        ChangeWorldDirection(_worldDirection);

        if (Random.Range(0, 2) == 0) {
            _direction *= -1;
        } 

        _currentCoroutine = StartCoroutine(ReverseDirectionCoroutine());
    }

    void FixedUpdate() {
        _controller.Move(_direction * Speed * Time.fixedDeltaTime);
    }

    private void ReverseDirection() {
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ReverseDirectionCoroutine());
        _direction *= -1;
    }

    public void ChangeWorldDirection(WorldDirection dir) {
        switch (dir) {
            case WorldDirection.X:
                _direction = Vector3.right;
                break;
            case WorldDirection.Y:
                _direction = Vector3.up;
                break;
            case WorldDirection.Z:
                _direction = Vector3.forward;
                break;
            default:
                throw new ArgumentException("Unknown 'Direction' enum value: " + dir);
        }
    }

    IEnumerator ReverseDirectionCoroutine() {
        yield return new WaitForSeconds(_directionChangeSpeed);
        ReverseDirection();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        ReverseDirection();
    }

    private void OnValidate() {
        if (_directionChangeSpeed < 0) {
            _directionChangeSpeed = 0;
        }
    }
}

public enum WorldDirection {
    X,
    Y,
    Z
}

public enum LinearDirection {
    Off,
    Pos,
    Neg,
    Rand
}
