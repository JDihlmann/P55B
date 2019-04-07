using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InregdientsUI : MonoBehaviour {

    public static InregdientsUI ingredientsUI;

    private List<IngredientObject> ingredientList;

    public GameObject ingredientHolderPrefab;

    public Transform grid;

    public GameObject modalWindow;

	// Use this for initialization
	void Start () {
        ingredientsUI = this;
        ingredientList = IngredientManager.GetIngredients();
        FillList();
        Debug.Log("start");
	}

    void FillList() {
        for (int i = 0; i < ingredientList.Count; i++){
            GameObject holder = Instantiate(ingredientHolderPrefab, grid);
            IngredientHolder holderScript = holder.GetComponent<IngredientHolder>();
            holderScript.name.text = ingredientList[i].Name;
            holderScript.amount.text = (GameSystem.Instance.ingredientAmount[i]).ToString();
            holderScript.price.text = ingredientList[i].Price + "$ each";
            holderScript.image.sprite = Resources.Load<Sprite>("Sprites/" + ingredientList[i].Image);
            int tempInt = ingredientList[i].Price;
            string tempString = ingredientList[i].Name;
            int tempId = ingredientList[i].ID;
            holderScript.button.onClick.AddListener(() => openModal(tempInt, tempString, holderScript, tempId));
            //holderScript.ingredientName.text = ingredientList[i].IngredientName;
            //holderScript.ingredientCount.text = ingredientList[i].Count.ToString();
            //holderScript.ingredientImage.color = ingredientList[i].Color;
            //holderScript.ingredientID = ingredientList[i].IngredientID;
        }
    }

    private void Update()
    {
        for (int i = 0; i < ingredientList.Count; i++)
        {
            grid.transform.GetChild(i).GetComponent<IngredientHolder>().amount.text = (GameSystem.Instance.ingredientAmount[i]).ToString();
        }
    }

    void openModal(int itemPrice, string ingredient, IngredientHolder holderScript, int id)
    {
        modalWindow.transform.SetAsLastSibling();
        GameObject holder = Instantiate(modalWindow, this.gameObject.transform.parent);
        holder.GetComponent<IngredientModalWindowHolder>().itemPrice = itemPrice;
        holder.GetComponent<IngredientModalWindowHolder>().ingredient = ingredient;
        holder.GetComponent<IngredientModalWindowHolder>().holderScript = holderScript;
        holder.GetComponent<IngredientModalWindowHolder>().id = id;
    }
}
