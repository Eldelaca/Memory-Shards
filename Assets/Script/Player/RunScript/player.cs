using UnityEngine;
using UnityEngine.UI;  

public class UIManager : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public PlayerSaveSystem saveSystem;
    public Transform playerTransform;
    public Transform enemyTransform; 

    void Start()
    {
        // Assign button listeners
        saveButton.onClick.AddListener(SavePosition);
        loadButton.onClick.AddListener(LoadPosition);
    }

    public void SavePosition()
    {
        saveSystem.SavePosition(playerTransform, enemyTransform);
        Debug.Log("Player and Enemy positions saved!");
    }

    public void LoadPosition()
    {
        saveSystem.LoadPosition(playerTransform, enemyTransform);
        Debug.Log("Player and Enemy positions loaded!");
    }
}
