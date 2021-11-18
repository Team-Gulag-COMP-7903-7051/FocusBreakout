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

    public int TestScore;

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
            if (_levelDataArray[_level] != null) {
                _currentHighScore = _levelDataArray[_level].HighScore;
            } else {
                _currentHighScore = 0;
            }
        }
    }

    private void Start() {
        _keyCollected = false;

        // **INVESTIGATE ISSUE IN BUILD** BACKUP CODE IN OBJECTSTOCOLLECT.CS

        //_keyObject = GameObject.Find("Glitch(Key)");
        //_keyUI = GameObject.Find("UIKey");

        Application.targetFrameRate = 120;
    }

    public bool SaveLevelData() {
        float score = GetScore();
        Debug.Log("Score: " + score);
        float timeTaken = _timer.TimeTaken();
        Debug.Log("timeTaken: " + timeTaken);
        if (score >= _currentHighScore) {
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
        // ** BELOW CODE DOESN'T WORK ON BUILDS**

        //string path = Application.dataPath + "/Scenes/Levels";

        //if (Directory.Exists(path)) {
        //    return Directory.GetFiles(path, "*.unity").Length;
        //} else {
        //    throw new ArgumentException("Following path could not be found: " + path);
        //}

        return 4;
    }

    public float GetScore() {
        float timeTaken = _timer.TimeTaken();
        int dmgTaken = _player.DamageTaken;

        return timeTaken + dmgTaken * 1.5f;
    }

    private void Update() {
        // **INVESTIGATE ISSUE IN BUILD** BACKUP CODE IN OBJECTSTOCOLLECT.CS

        //if (!_keyObject.activeSelf) {
        //    _keyCollected = true;
        //    _keyUI.GetComponent<Image>().enabled = true;
        //}
    }

    public LevelData[] LevelData {
        get { return _levelDataArray; }
    }

    public bool KeyCollected {
        get { return _keyCollected; }
        set { _keyCollected = value;  }
    }

    public int LevelCount {
        get { return _levelCount; }
    }
}
