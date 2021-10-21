using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float _jumpHeight = 1f;

    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _movementAction;
    private InputAction _jumpAction;
    private Transform _cameraTransform;
    private Vector3 _move;
    private Vector3 _playerVelocity;
    private float _playerSpeed;
    private bool _groundedPlayer;

    private void Start() {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerSpeed = GetComponent<Player>().Speed;
        _cameraTransform = Camera.main.transform;
        _movementAction = _playerInput.actions["Movement"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    void Update() {
        // no idea what grounded or playerVelocity does yet.
        // grounded something to do with whether or not u touch object with "ground tag"
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0) {
            _playerVelocity.y = 0f;
        }
    }

    private void FixedUpdate() {
        // Player movement
        Vector2 input = _movementAction.ReadValue<Vector2>();
        _move = new Vector3(input.x, 0, input.y);
        _move = _move.x * _cameraTransform.right.normalized + _move.z * _cameraTransform.forward.normalized;
        _move.y = 0f;
        _controller.Move(_move * Time.fixedDeltaTime * _playerSpeed);

        // Player jump
        if (_jumpAction.triggered && _groundedPlayer) {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * Constants.Gravity);
        }
        _playerVelocity.y += Constants.Gravity * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _playerSpeed * Time.fixedDeltaTime);
    }

    public Vector3 Move {
        get { return _move; }
    }

    private void OnValidate() {
        if (_jumpHeight < 0) {
            _jumpHeight = 0;
        }
    }
}