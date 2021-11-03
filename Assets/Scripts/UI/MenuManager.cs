using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenuByEsc;

    public static bool IsPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else {
                PauseGame();
            }
        } 
    }
    public void Resume()
    {
        PauseMenuByEsc.SetActive(false);
        Time.timeScale = 1f; //active the game
        IsPaused = false;
    }

    public void PauseGame() {
        PauseMenuByEsc.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
