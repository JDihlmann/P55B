using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {

	public static void SaveIngredients (Blackhole blackhole)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/ingredients.bla";
        string path = "ingredients.bla";
        FileStream stream = new FileStream(path, FileMode.Create);

        IngredientsData data = new IngredientsData(blackhole);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static IngredientsData LoadIngredients()
    {
        //string path = Application.persistentDataPath + "/ingredients.bla";
        string path = "ingredients.bla";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            IngredientsData data = formatter.Deserialize(stream) as IngredientsData;
            stream.Close();

            return data;
            
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
