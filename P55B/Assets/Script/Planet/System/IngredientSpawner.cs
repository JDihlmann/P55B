using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {

	#region Variables
	[Header("Components")]
	public GameObject ingredient;
	[Space]
	[Header("Variables")]
	public float spawnTime;
	public float spawnDelay;
	public static int objectCounter;
    private float timer = 0f;
    private Camera cam;
    private Vector3[] spawnPoints; 
    private Vector3 spawnpoint1 = new Vector3();
    private Vector3 spawnpoint2 = new Vector3();
    private Vector3 spawnpoint3 = new Vector3();
    private Vector3 spawnpoint4 = new Vector3();
    private Vector3 spawnpoint5 = new Vector3();
    private Vector3 spawnpoint6 = new Vector3();
    private Vector3 spawnpoint7 = new Vector3();
    private Vector3 spawnpoint8 = new Vector3();

    #endregion

    #region Methods
    // Use this for initialization 
    void Start ()
	{
		objectCounter = GameObject.FindGameObjectsWithTag("Moveable").Length;
        cam = Camera.main;
        spawnpoint1 = cam.ScreenToWorldPoint(new Vector3(0, 1.4f*Screen.height / 5, 15.5f));
        spawnpoint2 = cam.ScreenToWorldPoint(new Vector3(0, 1.8f * Screen.height / 5, 15.5f));
        spawnpoint3 = cam.ScreenToWorldPoint(new Vector3(0, 2.1f*Screen.height / 5, 15.5f));
        spawnpoint4 = cam.ScreenToWorldPoint(new Vector3(0, 3*Screen.height / 5, 15.5f));
        spawnpoint5 = cam.ScreenToWorldPoint(new Vector3(0, 3.4f * Screen.height / 5, 15.5f));
        spawnpoint6 = cam.ScreenToWorldPoint(new Vector3(0, 3.8f * Screen.height / 5, 15.5f));
        spawnpoint7 = cam.ScreenToWorldPoint(new Vector3(0, 4.2f * Screen.height / 5, 15.5f));
        spawnpoint8 = cam.ScreenToWorldPoint(new Vector3(0, 4.6f * Screen.height / 5, 15.5f));
        spawnPoints =  new Vector3[]{spawnpoint1, spawnpoint2 , spawnpoint3 , spawnpoint4, spawnpoint5, spawnpoint6, spawnpoint7, spawnpoint8 };
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            SpawnObject();
            timer = 0f;
        }
    }


    public void SpawnObject()
	{

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(ingredient, spawnPoints[spawnPointIndex], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
    }


	#endregion

}
