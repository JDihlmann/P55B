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
	public GameObject s1_Tabel;
	public GameObject s1_Chair;
	public GameObject s1_CouchS;
	public GameObject s1_CouchL;

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

		EmptyObjectGrid();
	}

	void Update() {
		// Not in Start because we have to wait until
		// ObjectGrid_Placement & ObjectGrid_Operations
		// Are loaded (No SEO does not help here)
		if(runOnce) {
			LoadObjects();
			runOnce = false; 
		}
	}


	private void EmptyObjectGrid() {
		objectGrid = new GameObject[width,length];
		integerObjectGrid = new Vector2Int[width,length];

		// Fill all arrays with [-1,-1]
		for (int i = 0; i < integerObjectGrid.GetLength(0); i++) {
			for (int j = 0; j < integerObjectGrid.GetLength(1); j++) {
				integerObjectGrid[i,j] = emptyIntObjectElement;
			}
		}
	}
	
	public void LoadObjects () {
		// TODO: Load Objects from JSON
		objectGridPlacement = GetComponent<ObjectGrid_Placement>();

		// GameObject table = Instantiate(s1_Tabel, Vector3.zero, Quaternion.identity);
		// table.transform.parent = transform;
		// objectGridPlacement.PlaceObjectOnGrid(table, new Vector2Int(8,8), Vector2Int.zero);
	}

	public void SaveObjects () {
		// TODO: Save Objects to JSON 
	}
}
