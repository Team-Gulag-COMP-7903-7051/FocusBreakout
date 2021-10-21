using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    public Vector3 Move;

    [SerializeField] private float _jumpHeight = 1f;

    private CharacterController _controller;
    private PlayerInput _playerInput;
    private InputAction _movement;
    private InputAction _jump;
    private Transform _cameraTransform;
    private float _playerSpeed;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private void Start() {
        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerSpeed = GetComponent<Player>().Speed;
        _cameraTransform = Camera.main.transform;
        _movement = _playerInput.actions["Movement"];
        _jump = _playerInput.actions["Jump"];
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
        Vector2 input = _movement.ReadValue<Vector2>();
        Move = new Vector3(input.x, 0, input.y);
        Move = Move.x * _cameraTransform.right.normalized + Move.z * _cameraTransform.forward.normalized;
        Move.y = 0f;
        _controller.Move(Move * Time.fixedDeltaTime * _playerSpeed);

        // Player jump
        if (_jump.triggered && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * Constants.Gravity);
        }
        _playerVelocity.y += Constants.Gravity * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _playerSpeed * Time.fixedDeltaTime);
    }

    private void OnValidate() {
        if (_jumpHeight < 0) {
            _jumpHeight = 0;
        }
    }
}