using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    #region Variables
    //[Header("Components")]

    //[Space]
    //[Header("Variables")]
    public Camera cam;
    private float width;
    private float height;
    #endregion

    #region Methods
    void Start()
    {
        height = cam.ScreenToWorldPoint(new Vector3(0,Screen.height, 65f)).y;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 70f)).x;
        transform.localScale = new Vector3(4f*width, 2.5f*height, 30f);
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
