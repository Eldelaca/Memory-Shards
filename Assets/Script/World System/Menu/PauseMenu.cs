using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI
    public PlayerMovement playerMovement; // Reference to the player movement script

    private bool isPaused = false; // Track if the game is paused or not

    private void Start()
    {
        pauseMenuUI.SetActive(false); // Initially, the pause menu is not active
    }

    private void Update()
    {
        // Check if the Escape key is pressed to toggle the pause menu
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

    // Show the pause menu and pause the game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Activate the pause menu UI
        
        isPaused = true; // Set the pause state to true

        // Make the cursor visible and allow it to be used
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so it can move freely
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
        SaveLoadManager.instance.SaveGame(); // Save the game data
        Debug.Log("Game Saved!");
    }

    // Load the game when the Load button is pressed
    public void LoadGame()
    {
        SaveLoadManager.instance.LoadGame(); // Load the game data

        /* Check if loaded data is valid
        if (SaveLoadManager.instance.gameData != null)
        {
            // Make sure the player position is updated to the loaded position
            playerMovement.transform.position = SaveLoadManager.instance.gameData.playerPosition;
            Debug.Log("Game Loaded! Player position updated.");
        }
        else
        {
            Debug.LogWarning("Game data is null. Cannot load game.");
        }
        */
    }

}
