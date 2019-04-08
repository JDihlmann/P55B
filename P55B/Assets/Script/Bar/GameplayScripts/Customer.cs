using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public PopupSystem popupSystem;
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
		popupSystem = gameObject.GetComponent<PopupSystem>();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		InitializeCustomer(customerId);
		AudioManager.Instance.Play("PortalOut");
	}

	void Update()
	{
		popupSystem.UpdateCustomerHappiness(happiness);

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
					// Debug.Log("Kunde hat zu lang gewartet");
					happiness = Random.Range(0f, 33f);
					currentState = CustomerState.LEAVE;
					GamePlaySystem.Instance.LeavingCustomer(gameObject);
					isWaiting = false;
					destination = null;
					popupSystem.CreatePopup(1);
					CalculateHappiness();
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

	private void LateUpdate()
	{
		if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
		{
			if (agent.velocity.normalized != Vector3.zero)
			{
				transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
			}
		}
		else
		{
			if (destination != null)
			{
				if (destination != GamePlaySystem.Instance.bar && destination != GamePlaySystem.Instance.exit)
				{
					Vector3 targetPosition = new Vector3(-destination.transform.position.x, transform.position.y, -destination.transform.position.z);
					transform.LookAt(targetPosition);
				}
				else
				{
					Vector3 targetPosition = new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z);
					transform.LookAt(targetPosition);
				}
			}
		}
	}

	#region SystemMethods
	private void InitializeCustomer(int newId)
	{
		actionTimer = Random.Range(0f, 2f);
		atDestination = false;
		customerId = newId;
		currentState = CustomerState.ENTER;
		happiness = 80;
		happinessLossRate = Random.Range(4f, 8f);
		isWaiting = false;
		waitingTimer = 0f;
		waitingMaximum = Random.Range(5f, 10f);
		pathTries = 0;
		path = new NavMeshPath();

		int remainder = 100;
		int random = 0;
		switch (customerId)
		{
			// 0 - Sweet, 1 - Sour, 2 - Salty, 3 - Bitter.In Percent.

			case 0: // Standard Blue: salty, bitter, sour
				customerName = "Blue";
				random = Random.Range(40, 71);
				remainder -= random;
				customerPreference[2] = random;
				random = Random.Range(Mathf.CeilToInt(0.5f * remainder), remainder + 1);
				remainder -= random;
				customerPreference[3] = random;
				customerPreference[1] = remainder;
				break;
			case 1: // Standard Green: sour, bitter, salty
				customerName = "Green";
				random = Random.Range(40, 71);
				remainder -= random;
				customerPreference[1] = random;
				random = Random.Range(Mathf.CeilToInt(0.5f * remainder), remainder + 1);
				remainder -= random;
				customerPreference[3] = random;
				customerPreference[2] = remainder;
				break;
			case 2: // Standard Orange: sweet, sour
				customerName = "Orange";
				random = Random.Range(40, 51);
				remainder -= random;
				customerPreference[0] = random;
				random = Random.Range(40, 51);
				remainder -= random;
				customerPreference[1] = random;
				customerPreference[Random.Range(2, 4)] = remainder;
				break;
			case 3: // Sim (Random distribution)
				customerName = "Sims";
				for (int i = 0; i < 3; i++)
				{
					random = Random.Range(0, remainder + 1);
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
			case 4: // Angel (Sweet)
				customerName = "Angel";
				happinessLossRate = Random.Range(2f, 4f);
				random = Random.Range(50, 101);
				remainder -= random;
				customerPreference[0] = random;
				random = Mathf.FloorToInt(remainder / 3);
				remainder -= random;
				customerPreference[1] = random;
				random = Mathf.FloorToInt(remainder / 3);
				remainder -= random;
				customerPreference[3] = random;
				customerPreference[2] = remainder;
				break;
			case 5: // Devil (bitter, sour)
				customerName = "Devil";
				happinessLossRate = Random.Range(8f, 10f);
				random = Random.Range(40, 51);
				remainder -= random;
				customerPreference[3] = random;
				random = Random.Range(40, 51);
				remainder -= random;
				customerPreference[1] = random;
				customerPreference[Random.Range(2, 4)] = remainder;
				break;
			case 6: // Style (neutral)
				customerName = "Style";
				for (int i = 0; i < 4; i++)
				{
					customerPreference[i] = 25;
				}
				break;
			default:
				Debug.Log("No valid Customer ID");
				break;
		}
	}

	private void GenerateOffset()
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
	private void SetNewDestination()
	{
		switch (currentState)
		{
			case CustomerState.ENTER:
				destination = GamePlaySystem.Instance.bar;
				break;
			case CustomerState.ORDER:
				destination = GamePlaySystem.Instance.bar;
				break;
			case CustomerState.WAITING:
				destination = GamePlaySystem.Instance.bar;
				break;
			case CustomerState.SEATING:
				destination = GamePlaySystem.Instance.GetAvailableSeat();
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
				destination = GamePlaySystem.Instance.GetAvailableSeat();
				break;
			case CustomerState.LEAVE:
				destination = GamePlaySystem.Instance.exit;
				break;
			default:
				Debug.Log("Error with SetNewDestination");
				break;
		}
	}

	private void GoToDestination()
	{
		actionTimer = Random.Range(1f, 1.5f);
		if (CalculateNewPath() == true)
		{
			if (destination == GamePlaySystem.Instance.bar)
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
			if (destination == GamePlaySystem.Instance.bar)
			{
				if (pathTries > 3)
				{
					Debug.Log("Kunde kommt nicht rein");
					happiness = 50;
					GamePlaySystem.Instance.LeavingCustomer(gameObject);
					destination = null;
					currentState = CustomerState.LEAVE;
				}
				else
				{
					agent.SetDestination(destination.transform.position + barOffset);
				}
			}
			else if (destination == GamePlaySystem.Instance.exit)
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

					GameObject tempDestination = GamePlaySystem.Instance.GetAvailableSeat();
					GamePlaySystem.Instance.AddSeatToList(destination);
					destination = tempDestination;
					pathTries = 0;
				}
			}
		}
	}

	private bool CalculateNewPath()
	{
		if(destination == GamePlaySystem.Instance.bar)
		{
			agent.CalculatePath(destination.transform.position + barOffset, path);
		}
		else
		{
			agent.CalculatePath(destination.transform.position, path);
		}

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
				GamePlaySystem.Instance.AddCustomerToList(this);
				currentState = CustomerState.ORDER;
				break;
			case CustomerState.ORDER:
				isWaiting = true;
				currentState = CustomerState.WAITING;
				break;
			case CustomerState.WAITING:
				isWaiting = true;
				destination = null;
				popupSystem.CreatePopup(0);
				currentState = CustomerState.SEATING;
				break;
			case CustomerState.SEATING:
				drinkTimer = Random.Range(15f, 30f);
				if (atDestination)
				{
					popupSystem.StartInteraction(drinkTimer);
				}
				currentState = CustomerState.CONSUMING;
				break;
			case CustomerState.CONSUMING:
				GamePlaySystem.Instance.AddSeatToList(destination);
				GamePlaySystem.Instance.LeavingCustomer(gameObject);
				destination = null;
				CalculateHappiness();
				currentState = CustomerState.LEAVE;
				break;
			case CustomerState.LEAVE:
				GamePlaySystem.Instance.DestroyCustomer(gameObject);
				break;
			default:
				Debug.Log("Error with NextState");
				break;
		}
	}

	public void SelectDrink()
	{
		int bestValue = 201;
		for (int i = 0; i < GameSystem.Instance.recipeList.Count; i++)
		{
			if (GameSystem.Instance.recipeList[i].recipeId != -1)
			{
				int currentValue = 0;
				for (int k = 0; k < 4; k++)
				{
					currentValue += Mathf.Abs(customerPreference[k] - GameSystem.Instance.recipeList[i].recipeStats[k]);
				}
				if (currentValue <= bestValue)
				{
					bestValue = currentValue;
					selectedRecipe = GameSystem.Instance.recipeList[i];
				}
			}
		}
		drinkValue = bestValue;
		// Debug.Log(selectedRecipe.recipeName + " with value " + bestValue);
	}

	private void CalculateHappiness()
	{
		if (happiness <= 33)
		{
			GameSystem.Instance.SubHappiness(1);
			if (customerId == 5)
			{
				GameSystem.Instance.SubHappiness(1);
				for (int i = 0; i < 5; i++)
				{
					GameSystem.Instance.SubIngredient(Random.Range(0, 5), 1);
				}
			}
			if (happiness <= 15)
			{
				GameSystem.Instance.SubHappiness(1);
				if (customerId == 5)
				{
					GameSystem.Instance.SubHappiness(1);
					for (int i = 0; i < 5; i++)
					{
						GameSystem.Instance.SubIngredient(Random.Range(0, 5), 1);
					}
				}
				popupSystem.CreatePopup(2);
			}
			else
			{
				popupSystem.CreatePopup(3);
			}
		}
		else if (happiness >= 75)
		{
			GameSystem.Instance.AddHappiness(1);
			if (customerId == 4)
			{
				GameSystem.Instance.AddHappiness(1);
			}
			if (happiness >= 90)
			{
				GameSystem.Instance.AddHappiness(1);
				if (customerId == 4)
				{
					GameSystem.Instance.AddHappiness(1);
				}
				popupSystem.CreatePopup(4);
			}
			else
			{
				popupSystem.CreatePopup(5);
			}
		}
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
			case CustomerState.ORDER:
				if (other.tag == "Bar")
				{
					atDestination = true;
				}
				break;
			case CustomerState.WAITING:
				if (other.tag == "Bar")
				{
					atDestination = true;
				}
				break;
			case CustomerState.SEATING:
				if (other.gameObject == destination)
				{
					atDestination = true;
				}
				break;
			case CustomerState.CONSUMING:
				if (other.gameObject == destination)
				{
					popupSystem.StartInteraction(drinkTimer);
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
