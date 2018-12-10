using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject {

    //public string IngredientName;
    //public int IngredientID;
    //public int Cost;
    //public int Count;
    //public Color Color;

    public string IngredientName { get; set; }
    public int IngredientID { get; set; }
    public int Cost { get; set; }
    public int Count { get; set; }
    public Color Color { get; set; }


    public IngredientObject(string ingredientName, int ingredientID, int cost, int count, Color color) {
        this.IngredientName = ingredientName;
        this.IngredientID = ingredientID;
        this.Cost = cost;
        this.Count = count;
        this.Color = color;
    }

    public void UpdateCount (int number){
        this.Count += number;
    }
        
}
