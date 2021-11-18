using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {
    [SerializeField] private GameObject[] _levelButtons;

    void Awake() {
        LevelData[] data = SaveManager.LoadData();
        int size = _levelButtons.Length;
        int currentLevel;

        if (data == null) {
            currentLevel = 0;
        } else {
            currentLevel = data.Length;
        }

        for (int i = currentLevel + 1; i < size; i++) {
            Image img = _levelButtons[i].GetComponent<Image>();
            img.color = Color.gray;

            Button btn = _levelButtons[i].GetComponent<Button>();
            btn.interactable = false;   
        }
    }
}
