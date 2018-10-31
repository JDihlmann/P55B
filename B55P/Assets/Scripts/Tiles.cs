using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tiles : MonoBehaviour {
	public int x;
	public int y;

	// Use this for initialization
	public void GenerateTiles() {
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = gameObject.transform;
		//cube.transform.position = new Vector3(0, 0, 0);
	}
}
