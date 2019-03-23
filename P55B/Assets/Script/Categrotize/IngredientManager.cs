using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{

    public static IngredientManager Instance;

    public static List<IngredientObject> Ingredients;

    public static List<ItemObject> Items;

    public static List<RecipeObject> Recipes;

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
            data = SaveSystem.LoadIngredients();
            BuildDatabase(data);
        }
    }

    private void BuildDatabase(IngredientsData data)
    {


        //string dataAsJson = File.ReadAllText("Assets/Script/Categrotize/Test.json");
        Items = BuildLists<ItemObject>("JSON/Items");

        Recipes = BuildLists<RecipeObject>("JSON/Recipes");

        //Ingredients = BuildLists<IngredientObject>("JSON/Ingredients");


        int[] savedData = new int[] { 0, 0, 0, 0 };

        if (data != null)
        {
            savedData = data.ingredients;

        }

        Ingredients = new List<IngredientObject>(){
            new IngredientObject("Strawberry", 1, 5, savedData[0], Color.red, "Qubi"),
            new IngredientObject("Blueberry", 1, 5, savedData[1], Color.blue, "Qubi"),
            new IngredientObject("Apple", 1, 5, savedData[2], Color.green, "Qubi"),
            new IngredientObject("Banana", 1, 5, savedData[3], Color.yellow, "Qubi"),
        };
    }

    private List<Objects> BuildLists<Objects>(string path)
    {
        string dataAsJson = Resources.Load<TextAsset>(path).ToString();

        List<Objects> list = new List<Objects>(JsonHelper.FromJson<Objects>(dataAsJson));

        return list;
    }

    public static List<RecipeObject> GetRecipes()
    {
        return Recipes;
    }

    public static List<IngredientObject> GetIngredients(){
        return Ingredients;
    }

    public static List<ItemObject> GetItems()
    {
        return Items;
    }

}
