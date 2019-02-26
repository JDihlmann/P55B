using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(Grid_Instantiate))]
public class Grid_Editor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		Grid_Instantiate grid = (Grid_Instantiate)target;

		if(GUILayout.Button("Generate Grid")) {
			grid.GenerateGrid();
		}
	}
}