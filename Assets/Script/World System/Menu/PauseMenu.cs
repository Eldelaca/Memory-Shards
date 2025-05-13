using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  
    public PlayerMovement playerMovement; 

    private bool isPaused = false; 

    private void Start()
    {
        // Set Pause to off on start
        pauseMenuUI.SetActive(false); 
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

    // Show Pause
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        
        isPaused = true; 

        // Show Cursors
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    // Resume Game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false; 

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Save Method
    public void SaveGame()
    {
        playerMovement.SavePlayerPosition(); 
        SaveLoadManager.instance.SaveGame(); 
        Debug.Log("Game Saved!");
    }

    // Load Mehtod
    public void LoadGame()
    {
        SaveLoadManager.instance.LoadGame(); 

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
