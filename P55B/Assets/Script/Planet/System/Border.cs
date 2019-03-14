using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    #region Variables
    //[Header("Components")]

    //[Space]
    //[Header("Variables")]
    private Camera cam;
    private float width;
    private float height;
    #endregion

    #region Methods
    void Start()
    {
        cam = Camera.main;
        height = cam.ScreenToWorldPoint(new Vector3(0,Screen.height, 15.5f)).y;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 15.5f)).x;
        transform.localScale = new Vector3(2f*width, 2f*height, 3f);
    }

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
