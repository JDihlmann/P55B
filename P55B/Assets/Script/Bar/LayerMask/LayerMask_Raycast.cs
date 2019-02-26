using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMask_Raycast : MonoBehaviour {

	// Distance
	private static int rayDistance = 100;

	// Layermask 
	private int layermaskObject;
	private int layermaskMoveable;
	private int layermaskPlaceable;

	// Raycast hit
	public RaycastHit objectHit;
	public RaycastHit moveableHit;
	public RaycastHit placeableHit;

	void Start () {
		layermaskObject = LayerMask.GetMask("Object");
		layermaskMoveable = LayerMask.GetMask("Moveable");
		layermaskPlaceable = LayerMask.GetMask("Placeable");
	}

	public bool IsMoveable() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out moveableHit, rayDistance, layermaskMoveable); 
	}

	public bool IsPlaceable() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out placeableHit, rayDistance, layermaskPlaceable); 
	}

	public bool IsObject() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out objectHit, rayDistance, layermaskObject); 
	}
}
