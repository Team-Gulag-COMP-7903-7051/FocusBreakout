using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public float TimeValue;
    public Text TexTimer;
    
    void Update() {

        if (TimeValue > 0){
            TimeValue -= Time.deltaTime;
        } else {
            TimeValue = 0;
            SceneManager.LoadScene("GameOverSceneRanOutTime");
        }

        DisplayCountDownTime(TimeValue);

    }

    void DisplayCountDownTime(float DisplayTheTime) {

        if (DisplayTheTime < 0) {
            DisplayTheTime = 0;
        }

        float minutes = Mathf.FloorToInt(DisplayTheTime / 60);
        float seconds = Mathf.FloorToInt(DisplayTheTime % 60);
        float millsec = DisplayTheTime % 1 * 1000;

        TexTimer.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millsec);
    }
}
