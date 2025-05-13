using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;  // Singleton instance
    public float elapsedTime = 0; // Tracks the time elapsed
    public bool timeIsRunning = true;
    public TMP_Text timeText; // Reference to TMP_Text for displaying time

    public GameObject EndScreen;

    void Awake()
    {
        // Ensure there's only one instance of Timer
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy when switching scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void Start()
    {
        if (EndScreen != null)
        {
            EndScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            elapsedTime += Time.deltaTime;
            DisplayTime(elapsedTime);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    // Public method to stop the timer
    public void StopTimer()
    {
        timeIsRunning = false;
        Debug.Log("Timer stopped!");
    }

    // Reset timer (when restarting the game)
    public void ResetTimer()
    {
        elapsedTime = 0;
        timeIsRunning = true;
        if (timeText != null)
        {
            timeText.text = "00:00";  // Reset the timer display text
        }
    }
}
