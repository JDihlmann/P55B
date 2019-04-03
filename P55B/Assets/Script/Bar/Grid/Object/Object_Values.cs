using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Values : MonoBehaviour {

	// Object placed at position
	public Vector2Int placedPosition = new Vector2Int(-1,-1); 

	// Space occupied by object
	// Center is always on position (0,0)
	public Vector2Int[] occupiedSpace = new Vector2Int[]{new Vector2Int(0,0)}; 

	// ID for Prefab
	public int ID; 

	[HideInInspector]
	public Vector2Int[] previousOccupiedSpace; 

	void Start() {
		previousOccupiedSpace = (Vector2Int[])occupiedSpace.Clone();
	}
}
