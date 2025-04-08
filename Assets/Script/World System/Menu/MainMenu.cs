using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;  // Reference to the pause menu UI
    public PlayerMovement playerMovement; // Reference to the player movement script
    public SaveLoadManager saveLoadManager; // Reference to the SaveLoadManager script

    private bool isPaused = false; // Track if the game is paused or not

    private void Start()
    {
        pauseMenuUI.SetActive(false); // PauseUi Turned off at start
    }

    private void Update()
    {
        // Toggles teh Inputs
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Show the pause menu and pauses the game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Activate the pause menu UI

        isPaused = true; // Set the pause state to true

        // Make the cursor visible and allow it to be used
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can move freely
        Time.timeScale = 0f; // Pause the game
    }

    // Hide the pause menu and resume the game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI
        Time.timeScale = 1f; // Resume the game
        isPaused = false; // Set the pause state to false

        // Hide the cursor and lock it back to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center
    }

    // Save the game when the Save button is pressed
    public void SaveGame()
    {
        playerMovement.SavePlayerPosition(); // Save the player position
        saveLoadManager.SaveGame(); // Save the game data
        Debug.Log("Game Saved!");
    }

    // Load the game when the Load button is pressed
    public void LoadGame()
    {
        saveLoadManager.LoadGame(); // Load the game data
        Debug.Log("Game Loaded!");

    }

    // Start a new game
    public void StartNewGame()
    {
        // Ensures on Click creates a new game
        saveLoadManager.NewGame(); // Initialize new game data

        // Load the main game scene
        SceneManager.LoadScene("Memory Shards");
    }

    // Continue from the saved game
    public void ContinueGame()
    {
        // Load the saved game data
        saveLoadManager.LoadGame();

        // Load the main game scene
        SceneManager.LoadScene("Memory Shards");
    }

    // Quits the game
    public void QuitGame()
    {
        // On Click creates Quits
        print("Exit Game");
        Application.Quit(); // ONLY WORKS WHEN GAME IS BUILT

        // Allows to stop the Unity editor from playing
        EditorApplication.isPlaying = false;
    }
}
