using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public void SavePosition()
    {
        SaveLoadManager.instance.SaveGame();
        Debug.Log("Saved Data");
    }

    public void LoadPosition()
    {
        SaveLoadManager.instance.LoadGame();
        Debug.Log("Loaded Data");
    }
}
