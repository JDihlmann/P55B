using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {

	#region Variables
	//[Header("Components")]

	//[Space]
	[Header("Variables")]
	public int[] ingredientCounter = new int[4];
	#endregion

	#region Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Moveable")
		{
			IngredientMovement movement = other.GetComponent<IngredientMovement>();
			Ingredient ingredient = other.GetComponent<Ingredient>();
			// Disables player input:
			// Stops the momentum of the object:
			movement.StopMovement();

			// Increment counter:
			ingredientCounter[ingredient.index] += 1;
			
			// Adds force to the center of the blackhole (TODO)
			/*
			Vector3 direction = planet.transform.position - transform.position;
			direction = direction.normalized;
			rbody.AddForce(1000 * direction);
			*/

			// Destroy Object
			Destroy(other.gameObject);
		}
	}

	#endregion

}
