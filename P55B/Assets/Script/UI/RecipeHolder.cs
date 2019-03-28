using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RecipeHolder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private int recipeId;
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text price;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Image boughtTag;
    private bool bought;

    public RecipeObject recipe;

    private bool pointerDown;
    private float pointerDownTimer;
    private float requiredHoldTime = 1f;

    public UnityEvent onLongClick;

    public GameObject modalWindow;

    private Sprite sprite;

    private void Start()
    {
        recipeId = recipe.ID;
        bought = IngredientManager.GetRecipeBoughtState()[recipeId];
        name.text = recipe.Name;
        price.text = recipe.Price.ToString();
        boughtTag.gameObject.SetActive(bought);
        sprite = Resources.Load<Sprite>("Sprites/" + recipe.Image);
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
            if (pointerDownTimer >= requiredHoldTime)
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
        RecipeModalWindowHolder holderScript = holder.GetComponent<RecipeModalWindowHolder>();
        holderScript.title.text = recipe.Name;
        holderScript.image.sprite = sprite;
        holderScript.amount = recipe.Amount;
        holderScript.ingredients = recipe.Ingredients;
        holderScript.price.text = recipe.Price.ToString();
        //holderScript.image.SetNativeSize();
        holderScript.image.preserveAspect = true;
    }

}

