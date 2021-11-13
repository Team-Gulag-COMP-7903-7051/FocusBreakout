using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour {
    public void LoadScene(string scene) {
        StopAllCoroutines();
        SceneManager.LoadScene(scene);
        SfxManager.sfxInstance.AudioSfx.PlayOneShot(SfxManager.sfxInstance.AudioClick);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
