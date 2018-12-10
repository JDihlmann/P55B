using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IngredientsData  {

    public int[] ingredients;

    public IngredientsData (Blackhole blackhole)
    {
        ingredients = blackhole.ingredientCounter;
    }
}
