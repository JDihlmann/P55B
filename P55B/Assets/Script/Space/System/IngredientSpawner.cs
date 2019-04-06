using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {

	#region Variables
	[Header("Components")]
	public GameObject ingredient1Left;
    public GameObject ingredient1Right;
    public GameObject ingredient2Left;
    public GameObject ingredient2Right;
    public GameObject ingredient3Left;
    public GameObject ingredient3Right;
    public GameObject ingredient4Left;
    public GameObject ingredient4Right;
    public GameObject ingredient5Left;
    public GameObject ingredient5Right;

    public Camera cam;
    [Space]
	[Header("Variables")]
	public float spawnTime;
	public float spawnDelay;
	public static int objectCounter;
    private float timer = 0f;
    private Vector3[] spawnPointsLeft;
    private Vector3[] spawnPointsRight;
    private Vector3 spawnpoint1 = new Vector3();
    private Vector3 spawnpoint2 = new Vector3();
    private Vector3 spawnpoint3 = new Vector3();
    private Vector3 spawnpoint4 = new Vector3();
    private Vector3 spawnpoint5 = new Vector3();
    private Vector3 spawnpoint6 = new Vector3();
    private Vector3 spawnpoint7 = new Vector3();

    #endregion

    #region Methods
    // Use this for initialization 
    void Start ()
	{
		objectCounter = GameObject.FindGameObjectsWithTag("Moveable").Length;
        //cam = Camera.Camera_Space; ;
        spawnpoint1 = cam.ScreenToWorldPoint(new Vector3(1.3f*Screen.width, 2.2f * Screen.height / 5, 57.5f));
        spawnpoint2 = cam.ScreenToWorldPoint(new Vector3(0, 2.6f*Screen.height / 5, 65f));
        spawnpoint3 = cam.ScreenToWorldPoint(new Vector3(1.3f*Screen.width, 3.0f * Screen.height / 5, 52.5f));
        spawnpoint4 = cam.ScreenToWorldPoint(new Vector3(0, 3.4f * Screen.height / 5, 60f));
        spawnpoint5 = cam.ScreenToWorldPoint(new Vector3(1.3f*Screen.width, 3.8f * Screen.height / 5, 47.5f));
        spawnpoint6 = cam.ScreenToWorldPoint(new Vector3(0, 4.2f * Screen.height / 5, 55f));
        spawnpoint7 = cam.ScreenToWorldPoint(new Vector3(1.3f*Screen.width, 4.6f * Screen.height / 5, 42.5f));
        spawnpoint2.x = spawnpoint2.x * 1.5f;
        spawnpoint4.x = spawnpoint2.x * 1.5f;
        spawnpoint6.x = spawnpoint2.x * 1.5f;
        spawnPointsRight =  new Vector3[]{spawnpoint1, spawnpoint3, spawnpoint5, spawnpoint7 };
        spawnPointsLeft = new Vector3[] {spawnpoint2, spawnpoint4, spawnpoint6,};
        spawnDelay = 1.3f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            chooseIngredient();
            timer = 0f;
        }
    }

    private void chooseIngredient()
    {
        int index = Random.Range(0, 5);
        switch (index)
        {
            case 0:
                SpawnObject(ingredient1Left, ingredient1Right);
                break;
            case 1:
                SpawnObject(ingredient2Left, ingredient2Right);
                break;
            case 2:
                SpawnObject(ingredient3Left, ingredient3Right);
                break;
            case 3:
                SpawnObject(ingredient4Left, ingredient4Right);
                break;
            case 4:
                SpawnObject(ingredient5Left, ingredient5Right);
                break;
            default:
                Debug.Log("No such index found.");
                break;
        }
    }


    public void SpawnObject(GameObject ingredientLeft, GameObject ingredientRight)
	{

        int spawnPointLeftIndex = Random.Range(0, spawnPointsLeft.Length);
        int spawnPointRightIndex = Random.Range(0, spawnPointsRight.Length);
        float side = Random.value;
        if (side < .5f)
        {
            Instantiate(ingredientLeft, spawnPointsLeft[spawnPointLeftIndex], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f), transform);
        }
        else
        {
            Instantiate(ingredientRight, spawnPointsRight[spawnPointRightIndex], new Quaternion(0.0f, 0.0f, 0.0f, 1.0f), transform);
        }
        
    }


	#endregion

}
