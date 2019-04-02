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

    public static List<MachineStatsObject> MachineStats;

    //private static IngredientsData data;

    // delete line
    public static List<bool> RecipeBoughtState;

    private void Awake()
    {
        BuildDatabase();
      
        RecipeBoughtState = new List<bool>() { true, false, false };

    }

    private void BuildDatabase()
    {


        //string dataAsJson = File.ReadAllText("Assets/Script/Categrotize/Test.json");
        Items = BuildLists<ItemObject>("JSON/Items");

        Recipes = BuildLists<RecipeObject>("JSON/Recipes");

        MachineStats = BuildLists<MachineStatsObject>("JSON/MachineStats");

        Ingredients = BuildLists<IngredientObject>("JSON/Ingredients");

        //Ingredients = BuildLists<IngredientObject>("JSON/Ingredients");


        //int[] savedData = new int[] { 0, 0, 0, 0 };

        //if (data != null)
        //{
        //    savedData = data.ingredients;

        //}

        //Ingredients = new List<IngredientObject>(){
        //    new IngredientObject("Strawberry", 1, 5, savedData[0], Color.red, "Qubi"),
        //    new IngredientObject("Blueberry", 1, 5, savedData[1], Color.blue, "Qubi"),
        //    new IngredientObject("Apple", 1, 5, savedData[2], Color.green, "Qubi"),
        //    new IngredientObject("Banana", 1, 5, savedData[3], Color.yellow, "Qubi"),
        //};
    }

    private List<Objects> BuildLists<Objects>(string path)
    {
        string dataAsJson = Resources.Load<TextAsset>(path).ToString();

        List<Objects> list = new List<Objects>(JsonHelper.FromJson<Objects>(dataAsJson));

        return list;
    }

    public static List<MachineStatsObject> GetMachineStats()
    {
        return MachineStats;
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

    // delete line
    public static List<bool> GetRecipeBoughtState()
    {
        return RecipeBoughtState;
    }

}
