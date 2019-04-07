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
	public int recipeUnlockCost;
	public int[] recipeIngredientCost = new int[4];
	public int recipePrice;
	public int recipeCraftTime;

	public Recipe(int recipeId)
	{
		this.recipeId = recipeId;
		switch (recipeId)
		{
			case -1:
				recipeName = "None";
				recipeDescription = "Do you really want to serve nothing to your customer?";
				recipeStats = new int[4];
				recipeIngredientCost = new int[4];
				/*
				recipeUnlockCost = 0;
				recipePrice = 0;
				recipeCraftTime = 0;
				recipeStats = new int[4];
				for (int i = 0; i < 4; i++)
				{
					recipeStats[i] = 0;
				}
				*/
				break;
			case 0:
				recipeName = "The Basic";
				recipeDescription = "A little bit of everything to please any specimens likings. Every tavern should list this drink so that no guest dries out.";
				recipeUnlockCost = 0;
				recipePrice = 10;
				recipeCraftTime = 2;
				recipeStats = new int[4];
				recipeIngredientCost = new int[4];
				for (int i = 0; i < 4; i++)
				{
					recipeStats[i] = 25;
					recipeIngredientCost[i] = 1;
				}
				break;
			case 1:
				recipeName = "Gremonade";
				recipeDescription = "When life gives you gremons...";
				recipeUnlockCost = 100;
				recipePrice = 15;
				recipeCraftTime = 3;
				recipeStats = new int[] { 0, 50, 35, 15 };
				recipeIngredientCost = new int[] { 0, 6, 4, 2 };
				break;
			case 2:
				recipeName = "Cöfi";
				recipeDescription = "Bitter but refreshing. Many people insist on only being able to converse in the morning after chugging one or two cups of Cöfi.";
				recipeUnlockCost = 300;
				recipePrice = 25;
				recipeCraftTime = 6;
				recipeStats = new int[] { 15, 35, 0, 50 };
				recipeIngredientCost = new int[] { 2, 4, 0, 6 };
				break;
			case 3:
				recipeName = "New Basic+";
				recipeDescription = "Nearly the same as The Basic but more expensive and strenuous thus it has to be better, right?!";
				recipeUnlockCost = 500;
				recipePrice = 20;
				recipeCraftTime = 4;
				recipeStats = new int[] { 25, 25, 25, 25 };
				recipeIngredientCost = new int[] { 5, 5, 5, 5};
				break;
			default:
				Debug.Log("Recipe not found.");
				break;
		}
	}
}