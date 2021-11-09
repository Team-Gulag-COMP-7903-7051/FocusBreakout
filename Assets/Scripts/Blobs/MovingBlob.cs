using System.Collections;
using UnityEngine;

// Default WorldDirection is (0, 0, 0) so they will not move.
// Change WorldDirection when instantiating this blob.
public class MovingBlob : Blob {
    [SerializeField] private float _directionChangeSpeed;

    private Vector3 _worldDirection;
    private CharacterController _controller;
    private Coroutine _currentCoroutine;
    void Start() {
        _controller = GetComponent<CharacterController>();
        _currentCoroutine = StartCoroutine(ReverseDirectionCoroutine());
    }

    void FixedUpdate() {
        _controller.Move(_worldDirection * Speed * Time.fixedDeltaTime);
    }

    private void ReverseDirection() {
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ReverseDirectionCoroutine());
        _worldDirection *= -1;
    }

    IEnumerator ReverseDirectionCoroutine() {
        yield return new WaitForSeconds(_directionChangeSpeed);
        ReverseDirection();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        ReverseDirection();
    }

    public float WorldDirectionX {
        get { return _worldDirection.x; }
        set {
            if (value > 1) {
                _worldDirection.x = 1;
            } else if (value < -1) {
                _worldDirection.x = -1;
            } else {
                _worldDirection.x = value;
            }
        }
    }

    public float WorldDirectionY {
        get { return _worldDirection.y; }
        set {
            if (value > 1) {
                _worldDirection.y = 1;
            } else if (value < -1) {
                _worldDirection.y = -1;
            } else {
                _worldDirection.y = value;
            }
        }
    }

    public float WorldDirectionZ {
        get { return _worldDirection.z; }
        set {
            if (value > 1) {
                _worldDirection.z = 1;
            } else if (value < -1) {
                _worldDirection.z = -1;
            } else {
                _worldDirection.z = value;
            }
        }
    }

    public Vector3 WorldDirection {
        get { return _worldDirection; }
        set {
            WorldDirectionX = value.x;
            WorldDirectionY = value.y;
            WorldDirectionZ = value.z;
        }
    }

    protected override void OnValidate() {
        base.OnValidate();
        if (_directionChangeSpeed < 0) {
            _directionChangeSpeed = 0;
        }
    }
}
