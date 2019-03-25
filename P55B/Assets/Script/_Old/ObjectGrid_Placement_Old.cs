using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid_Placement_Old : MonoBehaviour {

	// Floor Grid 
	private FloorGrid_Instantiate floorGridInstantiate;

	// Hit object
	private GameObject hitObject;
	private GameObject hitObjectParent;
	private Object_Values hitObjectParentValues;
	private Object_Movement hitObjectParentMovement;
	private Collider_Position hitObjectColliderPosition;
	

	// Hit tile
	private GameObject hitTile;
	private GameObject hitTileFloorGrid; 
	private Tile_Position hitTilePosition; 
	private FloorGrid_MarkTile hitTileFloorGridMarkTile; 
	private bool hitTileChanged = false; 

	// Grid operation
	private ObjectGrid_Operations objectGridOperations; 

	// Initial click bool
	private bool initalClick = true;
	private bool initalClickHitObject = false; 

	void Start () {
		objectGridOperations = GetComponent<ObjectGrid_Operations>(); 

		// Floor Grid
		Grid_Instantiate gridInstantiate = transform.parent.GetComponent<Grid_Instantiate>();
		floorGridInstantiate = gridInstantiate.floorGrid.GetComponent<FloorGrid_Instantiate>();
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			MouseDown(); 
		} else if (!initalClick) {
			MouseUp(); 
			ResetAllValues();
		}
	}

	private void MouseDown() {
		if (initalClick) {
			initalClick = false;
			initalClickHitObject = didRaycastHitObject(); 

			if (initalClickHitObject) {
				GetObjectComponents();
			}
		}

		if (initalClickHitObject) {
			// Move with mouse 
			Vector2Int colliderPosition = hitObjectColliderPosition.position; 
			hitObjectParentMovement.MoveOnMoveableLayerWithMouseAndOffset(colliderPosition);
			
			// Mark tiles hovered over
			if (didRaycastHitTile() && hitTileChanged) {
				GetTileComponents();
				
				// Object placeable at position
				bool placeable = objectGridOperations.IsObjectPlaceableAtPosition(hitObjectParent, hitTilePosition.position);

				// Mark tiles
				hitTileFloorGridMarkTile.MarkTiles(hitTilePosition.position, colliderPosition, hitObjectParentValues.occupiedSpace, placeable); 
			} else if (hitTileChanged) {
				hitTileFloorGridMarkTile.UnmarkTiles();
			}
			
		}
	}

	private void MouseUp() {
		if (initalClickHitObject) {
			GameObject obj = hitObjectParent; 

			if(hitTile == null) {
				Destroy(obj);
				return; 
			}

			// Grid Position
			Vector2Int toPos = hitTilePosition.position;
			Vector2Int offset = hitObjectColliderPosition.position;
			Vector2Int fromPos = hitObjectParentValues.placedPosition;
			
			if(objectGridOperations.IsPositionEmpty(fromPos)) {
				if(!PlaceObjectOnGrid(hitObjectParent, toPos, offset))
					RemoveObjectOnGrid(obj, fromPos); 
			} else {
				MoveObjectOnGrid(obj, fromPos, toPos, offset); 
			}

			// Unmark Tiles
			hitTileFloorGridMarkTile.UnmarkTiles();
		}
	}

	public void RemoveObjectOnGrid(GameObject obj, Vector2Int pos) {
		Destroy(obj);
		objectGridOperations.RemoveObject(pos); 
	}

	public bool PlaceObjectOnGrid(GameObject obj, Vector2Int pos, Vector2Int offset) {
		Vector2Int posWithOffset = pos - offset; 
		Object_Values objValues = obj.GetComponent<Object_Values>(); 
		Object_Movement objMovement = obj.GetComponent<Object_Movement>();

		if(objectGridOperations.AddObject(obj, posWithOffset)) {
			GameObject tile = floorGridInstantiate.floorGrid[posWithOffset.x, posWithOffset.y]; 
			objMovement.MoveToGridTileWithOffset(tile, new Vector2Int(0,0));
			objValues.placedPosition =  posWithOffset;
			return true;
		}

		return false; 
	}

	public bool MoveObjectOnGrid(GameObject obj, Vector2Int fromPos, Vector2Int toPos, Vector2Int offset) {
		Vector2Int posWithOffset = toPos - offset;
		Object_Values objValues = obj.GetComponent<Object_Values>(); 
		Object_Movement objMovement = obj.GetComponent<Object_Movement>();

		if(objectGridOperations.MoveObject(fromPos, posWithOffset)) {
			GameObject tile = floorGridInstantiate.floorGrid[posWithOffset.x, posWithOffset.y]; 
			objMovement.MoveToGridTileWithOffset(tile, new Vector2Int(0,0));
			objValues.placedPosition =  posWithOffset;
			return true;
		} 

		PlaceObjectOnGrid(obj, fromPos, new Vector2Int(0,0)); 

		return false; 
	}

	private void GetObjectComponents() {
		hitObjectParent = hitObject.transform.parent.gameObject; 
		hitObjectParentValues = hitObjectParent.GetComponent<Object_Values>();
		hitObjectParentMovement = hitObjectParent.GetComponent<Object_Movement>(); 
		hitObjectColliderPosition = hitObject.GetComponent<Collider_Position>(); 
	}

	private void GetTileComponents() {
		hitTileFloorGrid = hitTile.transform.parent.gameObject; 
		hitTileFloorGridMarkTile = hitTileFloorGrid.GetComponent<FloorGrid_MarkTile>();
		hitTilePosition = hitTile.GetComponent<Tile_Position>();
	}

	private bool didRaycastHitObject() {
		int layer_object = LayerMask.GetMask("Object");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Raycast hit object
		if(Physics.Raycast(ray, out hit, 100, layer_object)) {
			hitObject = hit.collider.gameObject;
		} else {
			hitObject = null; 
		}

		return hitObject != null; 
	}

	private bool didRaycastHitTile() {
		int layer_placeable= LayerMask.GetMask("Placeable");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Raycast tile hit
		if(Physics.Raycast(ray, out hit, 100, layer_placeable)) {
			hitTileChanged = hitTile != hit.collider.gameObject; 
			hitTile = hit.collider.gameObject;
		} else {
			hitTileChanged = hitTile != null;
			hitTile = null;
		}

		return hitTile != null; 
	} 

	private void ResetAllValues() {
		hitTile = null;
		hitObject = null; 
		initalClick = true;
		hitTileChanged = false; 
		initalClickHitObject = false; 
	}
}
