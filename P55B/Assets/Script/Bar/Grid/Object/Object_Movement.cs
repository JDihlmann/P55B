using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Movement : MonoBehaviour {

	// Lift constant
	public static float lift = 0.5f;

	// Moveable Boundry 
	public static int[] boundries = new int[] { 10, -10, 10, -10 }; 


	public void MoveOnMoveableLayerWithMouseAndOffset(Vector2Int colliderOffset) {
		int layer_moveable = LayerMask.GetMask("Moveable");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Raycast moveable region
		if(Physics.Raycast(ray, out hit, 100, layer_moveable)) {

			// Scale values
			float xScale = transform.localScale.x / 2;
			float zScale = transform.localScale.z / 2;
			float yScale = transform.localScale.y / 2;

			// Move point
			Vector3 movePoint = hit.point; 
			movePoint.x -= colliderOffset.x; 
			movePoint.z -= colliderOffset.y; 

			// Keep object in boundries
			movePoint.x = movePoint.x + xScale > boundries[0]? boundries[0] - xScale: movePoint.x;
			movePoint.x = movePoint.x - xScale < boundries[1]? boundries[1] + xScale: movePoint.x;
			movePoint.z = movePoint.z + zScale > boundries[2]? boundries[2] - zScale: movePoint.z;
			movePoint.z = movePoint.z - zScale < boundries[3]? boundries[3] + zScale: movePoint.z;
			
			// Floating above grid
			movePoint.y += yScale + lift;

			// Assign position
			transform.position = movePoint; 
		}
	}

	public void MoveToGridTileWithOffset(GameObject tile, Vector2Int colliderOffset) {
		Vector3 gloabalPosition = tile.transform.position;

		// Height position without lift and object height
		gloabalPosition.y += tile.transform.localScale.y / 2;
		gloabalPosition.y += transform.localScale.y / 2;
		
		// Width and length with collider offset
		gloabalPosition.x -= colliderOffset.x;
		gloabalPosition.z -= colliderOffset.y;

		// Set possition
		transform.position = gloabalPosition;
	}

	public void MoveToAboveGridTileWithOffset(GameObject tile, Vector2Int colliderOffset) {
		Vector3 gloabalPosition = tile.transform.position;

		// Height position with lift and object height
		gloabalPosition.y += tile.transform.localScale.y / 2;
		gloabalPosition.y += transform.localScale.y / 2;
		gloabalPosition.y += lift;
		
		// Width and length with collider offset
		gloabalPosition.x -= colliderOffset.x;
		gloabalPosition.z -= colliderOffset.y;

		// Set possition
		transform.position = gloabalPosition;
	}
}