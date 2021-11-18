using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
class GameData
{
    public int score;

    public List<int> highScores;
}

public class ScoreController : MonoBehaviour
{

    public List<int> highScores;

    const string fileName = "/highscore.dat";

    public static ScoreController gCtrl;

    public void Awake()
    {
        if (gCtrl == null)
        {
            DontDestroyOnLoad(gameObject);
            gCtrl = this;
            LoadScore();
        }
    }

    public void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + fileName,
                FileMode.Open, FileAccess.Read);
            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();
            gCtrl.highScores = data.highScores;
        }
    }

    public void SaveScore(int score)
    {

        if (gCtrl.highScores.Count == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (score > gCtrl.highScores[i])
                {
                    gCtrl.highScores[i] = score;
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs = File.Open(Application.persistentDataPath + fileName,
                        FileMode.OpenOrCreate);
                    GameData data = new GameData();
                    data.score = score;
                    data.highScores = highScores;
                    bf.Serialize(fs, data);
                    fs.Close();
                    break;
                }
            }
        }
        if (gCtrl.highScores.Count < 4)
        {
            gCtrl.highScores.Add(score);
        }
    }

    public int GetCurrentScore()
    {
        return PlayerPrefs.GetInt("CurrentScore");
    }

    public void SetCurrentScore(int num)
    {
        PlayerPrefs.SetInt("CurrentScore", num);
    }
}
