using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {

	#region Variables
	[Header("Components")]
	public GameObject ingredient;
	[Space]
	[Header("Variables")]
	public bool stopSpawning = false;
	public int spawnMaximum = 5;
	public float spawnTime;
	public float spawnDelay;
	public static int objectCounter;
    public Vector3[] spawnPoints = { new Vector3(-25, -10, -4.5f), new Vector3(-25, -5, -4.5f), new Vector3(-25, 5, -4.5f), new Vector3(-25, 10, -4.5f) };
	#endregion

	#region Methods
	// Use this for initialization 
	void Start ()
	{
		objectCounter = GameObject.FindGameObjectsWithTag("Moveable").Length;
		InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
	}


	public void SpawnObject()
	{
		if (objectCounter < spawnMaximum)
		{
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(ingredient, spawnPoints[spawnPointIndex], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));

        }
		if (stopSpawning)
		{
			CancelInvoke("SpawnObject");
		}
	}


	#endregion

}
