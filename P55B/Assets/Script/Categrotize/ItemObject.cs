using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemObject
{

    public string ItemName;
    public int ItemID;
    public int Cost;
    public Color Color;
    public int HappinessFactor;
    public string Description;
    public bool Bought;
    //public string ItemName { get; set; }
    //public int ItemID { get; set; }
    //public int Cost { get; set; }
    //public Color Color { get; set; }
    //public int HappinessFactor { get; set; }
    //public string Description { get; set; }
    //public bool Bought { get; set; }


    //public ItemObject(string itemName, int itemID, int cost, Color color, int happinessFactor, string description, bool bought)
    //{
    //    this.ItemName = itemName;
    //    this.ItemID = itemID;
    //    this.Cost = cost;
    //    this.Color = color;
    //    this.HappinessFactor = happinessFactor;
    //    this.Description = description;
    //    this.Bought = bought;
    //}

    public void Buy()
    {
        this.Bought = true;
    }

}
