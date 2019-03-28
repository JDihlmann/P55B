using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public GamePlaySystem gameSystem;
	private Customer servicedCustomer;
	[Space]
	[Header("Variables")]
	public string workerName;
	public int workerId;
	public bool isIdle = true;
	public float craftSpeed;
	public float orderSpeed;
	public List<Recipe> recipeList = new List<Recipe>();
	public int recipeLimit = 1;
	private float timer;
	// Add more information like crafting time, cost, etc.

	#endregion

	#region Methods
	void Start()
	{
		InitializeWorker(workerId);
		gameSystem.AddWorkerToList(this);
		// Load worker stats here
	}

	private void Update()
	{
		if (isIdle)
		{
			if (gameSystem.orderingCustomerList.Count != 0)
			{
				for (int i = 0; i < gameSystem.orderingCustomerList.Count; i++)
				{
					if (recipeList.Contains(gameSystem.orderingCustomerList[i].selectedRecipe))
					{
						servicedCustomer = gameSystem.orderingCustomerList[i];
						gameSystem.orderingCustomerList.RemoveAt(i);
						timer = servicedCustomer.selectedRecipe.recipeCraftTime / craftSpeed;
						isIdle = false;
						break;
					}
				}

			}
		}
		if (isIdle)
		{
			if (gameSystem.customerList.Count != 0)
			{
				servicedCustomer = gameSystem.customerList[0];
				gameSystem.customerList.RemoveAt(0);
				isIdle = false;
				timer = 3 / orderSpeed;
			}
		}
		if (!isIdle)
		{
			if (timer > 0)
			{
				timer -= Time.deltaTime;
				servicedCustomer.isWaiting = false;
			}
			else
			{
				switch (servicedCustomer.currentState)
				{
					case Customer.CustomerState.ORDER:
						GetOrder(servicedCustomer);
						break;
					case Customer.CustomerState.WAITING:
						ServeOrder(servicedCustomer);
						break;
					default:
						break;
				}
				if (servicedCustomer.currentState != Customer.CustomerState.LEAVE)
				{
					servicedCustomer.NextState();
				}
				isIdle = true;
				servicedCustomer = null;
			}
		}
	}

	#region SystemMethods
	public void InitializeWorker(int newId)
	{
		workerId = newId;
		craftSpeed = 1;
		orderSpeed = 1;

		switch (workerId)
		{
			case 1:
				workerName = "HEL-Pi";
				recipeList = new List<Recipe> { new Recipe(1), new Recipe(2), new Recipe(3) };
				craftSpeed = 1;
				orderSpeed = 1;
				break;
			case 2:
				workerName = "HEL-Po";
				recipeList = new List<Recipe> { new Recipe(3) };
				break;
			default:
				Debug.Log("No valid Worker ID");
				break;
		}
	}

	public void AddRecipe(Recipe newRecipe)
	{
		if (recipeList.Count < recipeLimit)
		{
			if (!recipeList.Contains(newRecipe))
			{
				recipeList.Add(newRecipe);
			}
		}
	}

	public void RemoveRecipe(Recipe oldRecipe)
	{
		if (recipeList.Contains(oldRecipe))
		{
			recipeList.Remove(oldRecipe);
		}
	}
	#endregion

	#region GameplayMethods
	public void GetOrder(Customer customer)
	{
		customer.SelectDrink();
		gameSystem.AddOrderToList(customer);
	}

	public void ServeOrder(Customer customer)
	{
		int sum = customer.selectedRecipe.recipePrice;
		if (customer.drinkValue <= 90)
		{
			sum *= Mathf.RoundToInt(1 + Mathf.Floor((100 - customer.drinkValue) / 10) / 10);
		}

		Debug.Log("Customer pays " + sum);
		GameSystem.Instance.money += sum;
		// Calculate money here
		// Calculate happiness here
		// Debug.Log("Customer pays " + customer.selectedRecipe.recipePrice);
	}
	#endregion

	#endregion
}
