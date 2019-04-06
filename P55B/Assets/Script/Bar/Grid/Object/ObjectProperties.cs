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
	public int objectPositionX;
	public int objectPositionY;
	public float objectRotation;
	#endregion

	#region Methods
	public ObjectProperties(int objectId, Vector2Int objectPosition, float objectRotation)
	{
		this.objectId = objectId;
		this.objectPositionX = objectPosition.x;
		this.objectPositionY = objectPosition.y;
		this.objectRotation = objectRotation;
	}
	#endregion
}
