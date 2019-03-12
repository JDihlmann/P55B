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
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer >= requiredHoldTime)
            {
                Reset();
                modalWindow.transform.SetAsLastSibling();
                GameObject holder = Instantiate(modalWindow, this.gameObject.transform.parent.parent);
                ModalWindowHolder holderScript = holder.GetComponent<ModalWindowHolder>();
                holderScript.titel.text = item.ItemName;
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }

}
