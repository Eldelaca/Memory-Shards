using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;



    public float enemyPosX;
    public float enemyPosY;
    public float enemyPosZ;
    public float currentStamina;
    

    public GameData()
    {
        this.playerPosX = -10.3f;
        this.playerPosY = 0f;
        this.playerPosZ = -49.8f;
        
        this.enemyPosX = 0f;
        this.enemyPosY = 0f;
        this.enemyPosZ = 0f;
        
        this.currentStamina = 100f;
    }
}