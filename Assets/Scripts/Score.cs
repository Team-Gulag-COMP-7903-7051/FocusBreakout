using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float Currentscore;
    public Text CurrentScore;
    // Start is called before the first frame update
    void Start()
    {
        CurrentScore.text = Currentscore.ToString();

        PlayerPrefs.SetFloat("LevelData", Currentscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
