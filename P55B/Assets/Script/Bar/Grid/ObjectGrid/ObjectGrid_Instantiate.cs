using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid_Instantiate : MonoBehaviour {

	// Integer Size 
	private int width;
	private int length;

	// Grid for objects
	public GameObject[,] objectGrid;
	public Vector2Int[,] integerObjectGrid;

	// S1 objects
	public GameObject df_Chair;
	public GameObject df_CouchS;
	public GameObject df_CouchL;
	public GameObject df_Tabel;
	public GameObject s1_Chair_Purple;
	public GameObject s1_Chair_Red;
	public GameObject s1_Chair_Yellow;
	public GameObject s1_CouchS_Purple;
	public GameObject s1_CouchS_Red;
	public GameObject s1_CouchS_Yellow;
	public GameObject s1_CouchL_Purple;
	public GameObject s1_CouchL_Red;
	public GameObject s1_CouchL_Yellow;
	public GameObject s1_Tabel_Purple;
	public GameObject s1_Tabel_Red;
	public GameObject s1_Tabel_Yellow;

	// Empty integer object element
	public Vector2Int emptyIntObjectElement = new Vector2Int(-1,-1); 

	// Placement component
	private ObjectGrid_Placement objectGridPlacement; 

	// Run Once
	private bool runOnce = true; 

	// Generate grid in edit mode
	public void GenerateObjectGrid(int gridWidth, int gridLength) {
		width = gridWidth;
		length = gridLength; 

		InitObjectGrid();
	}

	void Update() {
		// Not in Start because we have to wait until
		// ObjectGrid_Placement & ObjectGrid_Operations
		// Are loaded (No SEO does not help here)
		if(runOnce) {
			LoadObjects();
			TestBar(); 
			runOnce = false; 
		}
	}


	private void InitObjectGrid() {
		objectGrid = new GameObject[width,length];
		integerObjectGrid = new Vector2Int[width,length];

		// Fill all arrays with [-1,-1]
		for (int i = 0; i < integerObjectGrid.GetLength(0); i++) {
			for (int j = 0; j < integerObjectGrid.GetLength(1); j++) {
				integerObjectGrid[i,j] = emptyIntObjectElement;
			}
		}
	}

	public void EmptyObjectGrid() {
		// Fill all arrays with [-1,-1]
		for (int i = 0; i < integerObjectGrid.GetLength(0); i++) {
			for (int j = 0; j < integerObjectGrid.GetLength(1); j++) {
				integerObjectGrid[i,j] = emptyIntObjectElement;
			}
		}

		// Fill all arrays with null
		for (int i = 0; i < objectGrid.GetLength(0); i++) {
			for (int j = 0; j < objectGrid.GetLength(1); j++) {
				Destroy(objectGrid[i,j]);
				objectGrid[i,j] = null;
			}
		}
	}
	
	public void LoadObjects() {
		// !!! BUILD SCRIPT MUST BE ENABLED TO LOAD OBJECTS !!!
		objectGridPlacement = GetComponent<ObjectGrid_Placement>();

		foreach(ObjectProperties properties in GameSystem.Instance.objectList) {
			int ID = properties.objectId; 
			Vector2Int position = new Vector2Int (properties.objectPositionX, properties.objectPositionY); 
			float rotation = properties.objectRotation; 
			
			GameObject objectPrefab = GetGameObjectForId(ID);
		
			// No Prefab for ID 
			if (objectPrefab != null) {
				GameObject gameObject = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
				gameObject.transform.parent = transform;
				objectGridPlacement.PlaceObjectOnGridWithRotation(gameObject, position, Vector2Int.zero, rotation);
			} 
		}
	}

	public void SaveObjects () {
		// Clear Object List
		GameSystem.Instance.objectList.Clear();

		// Save Object List
		for (int i = 0; i < objectGrid.GetLength(0); i++) {
			for (int j = 0; j < objectGrid.GetLength(1); j++) {
				if(objectGrid[i,j] != null) {
					Object_Values objectValues = objectGrid[i,j].GetComponent<Object_Values>();
					float rotation = objectGrid[i,j].transform.eulerAngles.y; 
					rotation = rotation == -90 ? 270 : rotation; 
					ObjectProperties objectProperties = new ObjectProperties(objectValues.ID, objectValues.placedPosition, rotation); 
					GameSystem.Instance.AddObject(objectProperties);
				}
			}
		}

		// Save Game
		GameSystem.SaveGameSystem(); 
	}


	public void SpawnNewObjectWithID(int id, int price) {
        // TODO: ID should select right prefab
		GameObject objectPrefab = GetGameObjectForId(id);

		// No Prefab for ID 
		if (objectPrefab == null) 
			return; 
		

		GameObject gameObject = Instantiate(objectPrefab, new Vector3(-100, -100 ,-100), Quaternion.identity);
		gameObject.transform.parent = transform;

		GameObject gameObjectCollider = gameObject.transform.Find("ColliderCube (1)").gameObject;

		if (gameObjectCollider != null) {
			objectGridPlacement = GetComponent<ObjectGrid_Placement>();
			objectGridPlacement.MoveSpawnObjectAtCollider(gameObjectCollider, price); 
		}
	}

	private GameObject GetGameObjectForId(int id) {
		switch (id) {
			case 0: 
				return df_Chair;
			case 1: 
				return df_CouchS;
			case 2: 
				return df_CouchL;
			case 3: 
				return df_Tabel;
			case 4: 
				return s1_Chair_Yellow;
			case 5: 
				return s1_Chair_Red;
			case 6: 
				return s1_Chair_Purple;
			case 7: 
				return s1_CouchS_Yellow;
			case 8: 
				return s1_CouchS_Red;
			case 9: 
				return s1_CouchS_Purple;
			case 10: 
				return s1_CouchL_Yellow;
			case 11: 
				return s1_CouchL_Red;
			case 12: 
				return s1_CouchL_Purple;
			case 13: 
				return s1_Tabel_Yellow;
			case 14: 
				return s1_Tabel_Red;
			case 15: 
				return s1_Tabel_Purple;

			default:
				return null; 
		}
	}

	public void TestBar() {
		objectGridPlacement = GetComponent<ObjectGrid_Placement>();

		GameObject chair = Instantiate(s1_Chair_Purple, Vector3.zero, Quaternion.identity);
		GameObject chair1 = Instantiate(s1_Chair_Yellow, Vector3.zero, Quaternion.identity);
		GameObject chair2 = Instantiate(s1_Chair_Yellow, Vector3.zero, Quaternion.identity);
		GameObject chair3 = Instantiate(s1_Chair_Purple, Vector3.zero, Quaternion.identity);
		GameObject chair4 = Instantiate(s1_Chair_Red, Vector3.zero, Quaternion.identity);
		GameObject chair5 = Instantiate(s1_Chair_Purple, Vector3.zero, Quaternion.identity);
		GameObject chair6 = Instantiate(s1_Chair_Purple, Vector3.zero, Quaternion.identity);
		GameObject chair7 = Instantiate(s1_Chair_Red, Vector3.zero, Quaternion.identity);
		GameObject chair8 = Instantiate(s1_Chair_Red, Vector3.zero, Quaternion.identity);
		GameObject chair9 = Instantiate(s1_Chair_Red, Vector3.zero, Quaternion.identity);

		GameObject table = Instantiate(s1_Tabel_Purple, Vector3.zero, Quaternion.identity);
		GameObject table1 = Instantiate(s1_Tabel_Yellow, Vector3.zero, Quaternion.identity);
		GameObject table2 = Instantiate(s1_Tabel_Yellow, Vector3.zero, Quaternion.identity);
		GameObject table3 = Instantiate(s1_Tabel_Purple, Vector3.zero, Quaternion.identity);
		GameObject table4 = Instantiate(s1_Tabel_Red, Vector3.zero, Quaternion.identity);
		GameObject table5 = Instantiate(s1_Tabel_Red, Vector3.zero, Quaternion.identity);

		GameObject couchs = Instantiate(s1_CouchS_Red, Vector3.zero, Quaternion.identity);
		GameObject couchs1 = Instantiate(s1_CouchS_Red, Vector3.zero, Quaternion.identity);

		GameObject couchl1 = Instantiate(s1_CouchL_Red, Vector3.zero, Quaternion.identity);

		chair.transform.parent = transform;
		chair1.transform.parent = transform;
		chair2.transform.parent = transform;
		chair3.transform.parent = transform;
		chair4.transform.parent = transform;
		chair5.transform.parent = transform;
		chair6.transform.parent = transform;
		chair7.transform.parent = transform;
		chair8.transform.parent = transform;
		chair9.transform.parent = transform;

		table.transform.parent = transform;
		table1.transform.parent = transform;
		table2.transform.parent = transform;
		table3.transform.parent = transform;
		table4.transform.parent = transform;
		table5.transform.parent = transform;

		couchs.transform.parent = transform;
		couchs1.transform.parent = transform;

		couchl1.transform.parent = transform;

		objectGridPlacement.PlaceObjectOnGridWithRotation(chair, new Vector2Int(1,1), Vector2Int.zero, 180);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair1, new Vector2Int(3,1), Vector2Int.zero, 180);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair2, new Vector2Int(6,1), Vector2Int.zero, 180);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair3, new Vector2Int(8,1), Vector2Int.zero, 180);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair4, new Vector2Int(7,6), Vector2Int.zero, 180);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair5, new Vector2Int(1,3), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair6, new Vector2Int(8,3), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair7, new Vector2Int(7,8), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair8, new Vector2Int(6,7), Vector2Int.zero, 270);
		objectGridPlacement.PlaceObjectOnGridWithRotation(chair9, new Vector2Int(8,7), Vector2Int.zero, 90);

		objectGridPlacement.PlaceObjectOnGridWithRotation(table, new Vector2Int(1,2), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(table1, new Vector2Int(3,2), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(table2, new Vector2Int(6,2), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(table3, new Vector2Int(8,2), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(table4, new Vector2Int(7,7), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(table5, new Vector2Int(2,7), Vector2Int.zero, 0);

		objectGridPlacement.PlaceObjectOnGridWithRotation(couchs, new Vector2Int(3,8), Vector2Int.zero, 0);
		objectGridPlacement.PlaceObjectOnGridWithRotation(couchs1, new Vector2Int(1,5), Vector2Int.zero, 270);
		
		objectGridPlacement.PlaceObjectOnGridWithRotation(couchl1, new Vector2Int(1,8), Vector2Int.zero, 0);
	}
}
