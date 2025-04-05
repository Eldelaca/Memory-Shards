using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public GameData gameData;
    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        LoadGame();
    }

    // Save Game Data to json
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved!");
    }

    // Load Game Data to json
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(json, gameData);
            Debug.Log("Game Loaded!");
        }
    }
}
