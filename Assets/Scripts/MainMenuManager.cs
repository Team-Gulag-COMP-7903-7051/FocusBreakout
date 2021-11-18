using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    private void Awake() {
        int num = SaveManager.GetMainLevelsCompleted();
    }
}
