using UnityEngine;

public class Player : Blob
{
    void Start() {}

    void Update() {}

    protected override void Die() {
        GameObject sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<SceneNavigation>().LoadScene("GameOverScene");
    }
}
