using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour {
    [SerializeField] private bool _keyCollected;
    [SerializeField] private int _level; // used as index in array
    [SerializeField] private string _levelName;
    [SerializeField] private Player _player;
    [SerializeField] private CountDownTimer _timer;

    public int testScore;

    private GameObject _keyObject;
    private GameObject _keyUI;
    private LevelData[] _levelDataArray;
    private int _levelCount;
    private float _currentHighScore;

    private void Awake() {
        _levelCount = GetLevelCount();
        _levelDataArray = SaveManager.LoadData();

        if (_levelDataArray == null) {
            _levelDataArray = new LevelData[_levelCount];
            _currentHighScore = 0;
        } else {
            _currentHighScore = _levelDataArray[_level].HighScore;
        }
    }

    private void Start() {
        _keyCollected = false;
        _keyObject = GameObject.Find("Glitch(Key)");
        _keyUI = GameObject.Find("UIKey");
        Application.targetFrameRate = 120;
    }

    public bool SaveLevelData() {
        float score = GetScore();
        float timeTaken = _timer.TimeTaken();
        if (score > _currentHighScore) {
            LevelData levelData = new LevelData(_levelName, _level, score, timeTaken, _player.DamageTaken);
            _levelDataArray[_level] = levelData;
            SaveManager.SaveData(_levelDataArray);
            return true;
        } else {
            return false;
        }
    }

    // Returns number of levels available if possible
    private int GetLevelCount() {
        string path = Application.dataPath + "/Scenes/Levels";

        if (Directory.Exists(path)) {
            return Directory.GetFiles(path, "*.unity").Length;
        } else {
            throw new ArgumentException("Following path could not be found: " + path);
        }
    }

    public float GetScore() {
        float timeTaken = _timer.TimeTaken();
        int dmgTaken = _player.DamageTaken;

        return timeTaken + dmgTaken * 1.5f;
    }

    void Update() {
        if (!_keyObject.activeSelf) {
            _keyCollected = true;
        }

        if (_keyCollected) {
            _keyUI.GetComponent<Image>().enabled = true;
        }
    }

    public LevelData[] LevelData {
        get { return _levelDataArray; }
    }

    public bool KeyCollected {
        get { return _keyCollected; }
    }

    public int LevelCount {
        get { return _levelCount; }
    }
}
