using TMPro;
using UnityEngine;

public class GameWinScoreManager : MonoBehaviour {

    private void Awake() {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        LevelData[] levelData = SaveManager.LoadData();
        float _highScore = levelData[DataManager.CurrentLevel].HighScore;
        float _currentScore = DataManager.CurrentScore;

        if (_currentScore > _highScore) {
            levelData[DataManager.CurrentLevel].HighScore = _currentScore;
            SaveManager.SaveData(levelData);
            _highScore = _currentScore;
        }

        text.text = "Score: " + _currentScore + "\nHighScore: " + _highScore;
    }
}
