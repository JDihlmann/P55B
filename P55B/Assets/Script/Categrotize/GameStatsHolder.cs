using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsHolder : MonoBehaviour {

    [SerializeField]
    private Text money;
    [SerializeField]
    private Text happiness;

    // Update is called once per frame
    void Update () {
        money.text = GameSystem.Instance.money.ToString();
        happiness.text = GameSystem.Instance.happiness.ToString();
	}
}
