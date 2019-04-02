using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SaveGameData {

    public bool[] recipeUnlocks = new bool[5];
    public int[] workerUnlocks = new int[4];
    public int[] ingredientAmount = new int[4];
    public int[] objectAmount = new int[5];
    public int money = 0;
    public int happiness = 0;

    public SaveGameData()
    {
        
        ingredientAmount = GameSystem.Instance.ingredientAmount;
        money = GameSystem.Instance.money;
        happiness = GameSystem.Instance.happiness;
        objectAmount = GameSystem.Instance.objectAmount;
        recipeUnlocks = GameSystem.Instance.recipeUnlocks;
        workerUnlocks = GameSystem.Instance.workerUnlocks;
        
    }
}
