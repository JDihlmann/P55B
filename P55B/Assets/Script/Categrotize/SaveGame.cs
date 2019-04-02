using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour {

    /*
	public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/ingredients.bla";
        string path = "SaveGame.bla";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveGameData data = new SaveGameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveGameData Load()
    {
        //string path = Application.persistentDataPath + "/ingredients.bla";
        string path = "SaveGame.bla";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveGameData data = formatter.Deserialize(stream) as SaveGameData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
	*/
}
