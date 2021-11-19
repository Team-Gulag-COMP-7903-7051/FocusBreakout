using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {
    [SerializeField] private float _startingTime;

    private Text _textTimer;
    private float _timeLeft;

    private void Start() {
        _timeLeft = _startingTime;
        _textTimer = GetComponent<Text>();
    }

    void Update() {
        if (_timeLeft > 0){
            _timeLeft -= Time.deltaTime;
        } else {
            DataManager.GameOverMessage = "Ran Out of Time :c";
            SceneManager.LoadScene("GameOverScene");
        }
        DisplayCountDownTime(_timeLeft);
    }

    void DisplayCountDownTime(float time) {
        if (time < 0) {
            time = 0;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        _textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
