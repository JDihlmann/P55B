using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeObject { 
    public int ID;
    public string Name;
    public int Price;
    public int MinIncome;
    public int MaxIncome;
    public string Description;
    public string Image;
    public string Rarety;
    public List<int> Amount;
    public List<int> Ingredients;
}
