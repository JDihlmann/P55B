using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
	#region Variables
	//[Header("Components")]
	private RectTransform popupTransform;
	private Image image;
	//[Space]
	//[Header("Variables")]
	public int id;
	private float timer;
	private float speed;
	#endregion

	#region Methods
	private void Start()
	{
		popupTransform = gameObject.GetComponent<RectTransform>();
		image = gameObject.GetComponent<Image>();
		InitializePopup();
	}

	void Update()
    {
		if (timer < 0)
		{
			Destroy(gameObject);
		}
		popupTransform.anchoredPosition += Vector2.up * Time.deltaTime * speed;
		timer -= Time.deltaTime;
	}

	public void InitializePopup()
	{
		timer = Random.Range(1.3f, 2);
		speed = 135 - timer * 45;
		image.sprite = GamePlaySystem.Instance.popupImage[id];
		image.color = new Color32(0, 0, 0, 255);
		if (id >= 1 && id <= 3)
		{
			image.color = new Color32(255, 55, 55, 255);
		}
		else if (id >=4 && id <= 5)
		{
			image.color = new Color32(55, 255, 55, 255);
		}
		
	}

	#endregion
}
