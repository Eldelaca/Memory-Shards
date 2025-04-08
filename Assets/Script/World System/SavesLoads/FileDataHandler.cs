using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


/// <summary>
/// 
/// This is used to Format the file directory 
/// Stores the data
/// This here will read and write the file to a directory
/// 
/// </summary>
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirpath, string dataFileName)
    {
        this.dataDirPath = dataDirpath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // Useful for pathing on different OS due to different pathing
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // This here will serialize the data from the file
                string dataToLoad = File.ReadAllText(fullPath);
                // Here it deserialize the data back from being a json file
                // Back into the C# object
                // This loads back from the Save Data into the Load data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
             

                
                
            }
            catch (Exception e) 
            {
                Debug.Log("Error occured when trying to Load data to file: " + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }

    public void Save(GameData data)
    {

        // Useful for pathing on different OS due to different pathing
        string fullPath = Path.Combine (dataDirPath, dataFileName);
        try
        {
            // Creates a directory the file can be written on if it does not exist
            Directory.CreateDirectory (Path.GetDirectoryName(fullPath));
            
            // This is the IMPORTANT!!!!
            // Using C# 
            // Serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson (data, true);

            // Write the serialized data to the file
            // When dealing with Writing/ Reading best to use using ()
            // Ensures the connections to the file is closed when Reading/Writing
            File.WriteAllText(fullPath, dataToStore);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " +  fullPath + "\n" + e);
        }
    }
}
