using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int unlockedLevel;
}

public static class LevelDataHandler
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "levelData.json");

    public static void SaveLevelData(LevelData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log(filePath);
    }

    public static LevelData LoadLevelData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<LevelData>(json);
        }
        return new LevelData { unlockedLevel = 1 };
    }
}
