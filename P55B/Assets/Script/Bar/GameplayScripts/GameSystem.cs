using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
	public static GameSystem Instance { get; private set; }

	#region Variables
	[Header("Unlocks")]
	public bool[] recipeUnlocks = new bool[5];
	public int[] workerUnlocks = new int[4];
	[Space]
	[Header("Values")]
	public int[] objectAmount = new int[5]; // Change this to bool[] if player only needs to buy once and can place freely
	public int[] ingredientAmount = new int[4];
	public int money = 0;
	public int happiness = 0;
	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void UnlockRecipe(int id)
	{
		if (id > recipeUnlocks.Length)
		{
			Debug.Log("Error recipe ID out of index");
		}
		else
		{
			recipeUnlocks[id] = true;
		}
	}

	public void UpgradeWorker(int id)
	{
		if (id > workerUnlocks.Length)
		{
			Debug.Log("Error worker upgrade ID out of index");
		}
		else
		{
			workerUnlocks[id] += 1;
		}
	}

	public void AddObject(int id, int amount)
	{
		if (id > objectAmount.Length)
		{
			Debug.Log("Error object ID out of index");
		}
		else
		{
			objectAmount[id] += amount;
		}
	}

	public void SubObject(int id, int amount)
	{
		if (id > objectAmount.Length)
		{
			Debug.Log("Error object ID out of index");
		}
		else
		{
			objectAmount[id] -= amount;
		}
	}

	public void SetObject(int id, int amount)
	{
		if (id > objectAmount.Length)
		{
			Debug.Log("Error object ID out of index");
		}
		else
		{
			objectAmount[id] = amount;
		}
	}

	public void AddIngredient(int id, int amount)
	{
		if (id > ingredientAmount.Length)
		{
			Debug.Log("Error ingredient ID out of index");
		}
		else
		{
			ingredientAmount[id] += amount;
		}
	}

	public void SubIngredient(int id, int amount)
	{
		if (id > ingredientAmount.Length)
		{
			Debug.Log("Error ingredient ID out of index");
		}
		else
		{
			ingredientAmount[id] -= amount;
		}
	}

	public void SetIngredient(int id, int amount)
	{
		if (id > ingredientAmount.Length)
		{
			Debug.Log("Error ingredient ID out of index");
		}
		else
		{
			ingredientAmount[id] = amount;
		}
	}

	public void AddMoney(int amount)
	{
		money += amount;
	}

	public void SubMoney(int amount)
	{
		money -= amount;
	}

	public void SetMoney(int amount)
	{
		money = amount;
	}

	public void AddHappiness(int amount)
	{
		happiness += amount;
	}

	public void SubHappiness(int amount)
	{
		happiness -= amount;
	}

	public void SetHappiness(int amount)
	{
		happiness = amount;
	}

	public void SaveGameSystem()
	{
		// Save all values into binary or smth
	}

	public void LoadGameSystem()
	{
		// Load all values from binary or smth
	}
	#endregion
}
