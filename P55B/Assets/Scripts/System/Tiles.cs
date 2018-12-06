using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Tiles : MonoBehaviour {

	public int x = 10;
	public int z = 10;
	public GameObject tilePrefab;

	public void GenerateTiles() {
		DeleteAllChildren();
		transform.position = new Vector3(0, 0, 0);
		for(int i = 0; i < x; i++) {
			for(int j = 0; j < z; j++) {
				GameObject tile = PrefabUtility.InstantiatePrefab(tilePrefab) as GameObject;
				tile.transform.parent = gameObject.transform;
				Vector3 vector = new Vector3(-x/2 + i + 0.5f, 0.5f, -z/2 + j + 0.5f);
				tile.transform.position = vector;
			}
		}
	}

	// Delete all child in edit mode work around
	public void DeleteAllChildren() {
		var tempArray = new GameObject[transform.childCount];


    for(int i = 0; i < tempArray.Length; i++) {
    	tempArray[i] = transform.GetChild(i).gameObject;
    }

    foreach(var child in tempArray) {
			DestroyImmediate(child);
    }
	}
}
