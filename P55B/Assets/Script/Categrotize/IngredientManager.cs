using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{

    public static IngredientManager Instance;

    public static List<IngredientObject> Ingredients;

    public static List<ItemObject> Items;

    private static IngredientsData data;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        data = SaveSystem.LoadIngredients();
        BuildDatabase(data);
    }

    private void BuildDatabase(IngredientsData data)
    {

        // get Items from the Test JSON File
        string dataAsJson = File.ReadAllText("Assets/Script/Categrotize/Test.json");
        Items = new List<ItemObject>(JsonHelper.FromJson<ItemObject>(dataAsJson));

        int[] savedData = new int[] { 0, 0, 0, 0 };

        if (data != null) {
            savedData = data.ingredients;

        }
        Ingredients = new List<IngredientObject>(){
            new IngredientObject("Strawberry", 1, 5, savedData[0], Color.red),
            new IngredientObject("Blueberry", 1, 5, savedData[1], Color.blue),
            new IngredientObject("Apple", 1, 5, savedData[2], Color.green),
            new IngredientObject("Banana", 1, 5, savedData[3], Color.yellow),
        };
    }

    public static List<IngredientObject> GetIngredients(){
        return Ingredients;
    }

    public static List<ItemObject> GetItems()
    {
        return Items;
    }

}
