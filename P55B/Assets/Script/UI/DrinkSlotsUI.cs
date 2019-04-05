using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSlotsUI : MonoBehaviour {

    public GameObject drinkSlot;

    public Transform grid;

    public GameObject modalWindow;

    private int[] drinks = new int[] { 1, 0, 0, 1 };

    private int drinkSlotAmount;

    void Start()
    {
        //AddDrinkSlot(0);
        drinkSlotAmount = GameSystem.Instance.workerUnlocks[0];
        FillList();
        //drinkSlotAmountLast = GameSystem.Instance.workerUnlocks[0];
    }

    private void Update()
    {
        if(drinkSlotAmount != GameSystem.Instance.workerUnlocks[0]){
            AddDrinkSlot(GameSystem.Instance.workerUnlocks[0]);
            drinkSlotAmount = GameSystem.Instance.workerUnlocks[0];
        }
    }

    void AddDrinkSlot(int i)
    {
        GameObject holder = Instantiate(drinkSlot, grid);
        if (i < drinks.Length)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[drinks[i]].Image);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            holder.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            holder.transform.GetChild(2).GetComponent<Text>().text = IngredientManager.GetRecipes()[drinks[i]].Name;
            holder.GetComponent<Button>().onClick.AddListener(openModal);
        }
    }

    void FillList()
    {
        for (int i = 0; i <= drinkSlotAmount; i++)
        {
            GameObject holder = Instantiate(drinkSlot, grid);
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[drinks[i]].Image);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            holder.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            holder.transform.GetChild(2).GetComponent<Text>().text = IngredientManager.GetRecipes()[drinks[i]].Name;
            holder.GetComponent<Button>().onClick.AddListener(openModal);
            //GetComponent<Image>().sprite = sprite;
            //MachineStatsHolder holderScript = holder.GetComponent<MachineStatsHolder>();
            //holderScript.machineStat = machineStats[i];
        }
    }

    void openModal()
    {
        Instantiate(modalWindow, this.gameObject.transform.parent);
    }

}
