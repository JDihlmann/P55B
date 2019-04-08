using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsMainScene : MonoBehaviour {

    public GameObject ingredientHolderPrefab;

    public Transform grid;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < IngredientManager.GetIngredients().Count; i++)
        {
            GameObject holder = Instantiate(ingredientHolderPrefab, grid);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetIngredients()[i].Image);
            holder.transform.GetChild(1).GetComponent<Text>().text = "x " + GameSystem.Instance.ingredientAmount[i].ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < IngredientManager.GetIngredients().Count; i++)
        {
            this.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = "x " + GameSystem.Instance.ingredientAmount[i].ToString();
        }
    }
}
