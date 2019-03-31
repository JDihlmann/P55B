using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSlotsUI : MonoBehaviour {

    public GameObject drinkSlot;

    public Transform grid;

    private int[] drinks = new int[] { 1, 0, 0, 1 };

    void Start()
    {
        FillList();
    }

    void FillList()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject holder = Instantiate(drinkSlot, grid);
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[drinks[i]].Image);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            holder.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            //GetComponent<Image>().sprite = sprite;
            //MachineStatsHolder holderScript = holder.GetComponent<MachineStatsHolder>();
            //holderScript.machineStat = machineStats[i];
        }
    }

}
