using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectProperties
{
	#region Variables
	//[Header("Components")]

	//[Space]
	[Header("Variables")]
	public int objectId;
	public Vector2Int[,] objectPosition;
	public float objectRotation;
	#endregion

	#region Methods
	public ObjectProperties(int objectId, Vector2Int[,] objectPosition, float objectRotation)
	{
		this.objectId = objectId;
		this.objectPosition = objectPosition;
		this.objectRotation = objectRotation;
	}
	#endregion
}
