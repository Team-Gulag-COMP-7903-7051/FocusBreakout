using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timeValue;
    public Text texTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else {
            timeValue = 0;
        }
        DisplayCountDownTime(timeValue);
    }

    void DisplayCountDownTime(float DisplayTheTime) {
        if (DisplayTheTime < 0) {
            DisplayTheTime = 0;
        } else if(DisplayTheTime > 0){
            DisplayTheTime += 1;
        }
        float minutes = Mathf.FloorToInt(DisplayTheTime / 60);
        float seconds = Mathf.FloorToInt(DisplayTheTime % 60);
        float millsec = DisplayTheTime % 1 * 1000;

        texTimer.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millsec);
    }
}
