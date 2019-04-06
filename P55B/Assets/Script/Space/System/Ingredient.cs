using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

	#region Variables
	//[Header("Components")]
	private Renderer ingredientRenderer;
	//[Space]
	[Header("Variables")]
	public string ingredientName;
	public int index;
    Shader lightweightShader;
    Renderer rend;
	#endregion

	#region Methods
	void Start()
	{
		ingredientRenderer = gameObject.GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        lightweightShader = Shader.Find("LightweightPipeline/Standard (Physically Based)");
        rend.material.shader = lightweightShader;
	}
	#endregion

}
