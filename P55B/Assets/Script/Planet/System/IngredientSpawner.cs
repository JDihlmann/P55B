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
    //public int spawnMaximum;
	public float spawnTime;
	public float spawnDelay;
	public static int objectCounter;
    public Vector3[] spawnPoints; 
    private Camera cam;
    private Vector3 spawnpoint1 = new Vector3();
    private Vector3 spawnpoint2 = new Vector3();
    private Vector3 spawnpoint3 = new Vector3();
    private Vector3 spawnpoint4 = new Vector3();

    #endregion

    #region Methods
    // Use this for initialization 
    void Start ()
	{
		objectCounter = GameObject.FindGameObjectsWithTag("Moveable").Length;
		InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        cam = Camera.main;
        spawnpoint1 = cam.ScreenToWorldPoint(new Vector3(0, 1.2f*Screen.height / 5, 15.5f));
        spawnpoint2 = cam.ScreenToWorldPoint(new Vector3(0, 2*Screen.height / 5, 15.5f));
        spawnpoint3 = cam.ScreenToWorldPoint(new Vector3(0, 3*Screen.height / 5, 15.5f));
        spawnpoint4 = cam.ScreenToWorldPoint(new Vector3(0, 4*Screen.height / 5, 15.5f));
        spawnPoints =  new Vector3[]{spawnpoint1, spawnpoint2 , spawnpoint3 , spawnpoint4 };
    }


	public void SpawnObject()
	{
		if (stopSpawning)
		{
			CancelInvoke("SpawnObject");
		}
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(ingredient, spawnPoints[spawnPointIndex], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
    }


	#endregion

}
