using System.Collections;
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
    public Text happinessFactor;

    public ItemObject item; 

    private bool pointerDown;
    private float pointerDownTimer;
    private float requiredHoldTime = 1f;

    public UnityEvent onLongClick;

    public GameObject modalWindow;

    private void Start()
    {
        itemName.text = item.ItemName;
        happinessFactor.text = item.HappinessFactor.ToString();
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
                Debug.Log("long click");
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
    }

}
