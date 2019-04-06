using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientModalWindowHolder : MonoBehaviour {

    public Text title;

    public string ingredient;

    private int[] buyAmountIngredients = new int[] {5, 10, 15, 20, 30, 50};

    public Transform grid;

    public GameObject ingredientBuy;

    public int itemPrice;

    public Button background;

    public int id;

    public IngredientHolder holderScript;

    // Use this for initialization
    void Start () {
        Button btn = background.GetComponent<Button>();
        btn.onClick.AddListener(() => Destroy(this.gameObject));

        title.text = "Buy more " + ingredient;
        FillList();
	}

    void FillList()
    {
        for (int i = 0; i < buyAmountIngredients.Length; i++)
        {
            GameObject holder = Instantiate(ingredientBuy, grid);
            holder.transform.GetChild(1).GetComponent<Text>().text = (buyAmountIngredients[i]).ToString();
            holder.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = (buyAmountIngredients[i] * itemPrice).ToString();
            int amount = buyAmountIngredients[i];
            holder.GetComponent<Button>().onClick.AddListener(() => buyIngredients(amount));
        }
    }

    void buyIngredients(int amount)
    {
        holderScript.amount.text = (GameSystem.Instance.ingredientAmount[id] + amount).ToString();
        GameSystem.Instance.AddIngredient(id, amount);
    }
}
