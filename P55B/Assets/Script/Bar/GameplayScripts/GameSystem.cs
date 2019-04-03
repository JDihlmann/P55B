using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
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
	public List<Recipe> recipeList = new List<Recipe>(); // Equipped recipes of worker
	public int[] ingredientAmount = new int[4];
	public int money = 0;
	public int happiness = 0;
	public List<ObjectProperties> objectList = new List<ObjectProperties>(); // Placed objects
	// Save array with 3 variable objects
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
			return;
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

	public void AddObject(ObjectProperties placedObject)
	{
		if (objectList.Contains(placedObject))
		{
			Debug.Log("Don't add same object again");
		}
		else
		{
			objectList.Add(placedObject);
		}
	}

	public void SubObject(ObjectProperties placedObject)
	{
		if (objectList.Contains(placedObject))
		{
			objectList.Remove(placedObject);
		}
		else
		{
			Debug.Log("No viable gameObject");
		}
	}

	public ObjectProperties PopObject()
	{
		if (objectList.Count > 0)
		{
			ObjectProperties temp = objectList[0];
			objectList.RemoveAt(0);
			return temp;
		}
		return null;
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

	public void AddTime(float amount)
	{
		Time.timeScale += amount;
		if (Time.timeScale > 3)
		{
			Time.timeScale = 3;
		}
	}

	public void SubTime(float amount)
	{
		Time.timeScale -= amount;
		if (Time.timeScale < 0.25f)
		{
			Time.timeScale = 0.25f;
		}
	}

	public void SetTime(float amount)
	{
		Time.timeScale = amount;
		if (Time.timeScale > 3)
		{
			Time.timeScale = 3;
		}
		else if (Time.timeScale < 0.25f)
		{
			Time.timeScale = 0.25f;
		}
	}

	public void SaveGameSystem()
	{
		// Save all values into binary or smth
		BinaryFormatter formatter = new BinaryFormatter();
		//string path = Application.persistentDataPath + "/ingredients.bla";
		string path = "SaveGame.bla";
		FileStream stream = new FileStream(path, FileMode.Create);

		SaveGameData data = new SaveGameData();

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public SaveGameData LoadGameSystem()
	{
		// Load all values from binary or smth
		//string path = Application.persistentDataPath + "/ingredients.bla";
		string path = "SaveGame.bla";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			SaveGameData data = formatter.Deserialize(stream) as SaveGameData;
			stream.Close();

			return data;

		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}
	#endregion
}
