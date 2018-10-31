using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	Vector3 velocity     = new Vector3(0.0f, 1.0f, 0.0f);
	float floorHeight    = 0.0f;
	float sleepThreshold = 0.05f;
	float gravity        = -9.8f;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0.0f, 1.5f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		if (velocity.magnitude > sleepThreshold || transform.position.y > floorHeight)
        {
            velocity += new Vector3(0.0f, gravity * Time.fixedDeltaTime, 0.0f);
        }

        transform.position += velocity * Time.fixedDeltaTime;
        if (transform.position.y <= floorHeight)
        {
            transform.position = new Vector3(0.0f, floorHeight, 0.0f);
            velocity.y = -velocity.y;
        }
	}
}
