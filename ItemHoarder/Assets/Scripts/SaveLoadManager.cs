using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager 
{
    public static void SavePlayerData(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSaveData data = new PlayerSaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game saved");
    }

    public static PlayerSaveData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/SaveData.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
            stream.Close();

            return data;
        } 
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
