using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSlotModalWindowHolder : MonoBehaviour {

    public Transform grid;
    public Button background;
    public GameObject recipeButton;

    private bool[] boughtRecipes = new bool[] {true, false, false, true };

	// Use this for initialization
	void Start () {
    
        Button btn = background.GetComponent<Button>();
        btn.onClick.AddListener(hideModal);

        for(int i = 0; i < boughtRecipes.Length; i++)
        {
            if (boughtRecipes[i])
            {
                GameObject button = Instantiate(recipeButton, grid);
                button.transform.GetChild(0).GetComponent<Text>().text = IngredientManager.GetRecipes()[i].Name;
            }
        }
    }

    public void hideModal()
    {
        Destroy(this.gameObject);
    }

}
