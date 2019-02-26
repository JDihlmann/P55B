using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Selection: MonoBehaviour {

	// Renderer 
	private Renderer rend;

	// Color defined statics 
	public static Color defaultColor = Color.white;
	public Material tileDefault;
	public Material tileHighlight; 

	public static Color highlightColor = Color.green;

	void Start () {
		//Fetch the renderer from the gameobject
        rend = GetComponent<Renderer>();
	}

	public void HighlightTileMaterial(bool highlight) {
		//Set the main color of the material to white
       	// rend.material.shader = Shader.Find("_Color");
        // rend.material.SetColor("_Color", color);

		if (highlight) {
			rend.material = tileHighlight;
		} else {
			rend.material = tileDefault;
		}



		//Find the specular shader and change its color to red
        // rend.material.shader = Shader.Find("Specular");
        // rend.material.SetColor("_SpecColor", Color.red);
		//rend.material = tileDefault; 
	}
}
