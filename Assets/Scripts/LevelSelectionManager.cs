using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {
    [SerializeField] private GameObject[] _levelButtons;

    void Awake() {
        LevelData[] levelDataArray = SaveManager.LoadData();
        int size = _levelButtons.Length;
        int levelsCompleted = SaveManager.GetMainLevelsCompleted(); 

        // Lock levels levelsCompleted + 1 and after 
        // so player can only select the next level.
        for (int i = levelsCompleted + 1; i < size; i++) {
            Image img = _levelButtons[i].GetComponent<Image>();
            img.color = Color.gray;

            Button btn = _levelButtons[i].GetComponent<Button>();
            btn.interactable = false;   
        }
    }
}
