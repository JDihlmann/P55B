using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{

	private GameObject activeObject;

	void Update()
	{
		bool upAction = false;
		bool downAction = false;
		Vector3 end = new Vector2();
		Vector2 touchPosition = new Vector2(0.0f, 0.0f);

		// Mouse events
		if (Input.GetMouseButtonDown(0))
		{
			touchPosition = Input.mousePosition;
			downAction = true;
		}

		if (Input.GetMouseButtonUp(0))
		{
			touchPosition = Input.mousePosition;
			upAction = true;
		}


		// Touch events
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				touchPosition = touch.position;
				downAction = true;
			}

			if (touch.phase == TouchPhase.Ended)
			{
				touchPosition = touch.position;
				upAction = true;
			}
		}

		// Down action
		if (downAction)
		{
			Ray ray = Camera.main.ScreenPointToRay(touchPosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				// TODO: Select only viable objects
				if (hit.transform.tag == "Moveable")
				{
					activeObject = hit.transform.gameObject;
				}
			}
		}

		// Up action
		// TODO: Maybe limit by time
		if (upAction && activeObject != null)
		{
			end = Camera.main.ScreenToWorldPoint(touchPosition);
			activeObject.SendMessage("Move", end - activeObject.transform.position);
			activeObject = null;
		}
	}
}
