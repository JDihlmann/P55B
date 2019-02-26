using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public Rigidbody rb;
	public float counter;

	void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.AddForce(new Vector3(-800f,-100f,0f));

	}

	void Update () {
		Vector3 velocity = -transform.position;
		float distance = velocity.magnitude == 0f ? 0.01f: velocity.magnitude;

		// float force = (rb.mass * 100) / Mathf.Pow(distance, 2);
		// rb.AddForce(force * velocity);

		if (distance > 40) {
			float force = (rb.mass * 500) / Mathf.Pow(distance, 2);
			rb.AddForce(force * velocity);
		} else if (distance < 10) {
			float force = (rb.mass * 1000) / Mathf.Pow(distance, 2);
			rb.AddForce(force * velocity);
		} else if (distance < 14) {
			float force = (rb.mass * distance) / Mathf.Pow(distance, 2);
			rb.AddForce(force * velocity);
		} else {
			float force = (rb.mass * 120) / Mathf.Pow(distance, 2);
			rb.AddForce(force * velocity);
		}


	}

	public void Move(Vector3 velocity) {
		rb.AddForce(100 * velocity);
	}
}
