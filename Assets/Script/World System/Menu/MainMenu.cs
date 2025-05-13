using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Code use for the Main Menu Only
/// Starts the game loading by the scene name
/// Exits the game
/// and toggles the settings
/// </summary>

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void ExitGame()
    {
        print("Exit Game");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

}
