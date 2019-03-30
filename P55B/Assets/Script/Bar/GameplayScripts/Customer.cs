using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public GamePlaySystem gameSystem;
	private NavMeshAgent agent;
	[Space]
	[Header("Variables")]
	public string customerName;
	public int customerId;
	public int[] customerPreference = new int[4]; // 0-Sweet, 1-Sour, 2-Salty, 3-Bitter. In Percent.
	public int drinkValue;
	public Recipe selectedRecipe;
	public float actionTimer;

	public float happiness;
	public float happinessLossRate;
	public bool isWaiting;
	public float waitingTimer;
	public float waitingMaximum;

	public float drinkTimer;

	private Vector3 barOffset;
	private bool atDestination;
	private GameObject destination;
	private NavMeshPath path;
	private int pathTries;

	

	public enum CustomerState
	{
		ENTER,
		ORDER,
		WAITING,
		SEATING,
		CONSUMING,
		LEAVE
	}
	public CustomerState currentState;
	#endregion

	#region Methods
	void Start()
	{
		GenerateOffset();
		agent = gameObject.GetComponent<NavMeshAgent>();
		InitializeCustomer(0);
	}

	void Update()
	{
		if (isWaiting)
		{
			if (waitingTimer < waitingMaximum)
			{
				waitingTimer += Time.deltaTime;
				if (waitingTimer >= 0.5 * waitingMaximum)
				{
					happiness -= happinessLossRate * Time.deltaTime;
				}
			}
			else
			{
				if (currentState != CustomerState.SEATING)
				{
					Debug.Log("Kunde hat zu lang gewartet");
					happiness = Random.Range(0f, 33f);
					currentState = CustomerState.LEAVE;
					gameSystem.LeavingCustomer(gameObject);
					isWaiting = false;
					destination = null;
				}
				else
				{
					NextState();
					atDestination = false;
					destination = null;
					isWaiting = false;
				}
			}
		}

		if (drinkTimer > 0)
		{
			if (currentState == CustomerState.CONSUMING)
			{
				if (actionTimer > 0)
				{
					actionTimer -= Time.deltaTime;
				}
				else
				{
					if (!atDestination)
					{
						if (destination == null)
						{
							SetNewDestination();
						}
						else
						{
							GoToDestination();
						}
						
					}
				}

				if (!atDestination)
				{
					if (destination == null)
					{
						drinkTimer -= Time.deltaTime;
						happiness -= happinessLossRate * Time.deltaTime;
					}
					else
					{
						if (pathTries >= 0)
						{
							drinkTimer -= Time.deltaTime;
							happiness -= happinessLossRate * Time.deltaTime;
						}
					}
				}
				else
				{
					drinkTimer -= Time.deltaTime;
				}
			}
		}
		else
		{
			if (actionTimer > 0)
			{
				actionTimer -= Time.deltaTime;
			}
			else
			{
				if (currentState == CustomerState.CONSUMING)
				{
					atDestination = true;
				}
				if (atDestination)
				{
					NextState();
				}
				else
				{
					if (destination == null)
					{
						SetNewDestination();
					}
					else
					{
						GoToDestination();
					}
				}
			}
		}

	}

	#region SystemMethods
	public void InitializeCustomer(int newId)
	{
		actionTimer = Random.Range(0f, 2f);
		atDestination = false;
		customerId = newId;
		currentState = CustomerState.ENTER;
		happiness = 100;
		happinessLossRate = Random.Range(2f, 4f);
		isWaiting = false;
		waitingTimer = 0f;
		waitingMaximum = Random.Range(5f, 10f);
		pathTries = 0;
		path = new NavMeshPath();
		switch (customerId)
		{
			case 0: // Standard (Random distribution)
				customerName = "Kevin";
				int remainder = 100;
				for (int i = 0; i < 3; i++)
				{
					int random = Random.Range(0, remainder);
					remainder -= random;
					customerPreference[i] = random;
				}
				customerPreference[3] = remainder;
				for (int i = 0; i < 3; i++)
				{
					int index = Random.Range(0, i);
					if (index != i)
					{
						int temp = customerPreference[i];
						customerPreference[i] = customerPreference[index];
						customerPreference[index] = temp;
					}
				}
				break;
			default:
				Debug.Log("No valid Customer ID");
				break;
		}
	}

	public void GenerateOffset()
	{
		int random1 = Random.Range(0, 2); // X or Z axis
		int random2 = Random.Range(0, 2); // +1 or -1
		if (random1 == 0)
		{
			if (random2 == 0)
			{
				barOffset = new Vector3(0, 0, -1);
			}
			else
			{
				barOffset = new Vector3(0, 0, 1);
			}
		}
		else
		{
			if (random2 == 0)
			{
				barOffset = new Vector3(-1, 0, 0);
			}
			else
			{
				barOffset = new Vector3(1, 0, 0);
			}
		
		}
	}
	#endregion

	#region GameplayMethods
	public void SetNewDestination()
	{
		switch (currentState)
		{
			case CustomerState.ENTER:
				destination = gameSystem.bar;
				break;
			case CustomerState.ORDER:
				destination = gameSystem.bar;
				break;
			case CustomerState.WAITING:
				destination = gameSystem.bar;
				break;
			case CustomerState.SEATING:
				destination = gameSystem.GetAvailableSeat();
				if (destination != null)
				{
					isWaiting = false;
				}
				else
				{
					isWaiting = true;
				}
				break;
			case CustomerState.CONSUMING:
				destination = gameSystem.GetAvailableSeat();
				break;
			case CustomerState.LEAVE:
				destination = gameSystem.exit;
				break;
			default:
				Debug.Log("Error with SetNewDestination");
				break;
		}
	}

	public void GoToDestination()
	{
		actionTimer = Random.Range(1f, 1.5f);
		if (CalculateNewPath() == true)
		{
			if (destination == gameSystem.bar)
			{
				agent.SetDestination(destination.transform.position + barOffset);
			}
			else
			{
				agent.SetDestination(destination.transform.position);
			}
		}
		else
		{
			if (destination == gameSystem.bar)
			{
				if (pathTries > 3)
				{
					Debug.Log("Kunde kommt nicht rein");
					happiness = 50;
					gameSystem.LeavingCustomer(gameObject);
					destination = null;
					currentState = CustomerState.LEAVE;
				}
				else
				{
					agent.SetDestination(destination.transform.position + barOffset);
				}
			}
			else if (destination == gameSystem.exit)
			{
				if (pathTries > 3)
				{
					Debug.Log("Kunde kommt nicht raus");
					NextState();
				}
			}
			else
			{
				if (pathTries > 1)
				{
					if (currentState == CustomerState.SEATING)
					{
						NextState();
						atDestination = false;
						isWaiting = false;
					}

					GameObject tempDestination = gameSystem.GetAvailableSeat();
					gameSystem.AddSeatToList(destination);
					destination = tempDestination;
					pathTries = 0;
				}
			}
		}
	}

	public bool CalculateNewPath()
	{
		if(destination == gameSystem.bar)
		{
			agent.CalculatePath(destination.transform.position + barOffset, path);
		}
		else
		{
			agent.CalculatePath(destination.transform.position, path);
		}
		Debug.Log("Path calculated: " + path.status);
		if (path.status != NavMeshPathStatus.PathComplete)
		{
			pathTries += 1;
			return false;
		}
		else
		{
			pathTries = 0;
			return true;
		}
	}

	public void NextState()
	{
		if (currentState != CustomerState.SEATING)
		{
			atDestination = false;
		}
		switch (currentState)
		{
			case CustomerState.ENTER:
				isWaiting = true;
				gameSystem.AddCustomerToList(this);
				currentState = CustomerState.ORDER;
				break;
			case CustomerState.ORDER:
				isWaiting = true;
				currentState = CustomerState.WAITING;
				break;
			case CustomerState.WAITING:
				isWaiting = true;
				destination = null;
				currentState = CustomerState.SEATING;
				break;
			case CustomerState.SEATING:
				drinkTimer = Random.Range(5f, 10f);
				currentState = CustomerState.CONSUMING;
				break;
			case CustomerState.CONSUMING:
				gameSystem.AddSeatToList(destination);
				gameSystem.LeavingCustomer(gameObject);
				destination = null;
				currentState = CustomerState.LEAVE;
				break;
			case CustomerState.LEAVE:
				if (happiness <= 33)
				{
					GameSystem.Instance.SubHappiness(1);
					if (happiness <= 15)
					{
						GameSystem.Instance.SubHappiness(1);
					}
				}
				else if (happiness >= 75)
				{
					GameSystem.Instance.AddHappiness(1);
					if (happiness >= 90)
					{
						GameSystem.Instance.AddHappiness(1);
					}
				}
				gameSystem.DestroyCustomer(gameObject);
				break;
			default:
				Debug.Log("Error with NextState");
				break;
		}
	}

	public void SelectDrink()
	{
		int bestValue = 1000;
		for (int i = 0; i < gameSystem.totalRecipeList.Count; i++)
		{
			int currentValue = 0;
			for (int k = 0; k < 4; k++)
			{
				currentValue += Mathf.Abs(customerPreference[k] - gameSystem.totalRecipeList[i].recipeStats[k]);
			}
			if (currentValue < bestValue)
			{
				bestValue = currentValue;
				selectedRecipe = gameSystem.totalRecipeList[i];
			}
		}
		drinkValue = bestValue;
		Debug.Log(selectedRecipe.recipeName + " with value " + bestValue);
	}

	private void OnTriggerEnter(Collider other)
	{
		switch (currentState)
		{
			case CustomerState.ENTER:
				if (other.tag == "Bar")
				{
					atDestination = true;
				}
				break;
			case CustomerState.SEATING:
				if (other.gameObject == destination)
				{
					//gameObject.transform.position = other.gameObject.transform.position;
					atDestination = true;
				}
				break;
			case CustomerState.CONSUMING:
				if (other.gameObject == destination)
				{
					//gameObject.transform.position = other.gameObject.transform.position;
					atDestination = true;
				}
				break;
			case CustomerState.LEAVE:
				if (other.tag == "Exit")
				{
					atDestination = true;
				}
				break;
			default:
				Debug.Log("Error with OnTriggerEnter" + currentState);
				break;
		}
	}

	#endregion

	#endregion
}
