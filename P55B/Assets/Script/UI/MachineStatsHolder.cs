using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineStatsHolder : MonoBehaviour {
    [SerializeField]
    private int id;
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text price;
    
    public GameObject statPrefab;

    public Transform statGrid;

    public MachineStatsObject machineStat;

    private int currentStat = 1;

    private Sprite statSprite;

    private void Start()
    {
        id = machineStat.ID;
        name.text = machineStat.Name;
        price.text = machineStat.Steps[currentStat+1].Price.ToString();
        statSprite = Resources.Load<Sprite>("Sprites/MachineStatIce");
        Sprite nostat = Resources.Load<Sprite>("Sprites/NoStat");
        for (int i = 0; i < currentStat + 1; i++)
        {
            GameObject stat = Instantiate(statPrefab, statGrid);
            stat.GetComponent<Image>().sprite = statSprite;
        }
        for (int i = currentStat + 1; i < machineStat.Steps.Count; i++)
        {
            GameObject stat = Instantiate(statPrefab, statGrid);
            stat.GetComponent<Image>().sprite = nostat;
        }
        this.GetComponent<Button>().onClick.AddListener(updateStat);
    }

    private void updateStat()
    {
        currentStat += 1;

        if (currentStat == machineStat.Steps.Count - 1)
        {
            price.text = "";
            this.GetComponent<Button>().onClick.RemoveListener(updateStat);
        } else 
        {
            price.text = machineStat.Steps[currentStat + 1].Price.ToString();
        }

        statGrid.GetChild(currentStat).GetComponent<Image>().sprite = statSprite;
    }

}
