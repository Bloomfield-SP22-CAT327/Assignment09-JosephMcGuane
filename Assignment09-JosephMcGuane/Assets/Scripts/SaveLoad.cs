using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static List<GameController> savedGames = new List<GameController>();

    public static void Save()
    {
        SaveLoad.savedGames.Add(GameController.current);
        BinaryFormatter bf = new BinaryFormatter();
       
        FileStream file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "savedGames.gd");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "savedGames.gd"))
        {
            Debug.Log("Loading:" + Application.persistentDataPath + Path.DirectorySeparatorChar + "savedGames.gd");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<GameController>)bf.Deserialize(file);
            file.Close();
        }
    }
}
