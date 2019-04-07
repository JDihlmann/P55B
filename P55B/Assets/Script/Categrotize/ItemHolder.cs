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
    private Image image;
    [SerializeField]
    private Text Price;
    [SerializeField]
    private GameObject CantBuy;

    public ItemObject item; 

    private bool pointerDown;
    private float pointerDownTimer;
    private float requiredHoldTime = 1f;

    public ObjectGrid_Instantiate objectGrid;
    public StateChange changer;

    public UnityEvent onLongClick;

    public GameObject modalWindow;

    private Sprite sprite;

    private void Start()
    {
        itemID = item.ItemID;
        itemName.text = item.ItemName;
        //happinessFactor.text = item.HappinessFactor.ToString();
        sprite = Resources.Load<Sprite>("Sprites/"+item.Image);
        image.sprite = sprite;
        image.preserveAspect = true;
        Price.text = item.Cost.ToString();
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
     
        CantBuy.gameObject.SetActive(!(GameSystem.Instance.money - item.Cost >= 0));

        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer >= requiredHoldTime)
            {
                Reset();
                if (GameSystem.Instance.money - item.Cost >= 0)
                {
                    this.gameObject.transform.parent.parent.parent.parent.parent.gameObject.SetActive(false);
                    changer.ActivateBuildState();
                    objectGrid.SpawnNewObjectWithID(itemID, item.Cost);
                    GameSystem.Instance.SubMoney(item.Cost);
                }
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
        holderScript.price.text = item.Cost.ToString();
        holderScript.happinessFactor.text = item.HappinessFactor.ToString();
        holderScript.description.text = item.Description;
        //holderScript.image.SetNativeSize();
        holderScript.image.preserveAspect = true;
    }

}
