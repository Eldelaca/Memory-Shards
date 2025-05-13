using UnityEngine;

public class PlayerDataManager : MonoBehaviour, IDataPesistence
{
    public float stamina;

    private void Start()
    {
        transform.position = Vector3.zero;
        stamina = 100f;
    }

    public void SaveData(ref GameData data)
    {
        data.currentStamina = stamina;
        data.playerPos = transform.position;
    }

    public void LoadData(GameData data)
    {
        stamina = data.currentStamina;
        transform.position = data.playerPos;
    }
}
