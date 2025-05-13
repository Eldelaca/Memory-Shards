using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPos;
    public Vector3 enemyPos;
    public float currentStamina;
    

    public GameData()
    {
        playerPos = Vector3.zero;
        enemyPos = Vector3.zero;

        currentStamina = 100f;

    }
}