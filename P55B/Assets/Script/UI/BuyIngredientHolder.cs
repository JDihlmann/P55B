using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyIngredientHolder : MonoBehaviour {
    public int price;
    public GameObject cantBuy;
	
	// Update is called once per frame
	void Update () {
        cantBuy.SetActive(!(GameSystem.Instance.money - price >= 0));
        this.gameObject.GetComponent<Button>().interactable = GameSystem.Instance.money - price >= 0;
    }
}
