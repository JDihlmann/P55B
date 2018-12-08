using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{

    public static IngredientManager Instance;

    private static List<IngredientObject> Ingredients;

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
        BuildDatabase();
    }

    private void BuildDatabase(){
        Ingredients = new List<IngredientObject>(){
            new IngredientObject("Strawberry", 1, 5, 0, Color.red),
        };
    }

    public static List<IngredientObject> GetIngredients(){
        return Ingredients;
    }

}
