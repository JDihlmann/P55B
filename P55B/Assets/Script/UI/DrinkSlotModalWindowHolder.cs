using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSlotModalWindowHolder : MonoBehaviour {

    public Transform grid;
    public Button background;
    public GameObject recipeButton;
    public int slotId;

    //private bool[] boughtRecipes = new bool[] {true, false, false, true };
    private bool[] boughtRecipes;

    // Use this for initialization
    void Start () {

        boughtRecipes = GameSystem.Instance.recipeUnlocks;

        Button btn = background.GetComponent<Button>();
        btn.onClick.AddListener(hideModal);
        
        for(int i = 0; i < boughtRecipes.Length; i++)
        {
            if (boughtRecipes[i])
            {
                GameObject button = Instantiate(recipeButton, grid);
                button.transform.GetChild(0).GetComponent<Text>().text = IngredientManager.GetRecipes()[i].Name;
                int tempInt = i;
                button.GetComponent<Button>().onClick.AddListener(() => chooseDrink(tempInt));
            }
        }
    }

    void chooseDrink(int recipeId)
    {
        this.gameObject.transform.parent.GetChild(this.gameObject.transform.GetSiblingIndex()-2).GetComponent<DrinkSlotsUI>().SetDrink(slotId, recipeId);
        Destroy(this.gameObject);
    }

    public void hideModal()
    {
        Destroy(this.gameObject);
    }

}
