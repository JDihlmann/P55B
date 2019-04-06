using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {

	#region Variables
	//[Header("Components")]

	//[Space]
	[Header("Variables")]

    public int[] ingredientCounter = new int[4];
    public int destroyedIngredient;

    #endregion

    #region Methods
	void Awake()
	{

            ingredientCounter = GameSystem.Instance.ingredientAmount;
	
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
                GameSystem.Instance.AddIngredient(destroyedIngredient, -1);
            }
            else
            {
                GameSystem.Instance.AddIngredient(ingredient.index, 1);
            }
			// Save Counter
			GameSystem.SaveGameSystem();

			// Destroy Object TODO? Replace with animation pulling into black hole
			IngredientSpawner.objectCounter -= 1;
		}
	}

	#endregion

}
