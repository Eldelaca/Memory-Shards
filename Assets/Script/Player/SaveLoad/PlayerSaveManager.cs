using UnityEngine;
using UnityEngine.UI;  

public class PlayerSaveManager : MonoBehaviour
{
    
    public PlayerSaveSystem saveSystem;
    public Transform playerTransform;
    public Transform enemyTransform; 

   

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
