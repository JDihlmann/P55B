using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

	// Enum possible states
	public enum State {UI, Bar, Build, Space};

    // Current state
    public State currentState;

    public State lastState;

    // UI GameObjects
    public GameObject planetButton;
    public GameObject leftButton;
    public GameObject barButton;
    public GameObject secondBarButton;
    public GameObject buildButton;

	// Bar GameObjects
	// TODO: Insert GameObjects 

	// Build GameObjects
	public GameObject objectGrid;

	// Space GameObjects
	// TODO: Insert GameObjects 


	void Start () {
		// TODO: Choose Initial State
		// !!! BUILD SCRIPT MUST BE ENABLED TO LOAD OBJECTS !!! -> DEACTIVATE AFTER STARTUP 

	}

    private void Update()
    {
        if(!Camera.main.GetComponent<Camera_State>().zoom && currentState == State.Space)
        {
            barButton.SetActive(true);
        } else if (!Camera.main.GetComponent<Camera_State>().zoom && currentState != State.Build)
        {
            leftButton.SetActive(true);
            planetButton.SetActive(true);
        }
    }

    #region General Changes

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
		if(currentState != State.UI) {
            lastState = currentState;
            DeactivateState(currentState);
			currentState = State.UI; 
		}

		Time.timeScale = 0;
		// TODO: Add ui relateded scripts / objects / ...

	}

	public void DeactivateUIState() {
        if(lastState == State.Bar)
        {
            currentState = State.Bar;
            ActivateBarState();
        } else if(lastState == State.Build)
        {
            currentState = State.Build;
            ActivateBuildState();
        }

		Time.timeScale = 1;
		// TODO: Add ui relateded scripts / objects / ...
	}

	# endregion

	# region Bar State

	public void ActivateBarState() {
		if(currentState != State.Bar) {
			DeactivateState(currentState);
			currentState = State.Bar; 
		}

        secondBarButton.SetActive(false);
        buildButton.SetActive(true);

		Time.timeScale = 1;
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

        // hide planet button
        planetButton.SetActive(false);
        secondBarButton.SetActive(true);
        buildButton.SetActive(false);

		Time.timeScale = 0;
		// Customer verschwinden
		GamePlaySystem.Instance.gameObject.SetActive(false);
		
	}

	public void DeactivateBuildState() {
		// Place last object, reset all values and disable script
		ObjectGrid_Placement objectGridPlacements = objectGrid.GetComponent<ObjectGrid_Placement>();
		objectGridPlacements.TryPlacingObjectOnGrid();
		objectGridPlacements.ResetAllValues(); 

		objectGridPlacements.enabled = false;

        // show planet button
        planetButton.SetActive(true);

		// Customer appear
		Time.timeScale = 1;
		GamePlaySystem.Instance.gameObject.SetActive(true);

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

        leftButton.SetActive(false);
        planetButton.SetActive(false);

		Time.timeScale = 1;
		// TODO: Add space relateded scripts / objects / ...
	}

	public void DeactivateSpaceState() {
		// TODO: Add space relateded scripts / objects / ...

		Camera_State cameraState = Camera.main.GetComponent<Camera_State>();
		cameraState.ZoomToBar();

        barButton.SetActive(false);
    }

	# endregion
}
