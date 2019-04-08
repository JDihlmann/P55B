using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySystem : MonoBehaviour
{
	public static GamePlaySystem Instance { get; private set; }

	#region Variables
	//[Header("Components")]
	public Sprite[] popupImage;
	private CustomerSpawner spawner;
	//[Space]
	[Header("Variables")]
	public List<Customer> customerList = new List<Customer>();
	public List<Customer> orderingCustomerList = new List<Customer>();
	public List<GameObject> availableSeatList = new List<GameObject>();
	public GameObject bar;
	public GameObject exit;
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

	public void Start()
	{
		bar = GameObject.FindGameObjectWithTag("Bar");
		exit = GameObject.FindGameObjectWithTag("Exit");

		SpawnWorker();
		for (int i = 0; i < GameSystem.Instance.workerUnlocks[2]; i++)
		{
			SpawnWorker();
		}
	}

	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.F1))
		{
			GameSystem.Instance.UpgradeWorker(1);
			for (int i = 0; i < 4; i++)
			{
				GameSystem.Instance.ingredientAmount[i] += 1;
			}
		}
		if (Input.GetKeyUp(KeyCode.Q))
		{
			GameSystem.Instance.AddMoney(20);
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			GameSystem.Instance.AddHappiness(3);
		}
		if (Input.GetKeyUp(KeyCode.KeypadPlus))
		{
			GameSystem.Instance.AddTime(0.25f);
		}
		if (Input.GetKeyUp(KeyCode.KeypadMinus))
		{
			GameSystem.Instance.SubTime(0.25f);
		}
	}

	public void AddCustomerToList(Customer customer)
	{
		customerList.Add(customer);
	}

	public void LeavingCustomer(GameObject customer)
	{
		if (customerList.Contains(customer.GetComponent<Customer>()))
		{
			Debug.Log("Removed Customer");
			customerList.Remove(customer.GetComponent<Customer>());
		}
		spawner.LeaveCustomer(customer);
	}

	public void DestroyCustomer(GameObject customer)
	{
		spawner.DeleteCustomer(customer);
	}

	public void ResetCustomerDestination()
	{
		foreach (GameObject customer in spawner.customerList)
		{
			customer.GetComponent<Customer>().ResetDestination();
		}
	}

	public void AddOrderToList(Customer customer)
	{
		orderingCustomerList.Add(customer);
	}

	public void SpawnWorker()
	{
		GameObject newWorker = Instantiate(new GameObject("Worker"), transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
		newWorker.transform.parent = gameObject.transform;
		newWorker.AddComponent<Worker>();
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

	public bool IngredientCost(Recipe selectedRecipe)
	{
		int[] ingredientAmount = GameSystem.Instance.ingredientAmount;
		bool hasEnough = true;

		for (int i = 0; i < 4; i++)
		{
			if (ingredientAmount[i] - selectedRecipe.recipeIngredientCost[i] < 0)
			{
				hasEnough = false;
			}
		}

		if (hasEnough)
		{
			for (int i = 0; i < 4; i++)
			{
				ingredientAmount[i] -= selectedRecipe.recipeIngredientCost[i];
			}
			GameSystem.Instance.ingredientAmount = ingredientAmount;
			return true;
		}
		return false;
	}
	#endregion
}
