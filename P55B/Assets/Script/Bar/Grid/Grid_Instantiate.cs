using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Instantiate : MonoBehaviour {

	// Integer Size
	public int width = 10;
    public int length = 10;

	// Gameobject
	public GameObject floorGrid;
	public GameObject objectGrid;

	void Start() {
		// Generate floor grid
		floorGrid.GetComponent<FloorGrid_Instantiate>().GenerateFloorGrid(width, length);

		// Generate object grid
		objectGrid.GetComponent<ObjectGrid_Instantiate>().GenerateObjectGrid(width, length); 
	}

	public void GenerateGrid() {
		// Generate floor grid
		floorGrid.GetComponent<FloorGrid_Instantiate>().GenerateFloorGrid(width, length);
	}
}
