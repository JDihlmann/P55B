using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour {

	#region Variables
	[Header("Components")]
	public Transform planet;
	public GameObject spawnee;
	public IngredientMovement movement;
	[Space]
	[Header("Variables")]
	public bool stopSpawning = false;
	public int spawnMaximum = 5;
	public float spawnTime;
	public float spawnDelay;
	public static int objectCounter;
	#endregion

	#region Methods
	// Use this for initialization
	void Start ()
	{
		objectCounter = GameObject.FindGameObjectsWithTag("Moveable").Length;

		movement = gameObject.GetComponent<IngredientMovement>();
		InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
	}

	private void Update()
	{
		movement.orbitSpeed = Random.Range(-1000, 1000);
	}

	public void SpawnObject()
	{
		if (objectCounter < spawnMaximum)
		{
			CreateObject(spawnee);

		}
		if (stopSpawning)
		{
			CancelInvoke("SpawnObject");
		}
	}

	public GameObject CreateObject(GameObject spawnee)
	{
		objectCounter += 1;
		GameObject createdObject = Instantiate(spawnee, transform.position, transform.rotation) as GameObject;
		IngredientMovement mover = createdObject.GetComponent<IngredientMovement>();
		mover.planet = planet;
		mover.orbitSpeed = Random.Range(40, 80);

		return createdObject;
	}

	#endregion

}
