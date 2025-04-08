using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{

    public string enemyName;
    public float speed;
    public int health;
    public float vision;
    public float fleeTime;
    public float fleeTimer;

}

