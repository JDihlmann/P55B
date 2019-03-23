using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid_Operations : MonoBehaviour {

	// Size
	private int width;
	private int length; 

	// GameObjects
	private GameObject[,] objectGrid;
	private Vector2Int[,] integerObjectGrid; 

	// Empty integer object element
	private Vector2Int emptyIntObjectElement;

	// Blocked Space
	private Vector2Int[] blockedSpace; 

	void Start () {
		// Object Grid instantiatet grids
		ObjectGrid_Instantiate objectGridInstantiate = GetComponent<ObjectGrid_Instantiate>();
		emptyIntObjectElement = objectGridInstantiate.emptyIntObjectElement; 
		integerObjectGrid = objectGridInstantiate.integerObjectGrid; 
		objectGrid = objectGridInstantiate.objectGrid; 

		// Size
		width = integerObjectGrid.GetLength(0);
		length = integerObjectGrid.GetLength(1);

		// Blocked Space By Center Bar 
		// TODO: Fix if grid changes size
		blockedSpace = new Vector2Int[]{new Vector2Int(4,4), new Vector2Int(4,5), new Vector2Int(5,4), new Vector2Int(5,5)};
	}

	// Get gameobject at position 
	public GameObject GetObject(Vector2Int pos) {

		if (IsPositionEmpty(pos))
			return null;

		Vector2Int objectPos = integerObjectGrid[pos.x, pos.y];
		return objectGrid[objectPos.x, objectPos.y];
	}

	// Add gameobject at position 
	public bool AddObject(GameObject obj,Vector2Int pos) {

		Object_Values objectValues = GetObjectValues(obj);

		if (!IsObjectPlaceableAtPosition(obj, pos))
			return false;

		// Add object to interger grid
		foreach (Vector2Int deltaPos in objectValues.occupiedSpace) {
			Vector2Int relativePos = deltaPos + pos;
			integerObjectGrid[relativePos.x, relativePos.y] = pos; 
		}

		//Add object to gameobject grid
		objectGrid[pos.x, pos.y] = obj; 

		return true;  
	}

	// Add gameobject at position 
	public bool IsObjectPlaceableAtPosition(GameObject obj, Vector2Int pos) {

		Object_Values objectValues = GetObjectValues(obj);

		foreach (Vector2Int deltaPos in objectValues.occupiedSpace) {
			Vector2Int relativePos = deltaPos + pos;

			// Out of bounds x position
			if (relativePos.x < 0 || relativePos.x >= width)
				return false;

			// Out of boundy y position
			if(relativePos.y < 0 || relativePos.y >= length)
				return false; 

			// Position empty
			Vector2Int objectGridPosition = integerObjectGrid[relativePos.x, relativePos.y];
			if (!IsPositionEmpty(objectGridPosition)) {
				if (obj != objectGrid[objectGridPosition.x, objectGridPosition.y]) {
					return false; 
				}
			}

			if (IsPositionBlocked(relativePos))
				return false;
		} 

		return true; 
	}
	
	// Remove gameobject at position 
	public void RemoveObject(Vector2Int pos) {

		if(IsPositionEmpty(pos))
			return;

		GameObject obj = GetObject(pos);
		Object_Values objectValues = GetObjectValues(obj);

		// Remove from index grid
		foreach (Vector2Int deltaPos in objectValues.occupiedSpace) {
			Vector2Int relativePos = deltaPos + pos;
			integerObjectGrid[relativePos.x, relativePos.y] = emptyIntObjectElement; 
		}

		// Remove object from gameobject grid
		objectGrid[pos.x, pos.y] = null; 
	}

	// Move gameobject from position to position 
	public bool MoveObject(Vector2Int fromPos, Vector2Int toPos) {
		GameObject obj = GetObject(fromPos);
		RemoveObject(fromPos);
		return AddObject(obj, toPos);
	}

	// Position empty
	public bool IsPositionEmpty(Vector2Int pos) {
		return pos == emptyIntObjectElement;
	}

	// Position blocked
	public bool IsPositionBlocked(Vector2Int pos) {
		foreach  (Vector2Int blockedPos in blockedSpace) {
			if (pos == blockedPos)
				return true;
		}

		return false; 
	}

	private Object_Values GetObjectValues(GameObject obj) {
		return obj.GetComponent<Object_Values>(); 
	}

	// Debug Grid
	// TODO: Delete
	public void DebugGrid() {
		Grid_Instantiate test = transform.parent.GetComponent<Grid_Instantiate>();
		FloorGrid_Instantiate test1 = test.floorGrid.GetComponent<FloorGrid_Instantiate>();

		// Fill all arrays with [-1,-1]
		for (int i = 0; i < integerObjectGrid.GetLength(0); i++) {
			for (int j = 0; j < integerObjectGrid.GetLength(1); j++) {
				if (!IsPositionEmpty(integerObjectGrid[i,j])) {
					Tile_Selection tileSelection = test1.floorGrid[i,j].GetComponent<Tile_Selection>();
					tileSelection.HighlightTileMaterial(true, true);  
				} else {
					Tile_Selection tileSelection = test1.floorGrid[i,j].GetComponent<Tile_Selection>();
					tileSelection.HighlightTileMaterial(false, false); 
				}
			}
		}
	}
}
