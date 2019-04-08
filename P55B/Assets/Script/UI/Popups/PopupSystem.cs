using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public GameObject popupPrefab;
	public Canvas canvas;
	public GameObject progressBar;
	public GameObject popupManager;
	public TextMeshProUGUI percentageText;
	public Image circleFill;
	private Camera mainCamera;
	[Space]
	[Header("Variables")]
	public bool isInteracting;
	private float startingTimer;
	private float currentTimer;
	private float percentageDone;
	private float happiness;
	#endregion

	#region Methods
	void Start()
    {
		mainCamera = Camera.main;
	}

    void Update()
    {
		if (canvas != null)
		{
			canvas.transform.LookAt(canvas.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
		}
		if (isInteracting)
		{
			if (currentTimer > 0)
			{
				currentTimer -= Time.deltaTime;
				UpdateInteraction();
			}
			else
			{
				StopInteraction();
			}
		}
    }

	public void StartInteraction(float time)
	{
		startingTimer = time;
		currentTimer = time;
		UpdateInteraction();
		if (progressBar != null)
		{
			progressBar.SetActive(true);
		}
		isInteracting = true;
	}

	private void UpdateInteraction()
	{
		if (canvas == null)
		{
			StopInteraction();
		}
		else
		{
			percentageDone = (1 - currentTimer / startingTimer);
			circleFill.fillAmount = percentageDone;
			UpdateColor();
			percentageText.text = Mathf.Round(percentageDone * 100) + "%";
		}
	}

	private void UpdateColor()
	{
		if (happiness >= 85)
		{
			circleFill.color = new Color32(0, 255, 0, 255);
		}
		else if (happiness < 85 && happiness >= 33)
		{
			int temp = Mathf.RoundToInt(255 - 5 * (happiness - 33));
			circleFill.color = new Color32((byte)temp, 255, 0, 255);
		}
		else
		{
			int temp = Mathf.RoundToInt(9 * happiness);
			if (temp > 255)
			{
				temp = 255;
			}
			circleFill.color = new Color32(255, (byte) temp, 0, 255);
		}
	}

	private void StopInteraction()
	{
		if (progressBar != null)
		{
			progressBar.SetActive(false);
		}
		isInteracting = false;
	}

	public void UpdateCustomerHappiness(float happiness)
	{
		this.happiness = happiness;
	}

	public void CreatePopup(int id)
	{
		GameObject newPopup = Instantiate(popupPrefab, popupManager.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
		newPopup.transform.SetParent(popupManager.transform, false);
		newPopup.GetComponent<Popup>().id = id;
		// popupManager
	}
	#endregion
}
