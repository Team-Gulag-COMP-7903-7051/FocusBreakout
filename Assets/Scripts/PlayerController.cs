using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 10f; 
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float jumpHeight = 1f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction movement;
    private InputAction jump;
    private Transform cameraTransform;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private void Start() {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        movement = playerInput.actions["Movement"];
        jump = playerInput.actions["Jump"];
    }

    void Update() {
        // no idea what grounded or playerVelocity does yet.
        // grounded something to do with whether or not u touch object with "ground" tag
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        // Player movement
        Vector2 input = movement.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * moveSpeed);

        // Player jump
        if (jump.triggered && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Constants.Gravity);
        }
        playerVelocity.y += Constants.Gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate player towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Ensures values in PlayerController's Inspector don't go below 0
    private void OnValidate() {
        if (moveSpeed < 0) {
            moveSpeed = 0;
        }
        if (rotationSpeed < 0) {
            rotationSpeed = 0;
        }
        if (jumpHeight < 0) {
            jumpHeight = 0;
        }
    }
}