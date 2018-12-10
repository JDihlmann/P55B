using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleButtonClick : MonoBehaviour {

    public Button ingredientButton;

	// Use this for initialization
	void Start () {
        ingredientButton.onClick.AddListener(OpenIngredientScreen);
	}

    void OpenIngredientScreen() {
        SceneManager.LoadScene("Ingredients");
    }

}
