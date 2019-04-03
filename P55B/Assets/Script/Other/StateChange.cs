using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

	// Enum possible states
	public enum State {UI, Bar, Build, Space};

	// Current state
	public State currentState; 

	// UI GameObjects
	// TODO: Insert GameObjects 

	// Bar GameObjects
	// TODO: Insert GameObjects 

	// Build GameObjects
	public GameObject objectGrid;

	// Space GameObjects
	// TODO: Insert GameObjects 


	void Start () {
		// TODO: Choose Initial State
	}

	# region General Changes

	public void DeactivateState(State state) {
		switch (state) {
			case State.UI: 
				DeactivateUIState();
				break;
			case State.Bar: 
				DeactivateBarState();
				break;
			case State.Build: 
				DeactivateBuildState();
				break;
			case State.Space: 
				DeactivateSpaceState();
				break;
		}
	}

	# endregion

	# region UI State

	public void ActivateUIState() {
		if(currentState != State.Space) {
			DeactivateState(currentState);
			currentState = State.Space; 
		}

		// TODO: Add ui relateded scripts / objects / ...

	}

	public void DeactivateUIState() {
		// TODO: Add ui relateded scripts / objects / ...
	}

	# endregion

	# region Bar State

	public void ActivateBarState() {
		if(currentState != State.Bar) {
			DeactivateState(currentState);
			currentState = State.Bar; 
		}

		// TODO: Add bar relateded scripts / objects / ...
	}

	public void DeactivateBarState() {
		// TODO: Add bar relateded scripts / objects / ...
	}

	# endregion

	# region Build State

	public void ActivateBuildState() {
		if(currentState != State.Build) {
			DeactivateState(currentState);
			currentState = State.Build; 
		}

		ObjectGrid_Placement objectGridPlacements = objectGrid.GetComponent<ObjectGrid_Placement>();
		objectGridPlacements.enabled = true; 	
	}

	public void DeactivateBuildState() {
		// Place last object, reset all values and disable script
		ObjectGrid_Placement objectGridPlacements = objectGrid.GetComponent<ObjectGrid_Placement>();
		objectGridPlacements.TryPlacingObjectOnGrid();
		objectGridPlacements.ResetAllValues(); 
		objectGridPlacements.enabled = false; 

		// Save all objects on grid 
		ObjectGrid_Instantiate objectGridInstantiate = objectGrid.GetComponent<ObjectGrid_Instantiate>();
		objectGridInstantiate.SaveObjects();

	}

	# endregion

	# region Space State

	public void ActivateSpaceState() {
		if(currentState != State.Space) {
			DeactivateState(currentState);
			currentState = State.Space; 
		}

		Camera_State cameraState = Camera.main.GetComponent<Camera_State>();
		cameraState.ZoomToSpace(); 

		// TODO: Add space relateded scripts / objects / ...
	}

	public void DeactivateSpaceState() {
		// TODO: Add space relateded scripts / objects / ...

		Camera_State cameraState = Camera.main.GetComponent<Camera_State>();
		cameraState.ZoomToBar(); 
	}

	# endregion
}
