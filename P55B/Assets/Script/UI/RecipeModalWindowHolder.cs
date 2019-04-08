using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeModalWindowHolder : MonoBehaviour {

    public Text title;
    public Button background;
    public Image image;
    public Text description;
    public List<int> amount;
    public List<int> ingredients;
    public Text price;
    public Button buyButton;
    public Text rarety;
    public Text income;
    public GameObject cantBuy;
    public int cost;

    public GameObject recipeIngredientHolderPrefab;
    public Transform ingredientGrid;
    public int id;

    void Start()
    {
        Button btn = background.GetComponent<Button>();
        btn.onClick.AddListener(hideModal);

        for(int i = 0; i < amount.Count; i++)
        {
            GameObject holder = Instantiate(recipeIngredientHolderPrefab, ingredientGrid);
            RecipeIngredientHolder holderScript = holder.GetComponent<RecipeIngredientHolder>();
            holderScript.text.text = "x " + amount[i].ToString();
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetIngredients()[ingredients[i]].Image);
            holderScript.image.sprite = sprite;
            holderScript.image.preserveAspect = true;
        }
    }

    private void Update()
    {
        cantBuy.SetActive(!(GameSystem.Instance.money - cost >= 0) || GameSystem.Instance.recipeUnlocks[id]);
        buyButton.GetComponent<Button>().interactable = GameSystem.Instance.money - cost >= 0;
    }

    public void hideModal()
    {
        Destroy(this.gameObject);
    }
}
