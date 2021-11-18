using UnityEngine;
using UnityEngine.UI;

public class GameWinScoreManager : MonoBehaviour {
    private Text _textObject;
    private float _highScore;
    private float _currentScore;

    private void Awake() {
        LevelData[] levelData = SaveManager.LoadData();
        float _highScore = levelData[DataManager.CurrentLevel].HighScore;
        _currentScore = DataManager.CurrentScore;

        if (_currentScore > _highScore) {
            levelData[DataManager.CurrentLevel].HighScore = _currentScore;
            SaveManager.SaveData(levelData);
            _highScore = _currentScore;
        }

        _textObject = GetComponent<Text>();
        _textObject.text = "Score: " + _currentScore + "\nHighScore: " + _highScore;
    }
}
