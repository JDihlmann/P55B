﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public GameObject customerPrefab;
	[Space]
	[Header("Variables")]
	public List<GameObject> customerList = new List<GameObject>();
	public List<GameObject> leavingCustomerList = new List<GameObject>();
	public int spawnMaximum;
	public float spawnDelay;
	public List<Transform> spawnPoints = new List<Transform>();
	private bool isSpawning = false;
	[SerializeField]
	private bool lockMaximum = false;
	#endregion

	#region Methods
	// Use this for initialization 
	private void Start()
	{
		foreach (Transform child in GamePlaySystem.Instance.exit.transform)
		{
			spawnPoints.Add(child);
		}
	}

	private void Update()
	{
		if (customerList.Count >= spawnMaximum)
		{
			isSpawning = false;
			CancelInvoke("SpawnObject");
		}
		else
		{
			if (!isSpawning)
			{
				InvokeRepeating("SpawnObject", 1, spawnDelay);
				isSpawning = true;
			}
		}
		if (spawnMaximum < 1)
		{
			spawnMaximum = 1;
		}
	}


	public void SpawnObject()
	{
		int spawnPointIndex = Random.Range(0, spawnPoints.Count);

		GameObject newCustomer = Instantiate(customerPrefab, spawnPoints[spawnPointIndex].position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
		newCustomer.transform.parent = gameObject.transform;
		customerList.Add(newCustomer);
	}

	public void LeaveCustomer(GameObject customer)
	{
		if (customerList.Contains(customer))
		{
			customerList.Remove(customer);
			leavingCustomerList.Add(customer);
		}
	}

	public void DeleteCustomer(GameObject customer)
	{
		if (leavingCustomerList.Contains(customer))
		{
			leavingCustomerList.Remove(customer);
			Destroy(customer);
			AudioManager.Instance.Play("PortalIn");
		}
		CheckHapiness();
	}

	public void CheckHapiness()
	{
		int happiness = GameSystem.Instance.happiness;
		spawnDelay = Random.Range(1, 3f);

		if (!lockMaximum)
		{
			if (happiness < Mathf.Pow(2, spawnMaximum - 1))
			{
				spawnMaximum -= 1;
			}
			else if (happiness >= Mathf.Pow(2, spawnMaximum))
			{
				spawnMaximum += 1;
			}
		}
	}

	#endregion
}
