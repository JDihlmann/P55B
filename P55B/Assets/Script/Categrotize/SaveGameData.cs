using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SaveGameData {

    public bool[] recipeUnlocks = new bool[5];
	public List<Recipe> recipeList = new List<Recipe>(); // Equipped recipes of worker
	public int[] workerUnlocks = new int[4];
    public int[] ingredientAmount = new int[4];
	public List<ObjectProperties> objectList = new List<ObjectProperties>(); // Placed objects
	public int money = 0;
    public int happiness = 0;

    public SaveGameData()
    {
        
        ingredientAmount = GameSystem.Instance.ingredientAmount;
        money = GameSystem.Instance.money;
        happiness = GameSystem.Instance.happiness;
        recipeUnlocks = GameSystem.Instance.recipeUnlocks;
        workerUnlocks = GameSystem.Instance.workerUnlocks;
		objectList = GameSystem.Instance.objectList;
		recipeList = GameSystem.Instance.recipeList;

	}
}
