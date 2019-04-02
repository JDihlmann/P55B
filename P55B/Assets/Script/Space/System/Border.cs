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
        height = cam.ScreenToWorldPoint(new Vector3(0,Screen.height, 54f)).y;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 54f)).x;
        transform.localScale = new Vector3(3.5f*width, 3f*height, 30f);
        transform.Rotate(0, 45f,0);
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
