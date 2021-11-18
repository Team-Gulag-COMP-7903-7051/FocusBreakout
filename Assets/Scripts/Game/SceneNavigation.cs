using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {
    // For level selection
    public void LoadScene(string scene) {
        StopAllCoroutines();
        SceneManager.LoadScene(scene);
        //SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
    }

    // For play button
    public void LoadHighestLevel() {
        int num = SaveManager.GetMainLevelsCompleted();
        StopAllCoroutines();
        SceneManager.LoadScene("Level" + num);
        //SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
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
