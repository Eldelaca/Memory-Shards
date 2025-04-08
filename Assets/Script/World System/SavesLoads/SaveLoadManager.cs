using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEditor.Overlays;

public class SaveLoadManager : MonoBehaviour
{
    [Header("File Storages Configurations")]
    [SerializeField] private string fileName = "savefile.json"; // Where we will save the data to
    private string saveFilePath ;

    private GameData gameData; 
    public static SaveLoadManager instance { get; private set; }
    private List<IDataPesistence> dataPersistenceObjects; // List to grab all the scripts from other projects
    private FileDataHandler dataHandler;

    
    

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

       DontDestroyOnLoad(gameObject); // This should keep the state even across the scenes
       saveFilePath = Path.Combine(Application.persistentDataPath, fileName);

        Debug.Log($"Save file path is: {saveFilePath}");
    }

    private void Start()
    {

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // On Start up Game is loaded up
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    // Save game data to a JSON file
    public void SaveGame()
    {
        
        // Here should handle the data by converting and passing the data to other scripts that need it so they can update it
        foreach (IDataPesistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        
        // Update gameData from current scene state:
        // Here save data to a file using the data handler

        // Convert gameData to JSON and write to file
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game Saved! at" + saveFilePath);
        

        /*
        SaveData data = new SaveData();
        string json = JsonUtility.ToJson(data, true);

        try
        {
            // Make sure directory exists
            string folderPath = Application.persistentDataPath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            File.WriteAllText(saveFilePath, json);

            Debug.Log("Game saved to: " + saveFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
        */


    }

    // Load game data from the JSON file and apply it to the scene
    public void LoadGame()
    {
        /*        // Loads any saved data from a file using the data handler
                this.gameData = dataHandler.Load();


                // This must load any save data from any file using the data handler
                // If no data is loaded, initialize to a new game
                if (this.gameData == null)
                {
                    Debug.Log("No data was found. Creating data to default values");
                    NewGame();
                }
        */
        // To Do - Create push of data to read a script





        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game Loaded! from: " + saveFilePath);

            foreach (IDataPesistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
        else
        {
            Debug.Log("No data was found. Creating data to default values");
            NewGame();
        }
        
    }

    
    
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("No save file to delete.");
        }
    }
    

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPesistence> FindAllDataPersistenceObjects()
    {

        // This code finds all the scripts that has the scripts IDataPersistence in the scene
        IEnumerable<IDataPesistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPesistence>();

        return new List<IDataPesistence>(dataPersistenceObjects);
    }

}
