using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {

	#region Variables
	//[Header("Components")]

	//[Space]
	[Header("Variables")]

    IngredientsData data = SaveSystem.LoadIngredients();
    public int[] ingredientCounter = new int[4];
    public int destroyedIngredient;

    #endregion

    #region Methods
	void Awake()
	{
		if (data != null)
			{
				ingredientCounter = data.ingredients;
			}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Moveable")
		{
            Destroy(other.gameObject);
            IngredientMovement movement = other.GetComponent<IngredientMovement>();
			Ingredient ingredient = other.GetComponent<Ingredient>();
			// Disables player input:
			// Stops the momentum of the object (Only for TODO pull animation):
			movement.StopMovement();

            // Increment counter:
            if (ingredient.index == 4)
            {
                destroyedIngredient = Random.Range(0, 4);
                IngredientManager.Ingredients[destroyedIngredient].Count -= 1;
                ingredientCounter[destroyedIngredient] = IngredientManager.Ingredients[destroyedIngredient].Count;
            }
            else
            {
                IngredientManager.Ingredients[ingredient.index].UpdateCount(1);
                // Next line is for Debug purposes:
                ingredientCounter[ingredient.index] = IngredientManager.Ingredients[ingredient.index].Count;
            }
			// Save Counter
			SaveSystem.SaveIngredients(this);

			// Destroy Object TODO? Replace with animation pulling into black hole
			IngredientSpawner.objectCounter -= 1;
		}
	}

	#endregion

}
