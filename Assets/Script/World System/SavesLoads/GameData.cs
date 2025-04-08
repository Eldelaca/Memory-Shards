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
        this.playerPosX = 0f;
        this.playerPosY = 0f;
        this.playerPosZ = 0f;
        this.currentStamina = 1000f;
    }
}