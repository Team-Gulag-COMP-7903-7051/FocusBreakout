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
        Debug.Log("LEVEL NUMBAAA: " + num);
        SceneManager.LoadScene("Level" + num);
        //SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
