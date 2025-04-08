using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public float currentStamina;
    public Vector3 enemyPosition;

    public GameData()
    {
        this.playerPosition = Vector3.zero;
        this.enemyPosition = Vector3.zero;
        this.currentStamina = 100f;
    }
}