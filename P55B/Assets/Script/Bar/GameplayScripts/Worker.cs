using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	private Customer servicedCustomer;
	[Space]
	[Header("Variables")]
	public bool isIdle = true;
	private float timer;
	#endregion

	#region Methods
	private void Update()
	{
		if (isIdle)
		{
			if (GamePlaySystem.Instance.orderingCustomerList.Count != 0)
			{
				for (int i = 0; i < GamePlaySystem.Instance.orderingCustomerList.Count; i++)
				{
					// if (GameSystem.Instance.recipeList.Contains(GamePlaySystem.Instance.orderingCustomerList[i].selectedRecipe))
					if (GamePlaySystem.Instance.orderingCustomerList[i].currentState != Customer.CustomerState.LEAVE)
					{
						if (GamePlaySystem.Instance.IngredientCost(GamePlaySystem.Instance.orderingCustomerList[i].selectedRecipe))
						{
							servicedCustomer = GamePlaySystem.Instance.orderingCustomerList[i];
							GamePlaySystem.Instance.orderingCustomerList.RemoveAt(i);
							timer = servicedCustomer.selectedRecipe.recipeCraftTime / (0.25f * GameSystem.Instance.workerUnlocks[1] + 1);
							servicedCustomer.popupSystem.StartInteraction(timer);
							isIdle = false;
							AudioManager.Instance.Play("DrinkPour" + Random.Range(1, 4));
							break;
						}
					}
				}
			}
		}
		if (isIdle)
		{
			if (GamePlaySystem.Instance.customerList.Count != 0)
			{
				servicedCustomer = GamePlaySystem.Instance.customerList[0];
				GamePlaySystem.Instance.customerList.RemoveAt(0);
				timer = 3 / (0.5f * GameSystem.Instance.workerUnlocks[1] + 1);
				servicedCustomer.popupSystem.StartInteraction(timer);
				isIdle = false;
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
	
	#endregion

	#region GameplayMethods
	public void GetOrder(Customer customer)
	{
		AudioManager.Instance.Play("DrinkMix" + Random.Range(1, 5));
		customer.SelectDrink();
		GamePlaySystem.Instance.AddOrderToList(customer);
	}

	public void ServeOrder(Customer customer)
	{
		// Calculate money here
		// Calculate happiness here
		int sum = customer.selectedRecipe.recipePrice;
		if (customer.drinkValue <= 50)
		{
			sum = Mathf.RoundToInt(sum * (1 + Mathf.Floor((55 - customer.drinkValue) / 10) / 10));
			if (customer.customerId == 6)
			{
				sum = Mathf.RoundToInt(1.5f * sum);
			}
			customer.happiness += 10;
		}
		else if(customer.drinkValue >= 100 && customer.drinkValue < 150)
		{
			sum = Mathf.RoundToInt(sum * (0.5f + Mathf.Floor((150 - customer.drinkValue) / 10) / 20));
			customer.happiness -= 10;
		}
		else if (customer.drinkValue >= 150)
		{
			sum = Mathf.RoundToInt(sum * 0.5f);
			customer.happiness -= 25;
		}

		AudioManager.Instance.Play("Cash" + Random.Range(1,4));
		
		GameSystem.Instance.money += sum;
		// Debug.Log("Customer pays " + customer.selectedRecipe.recipePrice);
	}
	#endregion

	#endregion
}
