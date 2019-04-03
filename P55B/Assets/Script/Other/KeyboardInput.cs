using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {

	// Reference to object grid
	public GameObject objectGrid;

	// Object grid components
	// Object grid placement component
	private ObjectGrid_Instantiate objectGridInstantiate; 
	private ObjectGrid_Placement objectGridPlacement; 

	// Reference to state change
	public GameObject gameStateChanger;

	// Object grid placement component
	private StateChange stateChange; 


	void Start() {
		objectGridInstantiate = objectGrid.GetComponent<ObjectGrid_Instantiate>();
		objectGridPlacement = objectGrid.GetComponent<ObjectGrid_Placement>();
		stateChange = gameStateChanger.GetComponent<StateChange>(); 
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

		if (Input.GetKeyUp("v")) {
            Debug.Log("Activate UI State");
			stateChange.ActivateUIState();
        }

		if (Input.GetKeyUp("b")) {
            Debug.Log("Activate Bar  State");
			stateChange.ActivateBarState();
        }

		if (Input.GetKeyUp("n")) {
            Debug.Log("Activate Build State");
			stateChange.ActivateBuildState();
        }

		if (Input.GetKeyUp("m")) {
            Debug.Log("Activate Space State");
			stateChange.ActivateSpaceState();
        }


		if (Input.GetKeyDown("space")) {
			objectGridInstantiate.SpawnNewObjectWithID(1);
		}	
	}
}
