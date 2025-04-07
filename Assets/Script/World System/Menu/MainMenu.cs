using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;

    // Start a new game
    public void StartNewGame()
    {
        // Set initial values for a new game
        SaveLoadManager.instance.gameData.playerPosition = Vector3.zero;  // Start at the origin or a specific spawn point
        SaveLoadManager.instance.SaveGame();

        // Load the main game scene
        SceneManager.LoadScene("Memory Shards");
    }

    // Continue from the saved game
    public void ContinueGame()
    {
        SaveLoadManager.instance.LoadGame();
        // Ensure the player position is loaded correctly
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null && SaveLoadManager.instance.gameData.playerPosition != null)
        {
            playerMovement.transform.position = SaveLoadManager.instance.gameData.playerPosition;
        }

        SceneManager.LoadScene("Memory Shards");
    }

    // Quits the game
    public void QuitGame()
    {
        print("Exit Game");
        Application.Quit(); // ONLY WORKS WHEN GAME IS BUILT

        // Allows to stop the unity editor from playing
        EditorApplication.isPlaying = false;
    }
}
