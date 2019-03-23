using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotation_Old : MonoBehaviour {

	// Center point
	public Vector3 center; 
	private Vector3 centerPointScreen; 

	// Rotation speed
	public float speed = 2;

	// LayerMask raycast
	private LayerMask_Raycast layerMaskRaycast;

	// Initial click bool
	private bool initalClick = true;
	private bool initialClickOutsidePlaceable = false;

	// Initial position
	private Vector3 initialClickPosition;
	private Vector3 initialCameraPosition;

	// Quadrant
	private enum Quadrant {TL, TR, BL, BR};
	private Quadrant quadrant;

	// Mouse rotation
	private bool isInitialMouseRotation; 
	private bool isMouseRotationCounterClockwise; 

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

		// Set initial center point
		centerPointScreen = Camera.main.WorldToScreenPoint(center);
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
					initialClickPosition = Input.mousePosition; 
					previousMousePosition = initialClickPosition; 

					// Mouse roation 
					MousePositionQuadrant(true); 
					isInitialMouseRotation = true; 
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

	void MousePositionQuadrant(bool initial) {
		// Mouse position 
		Vector3 mousePosition = Input.mousePosition;
		bool mouseIsInLeftXHalf = mousePosition.x < centerPointScreen.x;
		bool mouseIsInBottomYHalf = mousePosition.y < centerPointScreen.y;

		// Set current quadrant 
		if (mouseIsInLeftXHalf) {
			if (mouseIsInBottomYHalf) {
				quadrant = Quadrant.BL; 
			} else {
				quadrant = Quadrant.TL; 
			}
		} else {
			if (mouseIsInBottomYHalf) {
				quadrant = Quadrant.BR; 
			} else {
				quadrant = Quadrant.TR; 		
			}
		}

	}

	// TODO: Fix fast movement arround X-Axis 
	void RotateAroundCenter() {
		// Mouse position quadrant
		MousePositionQuadrant(false); 

		// Mouse direction vector
		Vector3 mousePosition = Input.mousePosition;
		Vector3 mouseDirection = mousePosition - previousMousePosition;

		// Mouse direction bools
		bool mouseXDirectionNegative = 0 > mouseDirection.x;
		bool mouseXDirectionPositive = 0 < mouseDirection.x;
		bool mouseYDirectionNegative = 0 >= mouseDirection.y;
		bool mouseYDirectionPositive = 0 <= mouseDirection.y;

		// Rotation mouse speed
		float xAxisInput = Input.GetAxis("Mouse X");
		float yAxisInput = Input.GetAxis("Mouse Y"); 
		float rotationInput = Mathf.Sqrt(Mathf.Pow(xAxisInput, 2) +  Mathf.Pow(yAxisInput , 2));
		if (rotationInput == 0) return; 

		// Initial mouse rotation
		if (isInitialMouseRotation) {
			if (quadrant == Quadrant.TL || quadrant == Quadrant.TR ) {
				if (mouseXDirectionPositive) {
					isMouseRotationCounterClockwise = false;
					isInitialMouseRotation = false; 
				} else if (mouseXDirectionNegative) {
					isMouseRotationCounterClockwise = true;
					isInitialMouseRotation = false; 
				}
			} else {
				if (mouseXDirectionPositive) {
					isMouseRotationCounterClockwise = true;
					isInitialMouseRotation = false; 
				} else if (mouseXDirectionNegative) {
					isMouseRotationCounterClockwise = false;
					isInitialMouseRotation = false; 
				}
			}
		}

		// Mouse rotation direction
		if (!isInitialMouseRotation) {
			switch (quadrant) {
				case Quadrant.BR:
				if (isMouseRotationCounterClockwise) {
					if(mouseXDirectionNegative && mouseYDirectionNegative) {
						isMouseRotationCounterClockwise = false; 
						rotationInput *= -1; 
					}
				} else {
					if(mouseXDirectionPositive && mouseYDirectionPositive) {
						isMouseRotationCounterClockwise = true; 
					} else {
						rotationInput *= -1; 
					}
				}
				break;
				case Quadrant.BL:
				if (isMouseRotationCounterClockwise) {
					if(mouseXDirectionNegative && mouseYDirectionPositive) {
						isMouseRotationCounterClockwise = false; 
						rotationInput *= -1; 
					}
				} else {
					if(mouseXDirectionPositive && mouseYDirectionNegative) {
						isMouseRotationCounterClockwise = true; 
					} else {
						rotationInput *= -1; 
					}
				}
				break;
				case Quadrant.TR:
				if (isMouseRotationCounterClockwise) {
					if(mouseXDirectionPositive && mouseYDirectionNegative) {
						isMouseRotationCounterClockwise = false; 
						rotationInput *= -1; 
					} 
				} else {
					if(mouseXDirectionNegative && mouseYDirectionPositive) {
						isMouseRotationCounterClockwise = true; 
					} else {
						rotationInput *= -1; 
					}
				}
				break;
				case Quadrant.TL:
				if (isMouseRotationCounterClockwise) {
					if(mouseXDirectionPositive && mouseYDirectionPositive) {
						isMouseRotationCounterClockwise = false; 
						rotationInput *= -1; 
					} 
				} else {
					if(mouseXDirectionNegative && mouseYDirectionNegative) {
						isMouseRotationCounterClockwise = true; 
					} else {
						rotationInput *= -1; 
					}
				}
				break;
			}

			// Rotate around center
			transform.RotateAround(center, Vector3.up, rotationInput * speed);
			
		}

		// Set previous nouse position
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

			transform.RotateAround(center, Vector3.up, rotationSpeed);
			previousRotationDistance = rotationDistance; 
		} else {
			// Snap
			snapped = true; 
			transform.position = snapCameraPosition; 
			transform.eulerAngles = snapCameraRotation; 
		}
	}
}