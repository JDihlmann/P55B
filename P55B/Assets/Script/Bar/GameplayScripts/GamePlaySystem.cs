using System.Collections;
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

	public void Start()
	{
		SpawnWorker();
		for (int i = 0; i < GameSystem.Instance.workerUnlocks[2]; i++)
		{
			SpawnWorker();
		}
	}

	public void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			GameSystem.Instance.UpgradeWorker(1);
		}
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
	#endregion
}
