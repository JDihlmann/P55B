﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public int itemID;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text happinessFactor;
    [SerializeField]
    private Image image;

    public ItemObject item; 

    private bool pointerDown;
    private float pointerDownTimer;
    private float requiredHoldTime = 1f;

    public ObjectGrid_Instantiate objectGrid;

    public UnityEvent onLongClick;

    public GameObject modalWindow;

    private Sprite sprite;

    private void Start()
    {
        itemID = item.ItemID;
        itemName.text = item.ItemName;
        happinessFactor.text = item.HappinessFactor.ToString();
        sprite = Resources.Load<Sprite>("Sprites/"+item.Image);
        image.sprite = sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        if (!eventData.dragging)
        {
            OpenModal();
        }
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer >= requiredHoldTime)
            {
                Reset();
                this.gameObject.transform.parent.parent.parent.parent.parent.gameObject.SetActive(false);
                objectGrid.SpawnNewObjectWithID(itemID);
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }

    private void OpenModal()
    {
        modalWindow.transform.SetAsLastSibling();
        GameObject holder = Instantiate(modalWindow, this.gameObject.transform.parent.parent.parent.parent.parent);
        ModalWindowHolder holderScript = holder.GetComponent<ModalWindowHolder>();
        holderScript.titel.text = item.ItemName;
        holderScript.image.sprite = sprite;
        //holderScript.image.SetNativeSize();
        holderScript.image.preserveAspect = true;
    }

}
