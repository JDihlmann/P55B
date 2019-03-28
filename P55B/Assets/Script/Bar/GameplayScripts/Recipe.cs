using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
	public string recipeName;
	public int recipeId;
	public int[] recipeStats = new int[4]; // 0-Sweet, 1-Sour, 2-Salty, 3-Bitter. In Percent.
	public string recipeDescription;
	public int recipePrice;
	public int recipeCraftTime;

	public Recipe(int recipeId)
	{
		this.recipeId = recipeId;
		switch (recipeId)
		{
			case 1:
				recipeName = "Water";
				recipeDescription = "Neutral water, very healthy";
				recipePrice = 10;
				recipeCraftTime = 2;
				recipeStats = new int[4];
				for (int i = 0; i < 4; i++)
				{
					recipeStats[i] = 25;
				}
				break;
			case 2:
				recipeName = "Lemonade";
				recipeDescription = "When life gives you lemons...";
				recipePrice = 12;
				recipeCraftTime = 3;
				recipeStats = new int[] {20, 40, 0, 40};
				break;
			case 3:
				recipeName = "Coke";
				recipeDescription = "It is said to be the destroyer of enamel.";
				recipePrice = 13;
				recipeCraftTime = 3;
				recipeStats = new int[] {60, 20, 10, 10};
				break;
			default:
				Debug.Log("Recipe not found.");
				break;
		}
	}
}