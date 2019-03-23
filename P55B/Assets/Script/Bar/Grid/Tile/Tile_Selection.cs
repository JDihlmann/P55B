using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Selection: MonoBehaviour {

	// Renderer 
	private Renderer rend;

	// Color defined statics 
	public static Color defaultColor = Color.white;
	public Material tileDefault;
	public Material tileHighlightPlaceable;
	public Material tileHighlightNotPlaceable; 

	public static Color highlightColor = Color.green;

	void Start () {
		//Fetch the renderer from the gameobject
        rend = GetComponent<Renderer>();
	}

	public void HighlightTileMaterial(bool highlight, bool placeable) {

		if (highlight) {
			if (placeable) {
				rend.material = tileHighlightPlaceable;
			} else {
				rend.material = tileHighlightNotPlaceable; 
			}
		} else {
			rend.material = tileDefault;
		}
	}
}
