using UnityEngine;

public class EnemyDataManager : MonoBehaviour, IDataPesistence
{

    private void Start()
    {
        transform.position = Vector3.zero;
    }
    public void SaveData(ref GameData data)
    {
        data.enemyPos = transform.position;
    }

    public void LoadData(GameData data)
    {
        transform.position = data.enemyPos;
    }
}
