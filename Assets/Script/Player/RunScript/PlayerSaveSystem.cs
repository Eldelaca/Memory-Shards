using UnityEngine;

public class PlayerSaveSystem : MonoBehaviour
{
    // Player Position Variables
    private float playerPosX;
    private float playerPosY;
    private float playerPosZ;

    // Enemy Position Variables
    private float enemyPosX;
    private float enemyPosY;
    private float enemyPosZ;

    // Save the current player and enemy positions
    public void SavePosition(Transform playerTransform, Transform enemyTransform)
    {
        // Save player position
        playerPosX = playerTransform.position.x;
        playerPosY = playerTransform.position.y;
        playerPosZ = playerTransform.position.z;

        // Save enemy position
        enemyPosX = enemyTransform.position.x;
        enemyPosY = enemyTransform.position.y;
        enemyPosZ = enemyTransform.position.z;
    }

    // Load the saved player and enemy positions
    public void LoadPosition(Transform playerTransform, Transform enemyTransform)
    {
        // Load player position
        Vector3 loadedPlayerPosition = new Vector3(playerPosX, playerPosY, playerPosZ);
        playerTransform.position = loadedPlayerPosition;

        // Load enemy position
        Vector3 loadedEnemyPosition = new Vector3(enemyPosX, enemyPosY, enemyPosZ);
        enemyTransform.position = loadedEnemyPosition;
    }
}
