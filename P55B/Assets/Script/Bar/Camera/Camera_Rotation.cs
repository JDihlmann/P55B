using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour {

	// LayerMask raycast
	private LayerMask_Raycast layerMaskRaycast;

	// Initial click bool
	private bool initalClick = true;
	private bool initialClickOutsidePlaceable = false;

	// Initial position
	private Vector3 initialCameraPosition;

	// Previous mouse
	private Vector3 previousMousePosition;

	// Snap transform
	private Vector3 snapCameraPosition; 
	private Vector3 snapCameraRotation; 

	// Snap rotation
	private bool snapped; 
	private bool snapIsNegative; 
	private float previousRotationDistance; 


	void Start () {
		// Placement
		layerMaskRaycast = FindObjectOfType<LayerMask_Raycast>();

		// Set initial camera position
		initialCameraPosition = transform.position;

		// Set snap camera position
		snapCameraPosition = transform.position;
		snapCameraRotation = transform.eulerAngles; 
	}

	void Update() {

		// Mouse button down 
     	if (Input.GetMouseButton(0)) {
			if (initalClick) {
				snapped = true; 
				initalClick = false; 

				// Mouse in not placeable region
				if(!layerMaskRaycast.IsPlaceable() && !layerMaskRaycast.IsObject()) {
					initialClickOutsidePlaceable = true;
					previousMousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
				}
			}

			if (initialClickOutsidePlaceable) {
				// Rotate around center
        		RotateAroundCenter();
			}

		// Mouse button up
		} else if (!initalClick) {
			// Reset boolean
			initalClick = true;
			initialClickOutsidePlaceable = false; 

			// Snap to edge
			snapped = false; 
			SelectSnapEdge(); 
			previousRotationDistance = float.MaxValue; 
		} else {
			// Snap to edge over time 
			if (!snapped) SnapToEdge(); 
		}
		
	}

	void RotateAroundCenter() {
		// Mouse position
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
		
		// Screen Center
		Vector3 screenCenter = new Vector3(Screen.width /2, 0, Screen.height/2);

		// Centered mouse position vectors 
		Vector3 CenteredCurrentMousePosition = mousePosition - screenCenter;
		Vector3 CenteredPreviousMousePosition = previousMousePosition - screenCenter; 

		// Signed angle between vectors
		float angle = Vector3.SignedAngle(CenteredCurrentMousePosition, CenteredPreviousMousePosition, Vector3.up);

		// Rotate Camera
		transform.RotateAround(Vector3.zero, Vector3.up, angle);

		// Set previous mouse position
		previousMousePosition = mousePosition;	
	}

	void SelectSnapEdge() {
		// Postion and Transform
		Vector3 position = transform.position;
		
		// Snap to edge
		bool condition1 = position.x < 0 && position.z < 0;
		bool condition2 = position.x < 0 && position.z > 0;
		bool condition3 = position.x > 0 && position.z < 0;
		bool condition4 = position.x > 0 && position.z > 0;

		// Positive camera y, x and z values
		float positiveCameraY = initialCameraPosition.y;
		float positiveCameraX = Mathf.Abs(initialCameraPosition.x);
		float positiveCameraZ = Mathf.Abs(initialCameraPosition.z);

		// Set camera to edge
		if (condition1) {
			// Camera position -x, -z
			snapCameraPosition = new Vector3(-positiveCameraX, positiveCameraY, -positiveCameraZ);

			// Camera angle 45 degree
			snapCameraRotation.y =  45f;

			// Snap negative
			snapIsNegative = snapCameraPosition.x > position.x;  

		} else if (condition2) {
			// Camera position -x, +z
			snapCameraPosition = new Vector3(-positiveCameraX, positiveCameraY, positiveCameraZ);

			// Camera angle 135 degree
			snapCameraRotation.y = 135f; 

			// Snap negative
			snapIsNegative = snapCameraPosition.x < position.x;  

		} else if (condition3) {
			// Camera position +x, -z
			snapCameraPosition = new Vector3(positiveCameraX, positiveCameraY, -positiveCameraZ);

			// Camera angle -45 degree
			snapCameraRotation.y = -45f; 

			// Snap negative
			snapIsNegative = snapCameraPosition.x > position.x;  

		} else if (condition4) {
			// Camera position +x, +z
			snapCameraPosition = new Vector3(positiveCameraX, positiveCameraY, positiveCameraZ);

			// Camera angle -135 degree
			snapCameraRotation.y = -135f; 

			// Snap negative
			snapIsNegative = snapCameraPosition.x < position.x;  
		}
	}

	void SnapToEdge () {
		// Distance between camera and snap camera position
		float rotationDistance = Vector3.Distance(snapCameraPosition, transform.position); 

		if (rotationDistance < previousRotationDistance) {
			// Rotate

			float speed = 50; 
			float rotationSpeed = Time.deltaTime * speed * rotationDistance;
			rotationSpeed *= snapIsNegative ? -1 : 1; 

			transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed);
			previousRotationDistance = rotationDistance; 
		} else {
			// Snap
			snapped = true; 
			transform.position = snapCameraPosition; 
			transform.eulerAngles = snapCameraRotation; 
		}
	}
}