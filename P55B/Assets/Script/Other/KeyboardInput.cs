using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {

	// Reference to object grid
	public GameObject objectGrid;

	// Object grid placement component
	private ObjectGrid_Placement objectGridPlacement; 


	void Start() {
		objectGridPlacement = objectGrid.GetComponent<ObjectGrid_Placement>(); 
	}	

	// Keyboard Input for Debuging
	void Update () {

		if (Input.GetKeyUp("h")) {
            Debug.Log("Place");
			objectGridPlacement.UIPlaceObjectOnGrid();
        }

		if (Input.GetKeyUp("j")) {
            Debug.Log("Not Place");
			objectGridPlacement.UISetObjectOnGridBack();
        }

		if (Input.GetKeyUp("k")) {
            Debug.Log("Rotate");
			objectGridPlacement.UIRotateObjectOnGrid();
        }

		if (Input.GetKeyUp("l")) {
            Debug.Log("Delete");
			objectGridPlacement.UIRemoveObjectOnGrid();
        }
	}
}
