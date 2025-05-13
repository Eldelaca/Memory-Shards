using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // This function reloads the current scene
    public void Restart()
    {
        // Reset the timer
        if (Timer.instance != null)
        {
            Timer.instance.ResetTimer(); // Reset the timer in the Timer singleton
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}