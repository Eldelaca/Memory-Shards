using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    public TMP_Text timerText;

    void Start()
    {
        if (Timer.instance != null)
        {
            // Get the current elapsed time from the Timer singleton
            float elapsedTime = Timer.instance.elapsedTime;
            DisplayTime(elapsedTime);
        }
        else
        {
            Debug.LogError("Timer instance is not found!");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }
}
