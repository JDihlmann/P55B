using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrid_MarkTile : MonoBehaviour {

	private List<GameObject> previousMarkTiles;

	void Start() {
		previousMarkTiles = new List<GameObject>(); 
	}

	public void MarkTiles(Vector2Int tilePos, Vector2Int colliderPos, Vector2Int[] occupiedSpace) {
		UnmarkTiles();

		FloorGrid_Instantiate floorGridInstantiate = GetComponent<FloorGrid_Instantiate>();
		GameObject[,] floorGrid = floorGridInstantiate.floorGrid; 
		
		foreach (Vector2Int pos in occupiedSpace) {
			Vector2Int realPos = (pos - colliderPos) + tilePos;
			bool isInXBounds = realPos.x >= 0 && realPos.x < floorGrid.GetLength(0);
			bool isInYBounds = realPos.y >= 0 && realPos.y < floorGrid.GetLength(1); 

			if (isInXBounds && isInYBounds) {	
				GameObject tile = floorGrid[realPos.x,realPos.y];
				Tile_Selection tileSelection = tile.GetComponent<Tile_Selection>();
				tileSelection.HighlightTileMaterial(true); 
				previousMarkTiles.Add(tile);
			}
		}

	}

	public void UnmarkTiles() {
		// Reset color to default
		foreach (GameObject tile in previousMarkTiles) {
			Tile_Selection tileSelection = tile.GetComponent<Tile_Selection>();
			tileSelection.HighlightTileMaterial(false); 
		}

		// Reset list 
		previousMarkTiles = new List<GameObject>(); 
	}

}
