using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {

	#region Variables
	//[Header("Components")]
	[HideInInspector]
	public Rigidbody rbody;
	//[Space]
	//[Header("Variables")]
	public static List<GravityAttractor> Attractors;

	#endregion

	#region Methods
	private void Start() {
		rbody = gameObject.GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		foreach (GravityAttractor attractor in Attractors) {
			if (attractor != this) {
				Attract(attractor);
			}
		}
	}

	private void OnEnable() {
		if (Attractors == null) {
			Attractors = new List<GravityAttractor>();
		}
		Attractors.Add(this);
	}

	private void OnDisable()
	{
		Attractors.Remove(this);
	}

	void Attract(GravityAttractor objToAttract)
	{
		Rigidbody rbodyToAttract = objToAttract.rbody;

		Vector3 direction = rbody.position - rbodyToAttract.position;
		float distance = direction.magnitude;

		if (distance == 0f)
		{
			return;
		}

		float forceMagnitude = (rbody.mass * rbodyToAttract.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;

		rbodyToAttract.AddForce(force);
	}
	#endregion

}
