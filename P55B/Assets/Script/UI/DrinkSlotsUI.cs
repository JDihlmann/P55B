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
        holder.GetComponent<Button>().onClick.AddListener(() => openModal(i));
        if (GameSystem.Instance.recipeList[i].recipeId != -1)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[GameSystem.Instance.recipeList[i].recipeId].Image);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            holder.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            holder.transform.GetChild(2).GetComponent<Text>().text = IngredientManager.GetRecipes()[GameSystem.Instance.recipeList[i].recipeId].Name;
        }
    }

    void FillList()
    {
        for (int i = 0; i <= drinkSlotAmount; i++)
        {
            if (GameSystem.Instance.recipeList[i].recipeId == -1)
            {
                GameObject holder = Instantiate(drinkSlot, grid);
                int tempId = i;
                holder.GetComponent<Button>().onClick.AddListener(() => openModal(tempId));
            }
            else
            {
                GameObject holder = Instantiate(drinkSlot, grid);
                Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[GameSystem.Instance.recipeList[i].recipeId].Image);
                holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                holder.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                holder.transform.GetChild(2).GetComponent<Text>().text = IngredientManager.GetRecipes()[GameSystem.Instance.recipeList[i].recipeId].Name;
                int tempId = i;
                holder.GetComponent<Button>().onClick.AddListener(() => openModal(tempId));
            }
            //GetComponent<Image>().sprite = sprite;
            //MachineStatsHolder holderScript = holder.GetComponent<MachineStatsHolder>();
            //holderScript.machineStat = machineStats[i];
        }
    }

    public void SetDrink(int slotId, int drinkId)
    {
        GameSystem.Instance.SetRecipe(slotId, drinkId);
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + IngredientManager.GetRecipes()[drinkId].Image);
        grid.GetChild(slotId).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = sprite;
        grid.GetChild(slotId).transform.GetChild(0).gameObject.GetComponent<Image>().preserveAspect = true;
        grid.GetChild(slotId).transform.GetChild(2).gameObject.GetComponent<Text>().text = IngredientManager.GetRecipes()[drinkId].Name;

    }

    void openModal(int slotId)
    {
        GameObject modal = Instantiate(modalWindow, this.gameObject.transform.parent);
        modal.GetComponent<DrinkSlotModalWindowHolder>().slotId = slotId;
    }

}
