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
	public bool[] recipeUnlocks = new bool[5]; // arrayposition = recipeId
	public int[] workerUnlocks = new int[4]; // 0 = recipelimit, 1 = Speed, 2 = Multitasking, 3 = Quali
	[Space]
	[Header("Values")]
	public List<Recipe> recipeList = new List<Recipe>(); // Equipped recipes of worker
	public int[] ingredientAmount = new int[4];
	public int money = 0;
	public int happiness = 0;
	public List<ObjectProperties> objectList = new List<ObjectProperties>(); // Placed objects
                                                                             // Save array with 3 variable objects
    public SaveGameData savedData;
	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
            savedData = LoadGameSystem();

			if (savedData == null)
			{
				// First time
				InitializeGame();
			}
			else
			{
				LoadSavedGame();
			}
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}
	
	#region Recipes
	public void UnlockRecipe(int id)
	{
		if (id > recipeUnlocks.Length)
		{
			Debug.Log("Error recipe ID out of index");
		}
		else
		{
			recipeUnlocks[id] = true;
            Debug.Log("recipe unlocked");
        }
	}

	public void SetRecipe(int slot, int recipeId)
	{
		if (slot > workerUnlocks[0])
		{
			Debug.LogWarning("UI error? Slot not unlocked yet.");
		}
		else
		{
			recipeList[slot] = new Recipe(recipeId);
		}
	}
	#endregion

	#region Worker
	public void UpgradeWorker(int id)
	{
		if (id > workerUnlocks.Length)
		{
			Debug.Log("Error worker upgrade ID out of index");
		}
		else
		{
			workerUnlocks[id] += 1;
			if (id == 2)
			{
				GamePlaySystem.Instance.SpawnWorker();
			}
            Debug.Log("worker upgraded");
        }
	}
	#endregion

	#region Objects
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
	#endregion

	#region Ingredients
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
	#endregion

	#region Money
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
	#endregion

	#region Happiness
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
	#endregion

	#region TimeControl
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
	#endregion

	#region Save and Load
	public void InitializeGame()
	{
		Debug.Log("Initializing game");
		recipeUnlocks = new bool[5];
		workerUnlocks = new int[4];
		recipeUnlocks[0] = true;
		recipeUnlocks[1] = true;
		workerUnlocks[0] = 1;
		recipeList = new List<Recipe>(9)
				{
					new Recipe(1)
				};
		ingredientAmount = new int[4];
		money = 0;
		happiness = 0;
		objectList = new List<ObjectProperties>();
	}

	public static void SaveGameSystem()
	{
		Debug.Log("Saving Game");
		// Save all values into binary or smth
		BinaryFormatter formatter = new BinaryFormatter();
		//string path = Application.persistentDataPath + "/ingredients.bla";
		string path = "SaveGame.bla";
		FileStream stream = new FileStream(path, FileMode.Create);

		SaveGameData data = new SaveGameData();

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static SaveGameData LoadGameSystem()
	{
		// Load all values from binary or smth
		//string path = Application.persistentDataPath + "/ingredients.bla";
		string path = "SaveGame.bla";
		if (File.Exists(path))
		{
			Debug.Log("Loading Game");
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			SaveGameData data = formatter.Deserialize(stream) as SaveGameData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.Log("Save file not found in " + path);
			return null;
		}
	}

	public void LoadSavedGame()
	{
		ingredientAmount = savedData.ingredientAmount;
		money = savedData.money;
		happiness = savedData.happiness;
		recipeUnlocks = savedData.recipeUnlocks;
		workerUnlocks = savedData.workerUnlocks;
		objectList = savedData.objectList;
		recipeList = savedData.recipeList;
	}
	#endregion

	#endregion
}
