using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemsUI : MonoBehaviour
{

    public static ItemsUI itemUI;

    private List<ItemObject> itemsList;

    public GameObject itemHolderPrefab;

    public Transform grid;

    public GameObject objectGrid;
    public GameObject changer;

    // Use this for initialization
    void Start()
    {
        itemUI = this;
        itemsList = IngredientManager.GetItems();
        FillList();
    }

    void FillList()
    {
        for (int i = 1; i < itemsList.Count; i++)
        {
            Debug.Log(itemsList[i]);
            GameObject holder = Instantiate(itemHolderPrefab, grid);
            ItemHolder holderScript = holder.GetComponent<ItemHolder>();
            holderScript.item = itemsList[i];
            holderScript.objectGrid = objectGrid.GetComponent<ObjectGrid_Instantiate>();
            holderScript.changer = changer.GetComponent<StateChange>();
            //holderScript.itemName.text = itemsList[i].ItemName;
            //holderScript.itemID = itemsList[i].ItemID;
            //holderScript.happinessFactor.text = itemsList[i].HappinessFactor.ToString();
        }
    }

}
