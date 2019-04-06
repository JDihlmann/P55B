using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

	public float offset; 
	public float lower_offset; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float delta = Mathf.Sin((Time.time + offset)*2); 
		Vector3 pos = transform.position;

		pos.y += (delta * lower_offset) * 0.0008f;
		transform.position = pos; 
	}
}
