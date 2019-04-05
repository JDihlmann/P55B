using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_State : MonoBehaviour {

	// Rotation
	bool cameraRotationAllowed;
	Camera_Rotation cameraRotation; 

	// Bar camera setting
	public float barSize = 4.3f; 
	public Vector3 barPosition;
	public Vector3 barRotation;

	// Space camera setting 
	public float spaceSize = 30f; 
	public Vector3 spacePosition;
	public Vector3 spaceRotation;

	// Movement 
	public bool zoom = false; 
	float currentTime = 0f;
 	float timeToMove = 1.5f;

	// Position 
	float sizeLerp1;
	float sizeLerp2;   
	float positionLerp1;
	float positionLerp2;


	void Start() {
		// Rotation 
		cameraRotation = transform.GetComponent<Camera_Rotation>(); 

		// Bar camera setting
		barPosition = new Vector3(-28f, 22.8f, -28f);
		barRotation = new Vector3(30, 45, 0);

		// Space camera setting 
		spacePosition = new Vector3(-28f, 37.5f, -28f);
		spaceRotation =  new Vector3(30, 45, 0);
	}

	void LateUpdate () {
		if (zoom) {
			if (currentTime <= timeToMove)	{
            	currentTime += Time.deltaTime;
				Vector3 cameraPosition = new Vector3(-28f, 0, -28f);
				float t = currentTime / timeToMove;
				t = t*t*t * (t * (6f*t - 15f) + 10f);
            	Camera.main.orthographicSize = Mathf.Lerp(sizeLerp1, sizeLerp2, t);
				cameraPosition.y = Mathf.Lerp(positionLerp1, positionLerp2, t);
				Camera.main.transform.position = cameraPosition;
			} else {
				Camera.main.orthographicSize = Mathf.Lerp(sizeLerp1, sizeLerp2, currentTime / timeToMove);
				Camera.main.transform.position = new Vector3(-28f, positionLerp2, -28f);
				currentTime = 0f;
				zoom = false;
			}
		}
	
	}

	public void ZoomToBar() {
		sizeLerp1 = spaceSize;
		sizeLerp2 = barSize;   
		positionLerp1 = spacePosition.y;
		positionLerp2 = barPosition.y;

		zoom = true; 
	}

	public void ZoomToSpace() {
		sizeLerp1 = barSize;
		sizeLerp2 = spaceSize;   
		positionLerp1 = barPosition.y;
		positionLerp2 = spacePosition.y;

		zoom = true; 
	}
	
}
