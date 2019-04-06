using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float delta1 = Mathf.Sin(Time.time + 0); 
		float delta2 = Mathf.Sin(Time.time + 1); 
		float delta3 = Mathf.Sin(Time.time + 2); 
		Vector3 angle = transform.eulerAngles;

		angle.x += 0.5f ;
		angle.y += 0.5f ;
		angle.z += 0.5f  ;

		//transform.eulerAngles = angle; 
		
		transform.Rotate ( new Vector3(delta1, delta2, delta3) * ( 100 * Time.deltaTime ) );
	}
}

