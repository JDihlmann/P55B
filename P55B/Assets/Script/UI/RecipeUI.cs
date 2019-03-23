using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUI : MonoBehaviour {

    private List<RecipeObject> recipes;

    public GameObject recipeHolderPrefab;

    public Transform grid;

    void Start()
    {
        recipes = IngredientManager.GetRecipes();
        FillList();
    }

    void FillList()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            GameObject holder = Instantiate(recipeHolderPrefab, grid);
            RecipeHolder holderScript = holder.GetComponent<RecipeHolder>();
            holderScript.recipe = recipes[i];
        }
    }
}
