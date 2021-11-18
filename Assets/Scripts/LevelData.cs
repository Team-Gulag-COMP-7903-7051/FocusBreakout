using System;
using UnityEngine;

// EVERY LEVEL/SCENE SHOULD HAVE A UNIQUE LEVEL INTEGER
[Serializable]
public class LevelData {
    private string _name;
    private int _level; // Used for array index
    private float _highScore;
    private float _time;
    private int _damageTaken;

    public LevelData (string name, int level, float highScore, float time, int damageTaken) {
        if (highScore <= 0) {
            throw new ArgumentOutOfRangeException("HighScore cannot be negative.");
        } else if (time <= 0) {
            throw new ArgumentOutOfRangeException("Time cannot be negative.");
        } else if (damageTaken < 0) {
            throw new ArgumentOutOfRangeException("Damage Taken must be at least 0.");
        }

        _name = name;
        _level = level;
        _highScore = highScore;
        _time = time;
        _damageTaken = damageTaken;
    }

    // Used for testing
    public void Print() {
        Debug.Log("Name: " + _name + "\n" +
            "Level: " + _level + "\n" +
            "HighScore: " + _highScore + "\n" +
            "Time: " + _time + "\n" +
            "Damage Taken: " + _damageTaken);
    }

    public string Name {
        get { return _name; }
    }

    public int Level {
        get { return _level; }
    }

    public float HighScore {
        get { return _highScore; }
        set { _highScore = value; }
    }

    public float Time {
        get { return _time; }
    }

    public int DamageTaken {
        get { return _damageTaken; }
    }
}
