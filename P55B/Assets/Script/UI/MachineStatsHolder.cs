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

    private int currentStat;

    private Sprite statSprite;

    public GameObject cantBuy;

    private void Start()
    {
        currentStat = GameSystem.Instance.workerUnlocks[machineStat.ID];
        id = machineStat.ID;
        name.text = machineStat.Name;
        price.text = machineStat.Steps[currentStat].Price.ToString();
        statSprite = Resources.Load<Sprite>("Sprites/MachineStatIce");
        Sprite nostat = Resources.Load<Sprite>("Sprites/NoStat");
        for (int i = 0; i < currentStat; i++)
        {
            GameObject stat = Instantiate(statPrefab, statGrid);
            stat.GetComponent<Image>().sprite = statSprite;
        }
        for (int i = currentStat; i < machineStat.Steps.Count; i++)
        {
            GameObject stat = Instantiate(statPrefab, statGrid);
            stat.GetComponent<Image>().sprite = nostat;
        }
        this.GetComponent<Button>().onClick.AddListener(updateStat);
    }

    private void Update()
    {
        if(currentStat + 1 <= machineStat.Steps.Count)
        {
            cantBuy.SetActive(!(GameSystem.Instance.money - machineStat.Steps[currentStat].Price >= 0));
            this.gameObject.GetComponent<Button>().interactable = GameSystem.Instance.money - machineStat.Steps[currentStat].Price >= 0;
        }
    }

    private void updateStat()
    {

        statGrid.GetChild(currentStat).GetComponent<Image>().sprite = statSprite;
        GameSystem.Instance.SubMoney(machineStat.Steps[currentStat].Price);

        currentStat += 1;

        if (currentStat == machineStat.Steps.Count)
        {
            price.text = "";
            this.GetComponent<Button>().onClick.RemoveListener(updateStat);
            this.gameObject.GetComponent<Button>().interactable = false;
        } else 
        {
            price.text = machineStat.Steps[currentStat].Price.ToString();
        }

        GameSystem.Instance.UpgradeWorker(machineStat.ID);
    }

}
