using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

	#region Variables
	//[Header("Components")]

	//[Space]
	//[Header("Variables")]

	#endregion

	#region Methods
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Moveable")
		{
			Destroy(other.gameObject);
			IngredientSpawner.objectCounter -= 1;
		}
	}

	#endregion

}
