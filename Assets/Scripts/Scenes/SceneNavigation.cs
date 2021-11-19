using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {
    // For level selection
    public void LoadScene(string scene) {
        StopAllCoroutines();
        SceneManager.LoadScene(scene);
    }

    // For play button
    public void LoadHighestLevel() {
        int num = SaveManager.GetMainLevelsCompleted();

        if (num >= Constants.NumOfMainLevels) {
            num = Constants.NumOfMainLevels - 1;
        }

        StopAllCoroutines();
        SceneManager.LoadScene("Level" + num);
    }

    // For play again button
    public void LoadCurrentLevel() {
        StopAllCoroutines();
        SceneManager.LoadScene("Level" + DataManager.CurrentLevel);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
