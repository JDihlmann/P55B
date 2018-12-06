using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

	#region Variables
	//[Header("Components")]
	private Renderer ingredientRenderer;
	//[Space]
	[Header("Variables")]
	public string ingredientName;
	public int index;
	#endregion

	#region Methods
	void Start () {
		ingredientRenderer = gameObject.GetComponent<Renderer>();
		GenerateRandomIndex();
		SetIngredient();
	}

	public void GenerateRandomIndex() {
		index = Random.Range(0, 3);
	}

	public void SetIngredient() {
		switch (index) {
			case 0:
				ingredientName = "Tomato";
				ingredientRenderer.material.color = Color.red;
				break;
			case 1:
				ingredientName = "Meatball";
				ingredientRenderer.material.color = Color.blue;
				break;
			case 2:
				ingredientName = "Lettuce";
				ingredientRenderer.material.color = Color.green;
				break;
			case 3:
				ingredientName = "Cheese";
				ingredientRenderer.material.color = Color.yellow;
				break;
			case 4:
				ingredientName = "Broken";
				ingredientRenderer.material.color = Color.black;
				break;
			default:
				Debug.Log("No such index found.");
				break;
		}
	}
	#endregion

}
