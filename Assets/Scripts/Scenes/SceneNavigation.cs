using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {
    // Play button
    public void LoadHighestLevel() {
        LoadLevel(SaveManager.GetMainLevelsCompleted());
    }

    // Next level button
    public void LoadNextLevel() {
        LoadLevel(DataManager.CurrentLevel + 1);
    }

    // Play again button
    public void LoadCurrentLevel() {
        LoadLevel(DataManager.CurrentLevel);
    }

    private void LoadLevel(int level) {
        if (level >= Constants.NumOfMainLevels) {
            level = Constants.NumOfMainLevels - 1;
        }
        LoadScene("Level" + level);
    }

    public void LoadScene(string name) {
        StopAllCoroutines();
        SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
