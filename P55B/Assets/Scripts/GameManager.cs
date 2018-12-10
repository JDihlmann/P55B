using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
        } else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

}
