using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int money;

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
        } else{
            instance = this;
            DontDestroyOnLoad(gameObject);
            money = 50;
        }

    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SubtractMoney(int amount)
    {
        int newAmount = money - amount;
        if (newAmount >= 0)
        {
            money = newAmount;
            return true;
        }
        return false;
    }

}
