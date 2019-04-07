using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsHolder : MonoBehaviour {

    [SerializeField]
    private Text money;

	// Update is called once per frame
	void Update () {
        money.text = GameSystem.Instance.money.ToString();
	}
}
