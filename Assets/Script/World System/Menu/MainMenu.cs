using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;

    // Start a new game
    public void StartNewGame()
    {
        // Ensures on Click creates a new game
        // Set initial values for a new game
        SaveLoadManager.instance.NewGame();

        // Load the main game scene
        SceneManager.LoadScene("Memory Shards");
    }

    // Continue from the saved game
    public void ContinueGame()
    {
        // Ensures on Click creates loaded data 
        SaveLoadManager.instance.LoadGame();
        
        SceneManager.LoadScene("Memory Shards");
    }

    // Quits the game
    public void QuitGame()
    {
        // On Click creates Quits
        print("Exit Game");
        Application.Quit(); // ONLY WORKS WHEN GAME IS BUILT

        // Allows to stop the unity editor from playing
        EditorApplication.isPlaying = false;
    }
}
