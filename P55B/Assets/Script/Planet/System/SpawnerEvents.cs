using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEvents : MonoBehaviour {

    public IngredientSpawner spawner;
    private bool eventInProgress = false;
    private float timer;
    private float eventOccurence;
    private float eventType;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        StartEvent();
	}

    public void StartEvent()
    {
        if (eventInProgress == false)
        {
            eventOccurence = Random.value;
            if (eventOccurence < .0002f)
            {
                eventInProgress = true;
                eventType = Random.value;
                if (eventType < .5f)
                {
                    StartCoroutine(SetLowSpawnrate());
                    
                }
                else
                {
                    StartCoroutine(SetHighSpawnrate());
                }
            }
        }
    }

    IEnumerator SetLowSpawnrate()
    {        
        spawner.spawnDelay = 5f;
        Debug.Log("Low");
        yield return new WaitForSecondsRealtime(30f);
        spawner.spawnDelay = 1.3f;
        eventInProgress = false;
    }

    IEnumerator SetHighSpawnrate()
    {
        spawner.spawnDelay = 0.5f;
        Debug.Log("High");
        yield return new WaitForSecondsRealtime(30f);
        spawner.spawnDelay = 1.3f;
        eventInProgress = false;

    }


}
