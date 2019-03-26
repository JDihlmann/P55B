using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStatsUI : MonoBehaviour {

    private List<MachineStatsObject> machineStats;

    public GameObject machineStatPrefab;

    public Transform grid;

    void Start()
    {
        machineStats = IngredientManager.GetMachineStats();
        FillList();
    }

    void FillList()
    {
        for (int i = 0; i < machineStats.Count; i++)
        {
            GameObject holder = Instantiate(machineStatPrefab, grid);
            MachineStatsHolder holderScript = holder.GetComponent<MachineStatsHolder>();
            holderScript.machineStat = machineStats[i];
        }
    }
}
