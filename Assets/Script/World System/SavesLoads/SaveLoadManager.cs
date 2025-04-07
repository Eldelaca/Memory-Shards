using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public float stamina;
}

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    public GameData gameData;

    private string saveFilePath;

    // Reference to PlayerMovement and StaminaBar
    public PlayerMovement playerMovement;  // Assign this in the Inspector
    public StaminaBar staminaBar;          // Assign this in the Inspector

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        saveFilePath = Application.persistentDataPath + "/savefile.json"; // Set path for the save file
        gameData = new GameData(); // Initialize game data

        // Optionally assign references if not set in the Inspector
        if (playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>(); // Get the PlayerMovement component in the scene

        if (staminaBar == null)
            staminaBar = FindObjectOfType<StaminaBar>(); // Get the StaminaBar component in the scene
    }

    // Save the game data to a file
    public void SaveGame()
    {
        GameData data = new GameData();
        data.playerPosition = playerMovement.transform.position; // Save the player's position
        data.stamina = staminaBar.GetCurrentStamina(); // Assuming stamina is being tracked

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json); // Save the data to a file

        Debug.Log("Game Saved! Position: " + data.playerPosition);
    }

    // Load the game data from a file
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            // Update the player's position and stamina from the loaded data
            playerMovement.transform.position = data.playerPosition; // Load the player's position
            staminaBar.SetStamina(data.stamina); // Load stamina if needed

            Debug.Log("Game Loaded! Position: " + data.playerPosition);
        }
        else
        {
            Debug.LogWarning("Save file does not exist.");
        }
    }
}
