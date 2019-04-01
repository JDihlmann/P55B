using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InregdientsUI : MonoBehaviour {

    public static InregdientsUI ingredientsUI;

    private List<IngredientObject> ingredientList;

    public GameObject ingredientHolderPrefab;

    public Transform grid;

	// Use this for initialization
	void Start () {
        ingredientsUI = this;
        ingredientList = IngredientManager.GetIngredients();
        FillList();
	}

    void FillList() {
        for (int i = 0; i < ingredientList.Count; i++){
            GameObject holder = Instantiate(ingredientHolderPrefab, grid);
            IngredientHolder holderScript = holder.GetComponent<IngredientHolder>();
            holderScript.name.text = ingredientList[i].Name;
            holderScript.amount.text = "50";
            holderScript.price.text = ingredientList[i].Price + "$ each";
            holderScript.image.sprite = Resources.Load<Sprite>("Sprites/" + ingredientList[i].Image);
            //holderScript.ingredientName.text = ingredientList[i].IngredientName;
            //holderScript.ingredientCount.text = ingredientList[i].Count.ToString();
            //holderScript.ingredientImage.color = ingredientList[i].Color;
            //holderScript.ingredientID = ingredientList[i].IngredientID;
        }
    }
}
