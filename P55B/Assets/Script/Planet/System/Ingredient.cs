using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

	#region Variables
	//[Header("Components")]
	private Renderer ingredientRenderer;
	//[Space]
	[Header("Variables")]
	public string ingredientName;
	public int index;
	#endregion

	#region Methods
	void Start()
	{
		ingredientRenderer = gameObject.GetComponent<Renderer>();
		GenerateRandomIndex();
		SetIngredient();
	}

	public void GenerateRandomIndex()
	{
		index = Random.Range(0, 5);
	}

	public void SetIngredient()
	{
		switch (index)
		{
			case 0:
				ingredientName = "Strawberry";
				ingredientRenderer.material.color = Color.red;
				break;
			case 1:
				ingredientName = "Blueberry";
				ingredientRenderer.material.color = Color.blue;
				break;
			case 2:
				ingredientName = "Kiwi";
				ingredientRenderer.material.color = Color.green;
				break;
			case 3:
				ingredientName = "Pineapple";
				ingredientRenderer.material.color = Color.yellow;
				break;
            case 4:
                ingredientName = "Bug";
                ingredientRenderer.material.color = Color.grey;
                break;
			default:
				Debug.Log("No such index found.");
				break;
		}
	}
	#endregion

}
