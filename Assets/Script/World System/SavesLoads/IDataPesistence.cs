using UnityEngine;
/// <summary>
/// 
/// This will be used to pass data to other scripts
/// Ensures code can get passed on from other scripts
/// No need to attach the script 
/// 
/// </summary>
public interface IDataPesistence
{
    

    // This reads the line of data
    void LoadData(GameData data);
    
    // This in simple terms, can be updated/ modify the data
    void SaveData(ref GameData data);
}
