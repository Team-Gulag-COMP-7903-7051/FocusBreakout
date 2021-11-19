using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {
    [SerializeField] private GameObject _pause;
    private PlayerInput _playerInput;

    private bool _isPaused;
    private InputAction _pauseAction;

    private void Start() {
        _isPaused = false;
        _playerInput = GetComponent<PlayerInput>();
        _pauseAction = _playerInput.actions["Pause"];
    }

    private void Update() {
        if (_pauseAction.triggered) {
            if (_isPaused) {
                Resume();
            } else {
                PauseGame();
            }
        }


    }

    public void Resume() {
        _pause.SetActive(false);
        Time.timeScale = 1f; //activate the game
        _isPaused = false;
    }

    public void PauseGame() {
        _pause.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void Home() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
