using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
	#region Variables
	[Header("Components")]
	public Canvas canvas;
	public GameObject progressBar;
	public GameObject popupManager;
	public TextMeshProUGUI percentageText;
	public Image circleFill;
	private Camera camera;
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
		camera = Camera.main;
	}

    void Update()
    {
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
		progressBar.SetActive(true);
		isInteracting = true;
	}

	private void UpdateInteraction()
	{
		canvas.transform.LookAt(canvas.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
		percentageDone = (1 - currentTimer / startingTimer);
		circleFill.fillAmount = percentageDone;
		percentageText.text = Mathf.Round(percentageDone * 100) + "%";
	}

	private void StopInteraction()
	{
		progressBar.SetActive(false);
		isInteracting = false;
	}

	public void UpdateCustomerHappiness(float happiness)
	{
		this.happiness = happiness;
	}
	#endregion
}
