using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSlotsUI : MonoBehaviour {

    public GameObject drinkSlot;

    public Transform grid;

    void Start()
    {
        FillList();
    }

    void FillList()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject holder = Instantiate(drinkSlot, grid);
            //MachineStatsHolder holderScript = holder.GetComponent<MachineStatsHolder>();
            //holderScript.machineStat = machineStats[i];
        }
    }

}
