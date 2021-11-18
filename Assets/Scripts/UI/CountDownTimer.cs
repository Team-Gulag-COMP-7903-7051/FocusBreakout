using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {
    [SerializeField] private float _startingTime;
    [SerializeField] private Text _texTimer;

    private float _timeLeft;

    private void Start() {
        _timeLeft = _startingTime;
    }

    void Update() {
        if (_timeLeft > 0){
            _timeLeft -= Time.deltaTime;
        } else {
            _timeLeft = 0;
            SceneManager.LoadScene("GameOverSceneRanOutTime");
        }
        DisplayCountDownTime(_timeLeft);
    }

    void DisplayCountDownTime(float DisplayTheTime) {
        if (DisplayTheTime < 0) {
            DisplayTheTime = 0;
        }

        float minutes = Mathf.FloorToInt(DisplayTheTime / 60);
        float seconds = Mathf.FloorToInt(DisplayTheTime % 60);
        float millsec = DisplayTheTime % 1 * 1000;

        _texTimer.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millsec);
    }

    public float TimeTaken() {
        return _startingTime - _timeLeft;
    }

    public float StartingTime {
        get { return _startingTime; }
    }

    public float TimeLeft {
        get { return _timeLeft; }
    }

}
