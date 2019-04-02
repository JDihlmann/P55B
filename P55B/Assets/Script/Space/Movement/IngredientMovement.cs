using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class IngredientMovement : MonoBehaviour
{

	#region Variables
	[Header("Components")]
	public Transform planet;
	private Rigidbody rbody;
	[Space]
	[Header("Variables")]
	public float rotationSpeed;
    public float speed;
	[HideInInspector]
	public int playerTouchCount = 0;
	#endregion

	#region Methods
	void Start()
	{
		rbody = gameObject.GetComponent<Rigidbody>();   
    }

	void Update()
	{
		RotateWithin();
		MoveRight();
	}



    // Own Axis Rotation
    public void RotateWithin()
	{
		float angle = Time.deltaTime * rotationSpeed;
		Vector3 rotation = new Vector3(angle, angle, angle);
		transform.Rotate(rotation);
	}

	// Orbit Axis Rotation
	public void MoveRight()
	{
        transform.position += new Vector3(1, 0, -1f) * Time.deltaTime * speed;
	}


	public void Move(Vector3 velocity)
	{
		playerTouchCount += 1;

		if (playerTouchCount < 2)
		{
            rbody.AddForce(300 * velocity.x, 300 * velocity.y, 300 * velocity.z);

			// TODO Add more gravity to object:
			// rbody.mass = 0.01f;
		}

	}

	public void StopMovement()
	{
		// Disables player input:
		playerTouchCount = 2;
		// Stops the momentum of the object:
		rbody.velocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
		// gravAttrac.enabled = false;
	}

	#endregion

}
