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

	// Default prefabs
	public GameObject defaultObject_sc;
	public GameObject defaultObject_sp;
	public GameObject defaultObject_sl;
	public GameObject defaultObject_se;
	public GameObject defaultObject_bc;

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

		GameObject defaultObjectSC = Instantiate(defaultObject_sc, Vector3.zero, Quaternion.identity);
		GameObject defaultObjectSP = Instantiate(defaultObject_sp, Vector3.zero, Quaternion.identity);
		GameObject defaultObjectSE = Instantiate(defaultObject_se, Vector3.zero, Quaternion.identity);
		GameObject defaultObjectSL = Instantiate(defaultObject_sl, Vector3.zero, Quaternion.identity);
		GameObject defaultObjectBC = Instantiate(defaultObject_bc, Vector3.zero, Quaternion.identity);

		defaultObjectSC.transform.parent = transform;
		defaultObjectSP.transform.parent = transform; 
		defaultObjectSE.transform.parent = transform; 
		defaultObjectSL.transform.parent = transform; 
		defaultObjectBC.transform.parent = transform;

		objectGridPlacement.PlaceObjectOnGrid(defaultObjectSC, new Vector2Int(0,0), Vector2Int.zero);
		objectGridPlacement.PlaceObjectOnGrid(defaultObjectSP, new Vector2Int(4,4), Vector2Int.zero);
		objectGridPlacement.PlaceObjectOnGrid(defaultObjectSE, new Vector2Int(6,3), Vector2Int.zero);
		objectGridPlacement.PlaceObjectOnGrid(defaultObjectSL, new Vector2Int(0,8), Vector2Int.zero);
		objectGridPlacement.PlaceObjectOnGrid(defaultObjectBC, new Vector2Int(8,8), Vector2Int.zero);
	}

	public void SaveObjects () {
		// TODO: Save Objects to JSON 
	}
}
