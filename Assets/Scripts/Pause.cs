using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;

    public void pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //freeze the game
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //active the game
    }

    public void home(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
