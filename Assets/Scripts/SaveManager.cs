using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveManager {
    private static readonly string _path = Application.persistentDataPath + "/player.gucci";

    public static void SaveData(LevelData[] data) {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData[] LoadData() {
        if (File.Exists(_path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);
            LevelData[] data = formatter.Deserialize(stream) as LevelData[];
            stream.Close();
            return data;
        } else {
            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }

    // Return number of main levels completed 
    public static int GetMainLevelsCompleted() {
        LevelData[] _levelDataArray = LoadData();
        int levelsCompleted = 0;
        if (_levelDataArray != null) {
            switch (_levelDataArray.Length) {
                case 1:
                    level = 1;
                    break;
                case 2:
                    level = 2;
                    break;
                case 3:
                    level = 3;
                    break;
                case 4:
                    level = 4;
                    break;
                default:
                    throw new ArgumentException(_levelDataArray.Length +
                        " in SaveManager switch doesn't work");
            }
        }
        return levelsCompleted;
    }
}
