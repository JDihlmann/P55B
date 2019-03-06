using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FloorGrid_Instantiate : MonoBehaviour {

	// Size 
	private int width; 
	private int length; 

	// GameObjects
	public GameObject tile;
	public GameObject[,] floorGrid;

	// Generate grid in edit mode
	public void GenerateFloorGrid(int gridWidth, int gridLength) {
		DeleteFloorGrid();

		// Size 
		width = gridWidth;
		length = gridLength; 

		// Grid Array
		floorGrid = new GameObject[width, length];

		// Tile local scale
		float tileWidthX = tile.transform.localScale.x;
		float tileWidthZ = tile.transform.localScale.x;
		float tileHeight = tile.transform.localScale.y;

		// Tile local position
		float tilePosY = tile.transform.localPosition.y;

		// Starting position
		float startPosY = (tileHeight/2) + tilePosY;
		float startPosX = -(width/2) + (tileWidthX/2);
		float startPosZ = -(length/2) + (tileWidthZ/2);


		for (int x = 0; x < floorGrid.GetLength(0); x++) {
			for (int z = 0; z < floorGrid.GetLength(1); z++) {
				float xPos = startPosX + x * tileWidthX;
				float zPos = startPosZ + z * tileWidthZ;

				// Add tile to floor grid
				floorGrid[x,z] = Instantiate(tile, new Vector3(xPos, startPosY, zPos), Quaternion.identity);
				floorGrid[x,z].transform.parent = transform;

				// Set tile grid position on tile
				floorGrid[x,z].GetComponent<Tile_Position>().position = new Vector2Int(x,z);
			}
		}
		// Information Log 
		Debug.Log("Generated Grid");
	}

	// Delete grid in edit mode
	public void DeleteFloorGrid() {
		Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>(true); 
		foreach (Transform child in allChildren) {
			if (child.gameObject.name == tile.name + "(Clone)")
        		DestroyImmediate(child.gameObject);
		}
	}
}

