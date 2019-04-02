﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySystem : MonoBehaviour
{
	public static GamePlaySystem Instance { get; private set; }

	#region Variables
	//[Header("Components")]
	private CustomerSpawner spawner;
	//[Space]
	[Header("Variables")]
	public List<Worker> totalWorkerList = new List<Worker>();
	public List<Recipe> totalRecipeList = new List<Recipe>();
	public List<Customer> customerList = new List<Customer>();
	public List<Customer> orderingCustomerList = new List<Customer>();
	public List<GameObject> availableSeatList = new List<GameObject>();
	public GameObject bar;
	public GameObject exit;

	private float tempSeater = 1f;
	#endregion

	#region Methods
	public void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		spawner = gameObject.GetComponent<CustomerSpawner>();
	}

	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.KeypadPlus))
		{
			GameSystem.Instance.AddTime(0.25f);
		}
		if (Input.GetKeyUp(KeyCode.KeypadMinus))
		{
			GameSystem.Instance.SubTime(0.25f);
		}
		if (tempSeater > 0.75f)
		{
			tempSeater -= Time.deltaTime;
			UpdateSeatList();
		}
	}

	public void AddCustomerToList(Customer customer)
	{
		customerList.Add(customer);
	}

	public void LeavingCustomer(GameObject customer)
	{
		spawner.LeaveCustomer(customer);
	}

	public void DestroyCustomer(GameObject customer)
	{
		spawner.DeleteCustomer(customer);
	}

	public void AddOrderToList(Customer customer)
	{
		orderingCustomerList.Add(customer);
	}

	public void AddWorkerToList(Worker worker)
	{
		totalWorkerList.Add(worker);
		UpdateRecipes();
	}

	public void AddSeatToList(GameObject seat)
	{
		if (!availableSeatList.Contains(seat))
		{
			availableSeatList.Add(seat);
		}
	}

	public GameObject GetAvailableSeat()
	{
		GameObject seat;
		if (availableSeatList.Count > 0)
		{
			int random = Random.Range(0, availableSeatList.Count);
			seat = availableSeatList[random];
			availableSeatList.RemoveAt(random);
			return seat;
		}
		return null;
	}

	public void UpdateSeatList()
	{
		availableSeatList.Clear();
		GameObject[] seatArray = GameObject.FindGameObjectsWithTag("Seatable");
		foreach (GameObject seat in seatArray)
		{
			availableSeatList.Add(seat);
		}
	}

	public void UpdateRecipes() // Creates a List with ALL equipped recipes.
	{
		totalRecipeList.Clear();
		if (totalWorkerList.Count > 0)
		{
			for (int i = 0; i < totalWorkerList.Count; i++)
			{
				for (int k = 0; k < totalWorkerList[i].recipeList.Count; k++)
				{
					if (IsUniqueRecipe(totalWorkerList[i].recipeList[k].recipeId))
					{
						totalRecipeList.Add(totalWorkerList[i].recipeList[k]);
					}		
				}
			}
		}
		else
		{
			Debug.Log("No workers assigned");
		}
	}

	private bool IsUniqueRecipe(int newRecipeId)
	{
		for (int i = 0; i < totalRecipeList.Count; i++)
		{
			if (totalRecipeList[i].recipeId == newRecipeId)
			{
				return false;
			}
		}

		return true;
	}
	#endregion
}
