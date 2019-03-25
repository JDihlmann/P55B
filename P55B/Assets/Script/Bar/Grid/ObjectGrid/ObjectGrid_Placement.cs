using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrid_Placement : MonoBehaviour {

	// Floor Grid 
	private FloorGrid_Instantiate floorGridInstantiate;

	// Hit object
	private GameObject hitObject;
	private GameObject hitObjectParent;
	private Object_Values hitObjectParentValues;
	private Object_Movement hitObjectParentMovement;
	private Collider_Position hitObjectColliderPosition;
	private bool hitObjectParentPlaced = false; 

	// Hit object rotation
	private Vector3 previousHitObjectParentRotation;
	
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


	# region Mouse  

	private void MouseDown() {
		if (initalClick) {
			initalClick = false;
			initalClickHitObject = DidRaycastHitObject(); 

			if (initalClickHitObject) {
				bool IsDiffrentObject = (hitObjectParent != hitObject.transform.parent.gameObject);

				if(!hitObjectParentPlaced && hitObjectParent != null){
					if (IsDiffrentObject) {
						// Grid position
						Vector2Int offset = hitObjectColliderPosition.position;
						Vector2Int fromPos = hitObjectParentValues.placedPosition;
						Vector2Int toPos = hitTilePosition.position;

						TryPlacingObjectOnGrid(hitObjectParent, fromPos, toPos, offset); 
					}
				} 

				GetHitObjectComponents();

				if (IsDiffrentObject) {
					previousHitObjectParentRotation = hitObjectParent.transform.eulerAngles;
					hitObjectParentValues.previousOccupiedSpace = (Vector2Int[])hitObjectParentValues.occupiedSpace.Clone();
				}

				hitObjectParentPlaced = false; 
			}
		}

		if (initalClickHitObject) {
			// Move with mouse 
			Vector2Int colliderPosition = hitObjectColliderPosition.position; 
			hitObjectParentMovement.MoveOnMoveableLayerWithMouseAndOffset(colliderPosition);
			
			// Mark tiles hovered over
			if (DidRaycastHitTile() && hitTileChanged) {
				GetTileComponents();
				
				// Object placeable at position
				Vector2Int posWithOffset = hitTilePosition.position - colliderPosition;  
				bool placeable = objectGridOperations.IsObjectPlaceableAtPosition(hitObjectParent, posWithOffset);

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

			// Grid object position
			Vector2Int offset = hitObjectColliderPosition.position;
			Vector2Int fromPos = hitObjectParentValues.placedPosition;

			// Object not above grid 
			if(hitTile == null) {
				RemoveObjectOnGrid(obj, fromPos);
				return; 
			}

			// Grid tile position
			Vector2Int toPos = hitTilePosition.position;

			// Place object above grid
			PlaceObjectAboveGrid(obj, offset);

			// Toogle UI buttons
			Vector2Int posWithOffset = hitTilePosition.position - hitObjectColliderPosition.position;  
			bool placeable = objectGridOperations.IsObjectPlaceableAtPosition(hitObjectParent, posWithOffset);

			if (hitTile == null) {
				UIHideAllButtons();

				if(placeable) {
					UIShowAllButtons();
				} else {
					UIShowAllButtonsWithoutPlaceableButton();
				}
			}
			
		}
	}

	# endregion

	#region UI  

	public void UIRemoveObjectOnGrid() {
		// Grid position
		Vector2Int fromPos = hitObjectParentValues.placedPosition;

		RemoveObjectOnGrid(hitObjectParent, fromPos);

		// Unmark Tiles
		hitTileFloorGridMarkTile.UnmarkTiles();

		hitObjectParentPlaced = true; 
	}

	public void UISetObjectOnGridBack() {
		// Grid position
		Vector2Int fromPos = hitObjectParentValues.placedPosition;

		if(!objectGridOperations.IsPositionEmpty(fromPos)) {
			// Undo Rotation
			hitObjectParent.transform.eulerAngles = previousHitObjectParentRotation;
			hitObjectParentValues.occupiedSpace = (Vector2Int[])hitObjectParentValues.previousOccupiedSpace.Clone();
		
			PlaceObjectOnGrid(hitObjectParent, fromPos, new Vector2Int(0,0)); 
		}

		// Unmark Tiles
		hitTileFloorGridMarkTile.UnmarkTiles();

		hitObjectParentPlaced = true; 
	}

	public void UIPlaceObjectOnGrid() {
		// Grid position
		Vector2Int offset = hitObjectColliderPosition.position;
		Vector2Int fromPos = hitObjectParentValues.placedPosition;
		Vector2Int toPos = hitTilePosition.position;

		if(objectGridOperations.IsPositionEmpty(fromPos)) {
			PlaceObjectOnGrid(hitObjectParent, toPos, offset);
		} else {
			MoveObjectOnGrid(hitObjectParent, fromPos, toPos, offset); 
		}

		// Unmark Tiles
		hitTileFloorGridMarkTile.UnmarkTiles();

		hitObjectParentPlaced = true; 
	}

	public void UIRotateObjectOnGrid() {
		// Rotate Object
		Vector3 ninetyDegreeRotation = new Vector3(0, 90, 0);
		hitObjectParent.transform.eulerAngles += ninetyDegreeRotation;

		// Rotate Occupied Space
		for (int i = 0; i < hitObjectParentValues.occupiedSpace.Length;  i++) {
			Vector2Int space = hitObjectParentValues.occupiedSpace[i];
			hitObjectParentValues.occupiedSpace[i] = new Vector2Int(space.y, -space.x);
		}

		// Object placeable at position
		Vector2Int colliderPosition = hitObjectColliderPosition.position; 
		Vector2Int posWithOffset = hitTilePosition.position - colliderPosition;  
		bool placeable = objectGridOperations.IsObjectPlaceableAtPosition(hitObjectParent, posWithOffset);

		// Mark tiles
		hitTileFloorGridMarkTile.MarkTiles(hitTilePosition.position, colliderPosition, hitObjectParentValues.occupiedSpace, placeable); 


	}

	private void UIShowAllButtons() {

	}

	private void UIHideAllButtons() {
		
	}

	private void UIShowAllButtonsWithoutPlaceableButton() {

	}

	# endregion

	# region Placement 

	public void RemoveObjectOnGrid(GameObject obj, Vector2Int pos) {
		Destroy(obj);
		objectGridOperations.RemoveObject(pos); 
	}

	public void TryPlacingObjectOnGrid(GameObject obj, Vector2Int fromPos, Vector2Int toPos, Vector2Int offset) {
		// Try placing combination
		if(objectGridOperations.IsPositionEmpty(fromPos)) {
			if(!PlaceObjectOnGrid(hitObjectParent, toPos, offset))
				RemoveObjectOnGrid(hitObjectParent, fromPos); 
		} else {
			MoveObjectOnGrid(hitObjectParent, fromPos, toPos, offset); 
		}

		// Unmark Tiles
		hitTileFloorGridMarkTile.UnmarkTiles();

		hitObjectParentPlaced = true; 
	}

	public void PlaceObjectAboveGrid(GameObject obj,  Vector2Int offset) {
		Object_Movement objMovement = obj.GetComponent<Object_Movement>();

		objMovement.MoveToAboveGridTileWithOffset(hitTile, offset);
	}

	public bool PlaceObjectOnGrid(GameObject obj, Vector2Int pos, Vector2Int offset) {
		Vector2Int posWithOffset = pos - offset; 
		Object_Values objValues = obj.GetComponent<Object_Values>(); 
		Object_Movement objMovement = obj.GetComponent<Object_Movement>();

		if(objectGridOperations.AddObject(obj, posWithOffset)) {
			GameObject tile = floorGridInstantiate.floorGrid[posWithOffset.x, posWithOffset.y]; 
			objMovement.MoveToGridTileWithOffset(tile, new Vector2Int(0,0));
			objValues.placedPosition = posWithOffset;
			return true;
		}

		return false; 
	}

	public bool PlaceObjectOnGridWithRotation(GameObject obj, Vector2Int pos, Vector2Int offset, float degree) {
		Object_Values objValues = obj.GetComponent<Object_Values>(); 

		// Rotate Object
		Vector3 rotation = Vector3.zero;
		if (degree == 90) {
			rotation.y = 90;
		} else if (degree == 180) {
			rotation.y = 180;
		} else if (degree == 270) {
			rotation.y = 270;
		}
		obj.transform.eulerAngles += rotation;

		// Rotate Occupied Space
		for (int i = 0; i < objValues.occupiedSpace.Length;  i++) {
			Vector2Int space = objValues.occupiedSpace[i];
			if (degree == 90) {
				objValues.occupiedSpace[i] = new Vector2Int(space.y, -space.x);
			} else if (degree == 180) {
				objValues.occupiedSpace[i] = new Vector2Int(-space.x, -space.y);
			} else if (degree == 270) {
				objValues.occupiedSpace[i] = new Vector2Int(-space.y, space.x);
			}
		}

		return PlaceObjectOnGrid(obj, pos, offset);
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

		hitObjectParent.transform.eulerAngles = previousHitObjectParentRotation;
		hitObjectParentValues.occupiedSpace = (Vector2Int[])hitObjectParentValues.previousOccupiedSpace.Clone();

		PlaceObjectOnGrid(obj, fromPos, new Vector2Int(0,0)); 

		return false; 
	}

	# endregion

	# region Component 

	private void GetHitObjectComponents() {
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

	# endregion

	#region Raycast

	private bool DidRaycastHitObject() {
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

	private bool DidRaycastHitTile() {
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

	# endregion

	# region Reset

	private void ResetAllValues() {
		hitTile = null;
		hitObject = null; 
		initalClick = true;
		hitTileChanged = false; 
		initalClickHitObject = false; 
	}

	# endregion
}
