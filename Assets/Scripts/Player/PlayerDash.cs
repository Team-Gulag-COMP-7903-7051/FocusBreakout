using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour {
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private ParticleSystem _dashVfx;

    private CharacterController _characterController; //For adding velocity to the character
    private PlayerController _playerController; //For grabbing movement velocity
    private PlayerInput _playerInput;
    private InputAction _dash; //Input action binded to Shift
    private float _dashCooldown = 3.0f;
    private float _dashCooldownTime;


    void Start() {
        _characterController = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();
        _dash = _playerInput.actions["Dash"];
    }

    void Update() {
        if (_dash.triggered && Time.time > _dashCooldownTime) {

            _dashCooldownTime = Time.time + _dashCooldown;
            _dashVfx.Play(); // dash vfx effect
            StartCoroutine(Dash());
        }
    }

    //A function to implement the Dash
    IEnumerator Dash() {
        // Debug.Log("Dash dash!"); //Leaving this here for future tweaking/balancing

        float startTime = Time.time;

        while (Time.time < startTime + _dashTime) {
            _characterController.Move(_playerController.Move * _dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
