using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngredienButtonHandler : MonoBehaviour {

    public Button backButton;

    // Use this for initialization
    void Start()
    {
        backButton.onClick.AddListener(BackToMain);
    }

    void BackToMain()
    {
        SceneManager.LoadScene("Planet");
    }

}
