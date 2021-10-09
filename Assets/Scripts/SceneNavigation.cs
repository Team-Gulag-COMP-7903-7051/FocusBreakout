using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void loadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void quitGame() {
        Application.Quit();
    }
}
