using UnityEngine;
﻿using UnityEditor;

[CustomEditor(typeof(Tiles))]
public class TileGenerator : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		Tiles tiles = (Tiles)target;

		if(GUILayout.Button("Generate Tiles")) {
			tiles.GenerateTiles();
		}
	}
}
