using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public GameObject PauseMenuByEsc;

    public static bool IsPaused;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
            if (IsPaused) {
                Resume();
                SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
            } else {
                PauseGame();
                SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
            }
        } 
    }
    public void Resume() {
        PauseMenuByEsc.SetActive(false);
        Time.timeScale = 1f; //active the game
        IsPaused = false;
    }

    public void PauseGame() {
        PauseMenuByEsc.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Home() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
        SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
    }
}
