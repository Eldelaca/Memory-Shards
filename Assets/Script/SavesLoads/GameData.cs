using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    public string playerName;
    public int currentLevel;
    public string spawnPoint;
    public float stamina;
    public float maxStamina = 100f;
    public Vector3 playerPosition;

}